using MvCamCtrl.NET;

namespace Application.ImageProcess
{
    public class ImageData 
    {
        public byte[] Buffer { get; }
        public int Width { get; }
        public int Height { get; }
        public MyCamera.MvGvspPixelType PixelType { get; }
        public nint pData {  get; }

        public ImageData(byte[] buffer, int width, int height, MyCamera.MvGvspPixelType pixelType, nint pData)
        {
            this.Buffer = buffer;
            this.Width = width;
            this.Height = height;
            this.PixelType = pixelType;
            this.pData = pData;
        }
    }
}
