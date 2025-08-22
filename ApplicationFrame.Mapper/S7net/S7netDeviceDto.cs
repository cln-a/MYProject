namespace Application.Mapper
{
    public class S7netDeviceDto : BaseDomainDto
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

        public int RemotePort
        {
            get => _remotePort;
            set => SetProperty(ref _remotePort, value); 
        }

        public short DeviceRack
        {
            get => _deviceRack;
            set => SetProperty(ref _deviceRack, value);
        }

        public short DeviceSlot
        {
            get => _deviceSlot;
            set => SetProperty(ref _deviceSlot, value);
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
