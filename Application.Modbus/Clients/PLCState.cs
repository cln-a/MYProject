using Application.Common;

namespace Application.Modbus.Clients
{
    public class PLCState : ICommunicationStateMachine
    {
        public string Name => throw new NotImplementedException();

        public string Description => throw new NotImplementedException();

        public bool IsConnected => throw new NotImplementedException();

        public bool IsNotConnected => throw new NotImplementedException();

        public bool IsCommunicating => throw new NotImplementedException();

        public CommunicationStateEnum State => throw new NotImplementedException();

        public event EventHandler<CommunicationStateChangedEventArgs> CommunicationStateChangedEvent;

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
