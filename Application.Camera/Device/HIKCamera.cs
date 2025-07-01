using Microsoft.Extensions.Logging;
using MvCamCtrl.NET;
using System.Runtime.InteropServices;

namespace Application.Camera
{
    public class HIKCamera : ICamera , IDisposable
    {
        private readonly ILogger _logger;

        private MyCamera.MV_CC_DEVICE_INFO _device;
        private MyCamera m_MyCamera;
        private MyCamera.cbOutputExdelegate _imageCallback = null!;
        private IntPtr m_BufForDriver;
        private uint m_nBufSizeForDriver;

        public ILogger Logger => _logger;

        //public string SerialNumber => Encoding.ASCII.GetString(_device.SpecialInfo.stGigEInfo).TrimEnd('\0');
        public string SerialNumber => "OPT_Camera";

        public event EventHandler<CameraFrameEventArgs>? OnImageReceived;

        public HIKCamera(MyCamera.MV_CC_DEVICE_INFO _device, ILogger logger)
        {
            this._device = _device;
            this._logger = logger;

            this.m_MyCamera = new MyCamera();
        }

        public bool Open()
        {
            //创建句柄
            int nRet = m_MyCamera.MV_CC_CreateDevice_NET(ref _device);
            if (nRet != MyCamera.MV_OK)
            {
                Logger.LogDebug($"创建句柄失败,错误码：{nRet}");
                return false ;
            }
            //开启设备
            nRet = m_MyCamera.MV_CC_OpenDevice_NET();
            if (nRet != MyCamera.MV_OK)
            {
                Logger.LogDebug($"打开设备失败,错误码：{nRet}");
                return false;
            }
            // ch:探测网络最佳包大小(只对GigE相机有效) | en:Detection network optimal package size(It only works for the GigE camera)
            if (_device.nTLayerType == MyCamera.MV_GIGE_DEVICE)
            {
                int nPacketSize = m_MyCamera.MV_CC_GetOptimalPacketSize_NET();
                if (nPacketSize > 0)
                {
                    nRet = m_MyCamera.MV_CC_SetIntValue_NET("GevSCPSPacketSize", (uint)nPacketSize);
                    if (nRet != MyCamera.MV_OK)
                        return false;  
                }
            }
            m_MyCamera.MV_CC_SetEnumValue_NET("AcquisitionMode", (uint)MyCamera.MV_CAM_ACQUISITION_MODE.MV_ACQ_MODE_CONTINUOUS);
            m_MyCamera.MV_CC_SetEnumValue_NET("TriggerMode", (uint)MyCamera.MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_OFF);
            return true;
        }

        public bool StartGrabbing()
        {
            _imageCallback = new MyCamera.cbOutputExdelegate(ImageGrayCallback);
            //注册回调函数
            int ret = m_MyCamera.MV_CC_RegisterImageCallBackEx_NET(_imageCallback, IntPtr.Zero);
            if (ret != MyCamera.MV_OK)
            {
                Logger.LogDebug($"注册图像回调失败，错误码: {ret}");
                return false;
            }
            //开启取流
            ret = m_MyCamera.MV_CC_StartGrabbing_NET();
            if (ret != MyCamera.MV_OK)
            {
                Logger.LogDebug($"开始取流失败，错误码: {ret}");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 注册给HikCamera的回调函数，用于在每一帧图像采集到时被自动调用
        /// </summary>
        /// <param name="pData">图像数据指针</param>
        /// <param name="pFrameInfo">图像帧信息结构体</param>
        /// <param name="pUser">用户自定义指针</param>
        private void ImageGrayCallback(nint pData, ref MyCamera.MV_FRAME_OUT_INFO_EX pFrameInfo, nint pUser)
        {
            Logger.LogDebug("开始执行回调函数");
            // 1. 图像大小
            int dataLength = (int)pFrameInfo.nFrameLen;

            if (dataLength == 0 || pData == IntPtr.Zero)
                return;

            // 2. 将非托管图像数据复制到托管 byte[]
            byte[] buffer = new byte[dataLength];
            Marshal.Copy(pData, buffer, 0, dataLength);

            // 3. 构造事件参数并触发事件
            var args = new CameraFrameEventArgs(
                buffer,
                (int)pFrameInfo.nWidth,
                (int)pFrameInfo.nHeight,
                pFrameInfo.enPixelType
            );

            OnImageReceived?.Invoke(this, args);
        }

        public bool StopGrabbing()
        {
            //停止取流
            var ret = m_MyCamera.MV_CC_StopGrabbing_NET();
            if (ret != MyCamera.MV_OK)
            {
                Logger.LogDebug($"关闭图像取流失败，错误码: {ret}");
                return false;
            }
            return true;
        }

        public bool Close()
        {
            StopGrabbing();
            //关闭设备
            var ret = m_MyCamera.MV_CC_CloseDevice_NET();
            if (ret != MyCamera.MV_OK)
            {
                Logger.LogDebug($"关闭设备失败，错误码: {ret}");
                return false ;
            }
            //销毁句柄
            ret = m_MyCamera.MV_CC_DestroyDevice_NET();
            if (ret != MyCamera.MV_OK)
            {
                Logger.LogDebug($"销毁句柄失败");
                return false;
            }
            return true;
        }

        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        public static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);

        public void Dispose()
        {
            Close();
            if (m_BufForDriver != IntPtr.Zero)
                Marshal.FreeHGlobal(m_BufForDriver);
        }
    }
}
