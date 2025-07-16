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

        public string? DeviceUri
        {
            get => _deviceUri;
            set => SetProperty(ref _deviceUri, value);
        }

        public string? DeviceName
        {
            get => _deviceName;
            set => SetProperty(ref _deviceName, value);
        }

        public string? RemoteIpAddress
        {
            get => _remoteIpAddress;
            set => SetProperty(ref _remoteIpAddress, value);
        }

        public string? LocalIpAddress
        {
            get => _localIpAddress;
            set => SetProperty(ref _localIpAddress, value);
        }

        public byte slaveId
        {
            get => _slaveId;
            set => SetProperty(ref _slaveId, value);
        }

        public int RemotePort
        {
            get => _remotePort;
            set => SetProperty(ref _remotePort, value);
        }

        public int LocalPort
        {
            get => _localPort;
            set => SetProperty(ref _localPort, value);
        }

        public int ReconnectInterval
        {
            get => _reconnectInterval;
            set => SetProperty(ref _reconnectInterval, value);
        }

        public int ReadInterval
        {
            get => _readInterval;
            set => SetProperty(ref _readInterval, value);
        }

        public string? DeviceBrand
        {
            get => _deviceBrand;
            set => SetProperty(ref _deviceBrand, value);
        }
    }
}
