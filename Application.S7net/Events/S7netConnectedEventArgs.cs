using Application.Model;

namespace Application.S7net
{
    public class S7netConnectedEventArgs : EventArgs
    {
        private bool _connected;
        private S7netDevice _device;
        
        public bool Connected => _connected;
        public S7netDevice Device => _device;

        public S7netConnectedEventArgs(bool connected, S7netDevice device)
        {
            this._connected = connected;
            this._device = device;
        }
    }
}
