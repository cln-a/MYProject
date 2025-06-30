using Microsoft.Extensions.Logging;
using MvCamCtrl.NET;
using System.Runtime.InteropServices;

namespace Application.Camera
{
    public class HIKCameraManager : ICameraManager
    {
        private readonly ILogger _logger;
        private List<MyCamera.MV_CC_DEVICE_INFO> _deviceList;
        
        public List<MyCamera.MV_CC_DEVICE_INFO> DeviceList => _deviceList;

        public ILogger Logger => _logger;

        public HIKCameraManager(ILogger logger)
        {
            this._logger = logger;
            this._deviceList = new List<MyCamera.MV_CC_DEVICE_INFO>();
        }

        public List<MyCamera.MV_CC_DEVICE_INFO> EnumerateDevices()
        {
            MyCamera.MV_CC_DEVICE_INFO_LIST stDeviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();
            int nRet = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref stDeviceList);
            if (nRet != MyCamera.MV_OK)
            {
                Logger.LogDebug($"设备枚举失败，错误码：{nRet}");
            }
            for (int i = 0; i < stDeviceList.nDeviceNum; i++)
            {
                IntPtr ptr = stDeviceList.pDeviceInfo[i];
                var device = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(ptr, typeof(MyCamera.MV_CC_DEVICE_INFO))!;
                _deviceList.Add(device);
            }
            return _deviceList;
        }
    }
}
