namespace Application.Model
{
    public class S7netDevice : BaseDomain
    {
        protected string? _deviceUri;
        protected string? _deviceName;
        protected string? _remoteIpAddress;
        protected int _remotePort;
        protected short _deviceRack;
        protected short _deviceSlot;
        protected int _reconnectInterval;
        protected int _readInterval;
        protected string? _deviceBrand;

        /// <summary>
        /// 设备唯一标识符
        /// </summary>
        public string? DeviceUri 
        { 
            get => _deviceUri; 
            set => _deviceUri = value;
        }

        /// <summary>
        /// 设备名称
        /// </summary>
        public string? DeviceName 
        { 
            get => _deviceName; 
            set => _deviceName = value;
        }

        /// <summary>
        /// 远程IP
        /// </summary>
        public string? RemoteIpAddress 
        { 
            get => _remoteIpAddress; 
            set => _remoteIpAddress = value;
        }

        /// <summary>
        /// 远程端口
        /// </summary>
        public int RemotePort 
        { 
            get => _remotePort; 
            set => _remotePort = value;
        }

        /// <summary>
        /// 设备机架号
        /// </summary>
        public short DeviceRack 
        { 
            get => _deviceRack; 
            set => _deviceRack = value;
        }

        /// <summary>
        /// 设备槽号
        /// </summary>
        public short DeviceSlot 
        { 
            get => _deviceSlot; 
            set => _deviceSlot = value;
        }

        /// <summary>
        /// 重连周期
        /// </summary>
        public int ReconnectInterval 
        {
            get => _reconnectInterval;
            set => _reconnectInterval = value;
        }

        /// <summary>
        /// 读取间隔
        /// </summary>
        public int ReadInterval 
        {
            get => _readInterval; 
            set => _readInterval = value;
        }

        /// <summary>
        /// 设备品牌
        /// </summary>
        public string? DeviceBrand 
        { 
            get => _deviceBrand; 
            set => _deviceBrand = value;
        }
    }
}
