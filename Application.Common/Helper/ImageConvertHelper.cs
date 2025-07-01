using System.Windows.Media.Imaging;
using System.Windows.Media;
using MvCamCtrl.NET;

namespace Application.Common.Helper
{
    public static class ImageConvertHelper
    {
        public static BitmapSource ConvertToBitmapSource(byte[] buffer, int width, int height, MyCamera.MvGvspPixelType pixelType)
        {
            PixelFormat pixelFormat;
            int stride;

            // 支持 Mono8 与 BGR8 示例（可扩展 Bayer、RGB16 等格式）
            switch (pixelType)
            {
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8:
                    pixelFormat = PixelFormats.Gray8;
                    stride = width;
                    break;

                case MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed:
                    pixelFormat = PixelFormats.Rgb24;
                    stride = width * 3;
                    break;

                default:
                    throw new NotSupportedException($"不支持的像素格式: {pixelType}");
            }

            return BitmapSource.Create(width, height, 96, 96, pixelFormat, null, buffer, stride);
        }
    }
}
