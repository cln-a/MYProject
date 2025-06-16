namespace Application.Common
{
    public interface ICommunicationStateMachine
    {
        public string Name { get; }
        public string Description { get;  }
        public bool IsConnected { get;  }
        public bool IsNotConnected { get; }
        public bool IsCommunicating { get; }
        public CommunicationStateEnum State { get; }
        void Start();
        void Stop();
        void SetCommunicating();
        void SetConnected();
        void SetDisConnected();

        event EventHandler<CommunicationStateChangedEventArgs> CommunicationStateChangedEvent;
    }
}
