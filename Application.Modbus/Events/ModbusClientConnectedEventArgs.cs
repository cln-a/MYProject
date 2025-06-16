using Application.Model;

namespace Application.Modbus
{
    public class ModbusClientConnectedEventArgs : EventArgs
    {
        private bool _connected;
        private ModbusDevice _device;

        public bool Connected => _connected;
        public ModbusDevice Device => _device;

        public ModbusClientConnectedEventArgs(ModbusDevice device, bool connected)
        {
            this._device = device;
            this._connected = connected;
        }
    }
}
