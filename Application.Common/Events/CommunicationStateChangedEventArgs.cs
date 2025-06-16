namespace Application.Common
{
    public class CommunicationStateChangedEventArgs : EventArgs
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public CommunicationStateEnum State { get; set; }

        public CommunicationStateChangedEventArgs()
        {
            
        }

        public CommunicationStateChangedEventArgs(string name, string description, CommunicationStateEnum state)
        {
            Name = name;
            Description = description;
            State = state;
        }
    }
}
