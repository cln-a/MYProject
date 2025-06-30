namespace Application.Camera
{
    public interface ICameraController
    {
        void InitializeAllCameras();
        IReadOnlyList<ICamera> Cameras { get; }
        ICamera? GetBySerial(string serial);
    }
}
