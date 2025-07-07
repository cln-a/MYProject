namespace Application.Mapper
{
    public class ModbusDeviceDto : BaseDomainDto
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

        public string? DeviceUri { get => _deviceUri; set => _deviceUri = value; }

        public string? DeviceName { get => _deviceName; set => _deviceName = value; }

        public string? RemoteIpAddress { get => _remoteIpAddress; set => _remoteIpAddress = value; }

        public string? LocalIpAddress { get => _localIpAddress; set => _localIpAddress = value; }

        public byte slaveId { get => _slaveId; set => _slaveId = value; }

        public int RemotePort{ get => _remotePort; set => _remotePort = value; }

        public int LocalPort { get => _localPort; set => _localPort = value; }

        public int ReconnectInterval { get => _reconnectInterval; set => _reconnectInterval = value; }

        public int ReadInterval { get => _readInterval; set => _reconnectInterval = value; }

        public string? DeviceBrand{ get => _deviceBrand; set => _deviceBrand = value; }
    }
}
