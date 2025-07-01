using Application.Common;

namespace Application.Camera
{
    public class CameraStateChangedEventArgs : EventArgs
    {
        public string Name { get; set; }        
        public CommunicationStateEnum State { get; set; }

        public CameraStateChangedEventArgs() { }

        public CameraStateChangedEventArgs(string name, CommunicationStateEnum state)
        {
            this.Name = name;
            this.State = state;
        }
    }
}
