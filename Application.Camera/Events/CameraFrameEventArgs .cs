using MvCamCtrl.NET;

namespace Application.Camera
{
    public class CameraFrameEventArgs : EventArgs
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public IntPtr DataPtr { get; set; }
        public uint DataLen { get; set; }
        public MyCamera.MvGvspPixelType PixelType { get; set; }
    }
}
