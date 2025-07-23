using SqlSugar;

namespace Application.Model
{
    [SugarTable]
    public class ModbusDevice : BaseDomain
    {
        protected string? _deviceUri;
        protected string? _deviceName;
        protected string? _remoteIpAddress;
        protected string? _localIpAddress;
        protected byte _slaveId;
        protected int _remotePort;
        protected int _localPort;
        protected int _reconnectInterval;
        protected int _readInterval;
        protected string? _deviceBrand;

        /// <summary>
        /// 设备唯一标识符
        /// </summary>
        [SugarColumn]
        public string? DeviceUri
        {
            get => _deviceUri;
            set => _deviceUri = value;
        }

        /// <summary>
        /// 设备唯一标识符
        /// </summary>
        [SugarColumn]
        public string? DeviceName
        {
            get => _deviceName;
            set => _deviceName = value;
        }

        /// <summary>
        /// 远程端口
        /// </summary>
        [SugarColumn]
        public string? RemoteIpAddress
        {
            get => _remoteIpAddress;
            set => _remoteIpAddress = value;
        }

        /// <summary>
        /// 本地端口
        /// </summary>
        [SugarColumn]
        public string? LocalIpAddress
        {
            get => _localIpAddress;
            set => _localIpAddress = value;
        }

        /// <summary>
        /// 从机Id
        /// </summary>
        [SugarColumn]
        public byte slaveId
        {
            get => _slaveId;
            set => _slaveId = value;
        }

        /// <summary>
        /// 远程端口
        /// </summary>
        [SugarColumn]
        public int RemotePort
        {
            get => _remotePort;
            set => _remotePort = value;
        }

        /// <summary>
        /// 本地端口
        /// </summary>
        [SugarColumn]
        public int LocalPort
        {
            get => _localPort;
            set => _localPort = value;
        }

        /// <summary>
        /// 重连间隔
        /// </summary>
        [SugarColumn]
        public int ReconnectInterval
        {
            get => _reconnectInterval;
            set => _reconnectInterval = value;
        }

        /// <summary>
        /// 读取间隔
        /// </summary>
        [SugarColumn]
        public int ReadInterval
        {
            get => _readInterval;
            set => _readInterval = value;
        }

        /// <summary>
        /// 设备品牌
        /// </summary>
        [SugarColumn]
        public string? DeviceBrand
        {
            get => _deviceBrand;
            set => _deviceBrand = value;
        }
    }
}
