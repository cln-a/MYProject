using MvCamCtrl.NET;

namespace Application.Camera
{
    public class CameraFrameEventArgs : EventArgs
    {
        public byte[] Buffer { get; }
        public int Width { get; }
        public int Height { get; }
        public MyCamera.MvGvspPixelType PixelType { get; }

        public CameraFrameEventArgs(byte[] buffer, int width, int height, MyCamera.MvGvspPixelType pixelType)
        {
            Buffer = buffer;
            Width = width;
            Height = height;
            PixelType = pixelType;
        }
    }
}
