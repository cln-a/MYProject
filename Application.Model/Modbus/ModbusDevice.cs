namespace Application.Model
{
    public class ModbusDevice : BaseDomain<int>
    {
        protected string _deviceUri;
        protected string _deviceName;
        protected string _remoteIpAddress;
        protected string _localIpAddress;
        protected byte _slaveId;
        protected int _remotePort;
        protected int _localPort;
        protected int _reconnectInterval;
        protected int _readInterval;
        protected string _deviceBrand;

        /// <summary>
        /// 设备唯一标识符
        /// </summary>
        public string DeviceUri
        {
            get => _deviceUri;
            set => SetProperty(ref _deviceUri, value);
        }

        /// <summary>
        /// 设备唯一标识符
        /// </summary>
        public string DeviceName
        {
            get => _deviceName;
            set => SetProperty(ref _deviceName, value);
        }

        /// <summary>
        /// 远程端口
        /// </summary>
        public string RemoteIpAddress
        {
            get => _remoteIpAddress;
            set => SetProperty(ref _remoteIpAddress, value);
        }

        /// <summary>
        /// 本地端口
        /// </summary>
        public string LocalIpAddress
        {
            get => _localIpAddress;
            set => SetProperty(ref _localIpAddress, value);
        }

        /// <summary>
        /// 从机Id
        /// </summary>
        public byte slaveId
        {
            get => _slaveId;
            set => SetProperty(ref _slaveId, value);
        }

        /// <summary>
        /// 远程端口
        /// </summary>
        public int RemotePort
        {
            get => _remotePort;
            set => SetProperty(ref _remotePort, value);
        }

        /// <summary>
        /// 本地端口
        /// </summary>
        public int LocalPort
        {
            get => _localPort;
            set => SetProperty(ref _localPort, value);
        }

        /// <summary>
        /// 重连间隔
        /// </summary>
        public int ReconnectInterval
        {
            get => _reconnectInterval;
            set => SetProperty(ref _reconnectInterval, value);
        }

        /// <summary>
        /// 读取间隔
        /// </summary>
        public int ReadInterval
        {
            get => _readInterval;
            set => SetProperty(ref _readInterval, value);
        }

        /// <summary>
        /// 设备品牌
        /// </summary>
        public string DeviceBrand
        {
            get => _deviceBrand;
            set => SetProperty(ref _deviceBrand, value);
        }
    }
}
