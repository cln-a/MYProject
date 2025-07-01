namespace Application.Camera
{
    public interface ICameraController
    {
        void InitializeAllCameras(); 
        void StopAllCameras();
        IReadOnlyList<ICamera> Cameras { get; }
        ICamera? GetBySerial(string serial);
    }
}
