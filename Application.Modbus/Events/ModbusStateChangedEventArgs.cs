namespace Application.Modbus
{
    public class ModbusStateChangedEventArgs : EventArgs
    {
        public bool Connected { get; private set; }
        public long DeviceId { get; private set; }
        public string DeviceName { get; private set; }

        public ModbusStateChangedEventArgs(bool connected,long deviceid,string devicename)
        {
            this.Connected = connected;
            this.DeviceId = deviceid;
            this.DeviceName = devicename;
        }
    }
}
