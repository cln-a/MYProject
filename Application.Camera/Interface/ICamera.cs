namespace Application.Camera
{
    public interface ICamera
    {
        string SerialNumber { get; }

        /// <summary>
        /// 创建句柄 & 开启设备
        /// </summary>
        /// <returns></returns>
        bool Open();

        /// <summary>
        /// 注册回调函数 & 开始取流
        /// </summary>
        /// <returns></returns>
        bool StartGrabbing();

        /// <summary>
        /// 停止取流
        /// </summary>
        /// <returns></returns>
        bool StopGrabbing();

        /// <summary>
        /// 关闭设备 & 销毁句柄
        /// </summary>
        /// <returns></returns>
        bool Close();
    }
}
