using MvCamCtrl.NET;

namespace Application.Common
{
    public class CameraFrameEventArgs : EventArgs
    {
        public byte[] Buffer { get; }
        public int Width { get; }
        public int Height { get; }
        public MyCamera.MvGvspPixelType PixelType { get; }
        public nint pData {  get; }

        public CameraFrameEventArgs(byte[] buffer, int width, int height, MyCamera.MvGvspPixelType pixelType, nint pData)
        {
            Buffer = buffer;
            Width = width;
            Height = height;
            PixelType = pixelType;
            this.pData = pData;
        }
    }
}
