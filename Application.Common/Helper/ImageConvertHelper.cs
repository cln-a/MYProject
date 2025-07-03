using System.Windows.Media.Imaging;
using System.Windows.Media;
using MvCamCtrl.NET;
using static MvCamCtrl.NET.MyCamera;
using HalconDotNet;

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

        public static HObject ConvertToHalconImage(int width, int height, MvGvspPixelType pixelType,nint pData)
        {
            HObject image;

            switch (pixelType)
            {
                case MvGvspPixelType.PixelType_Gvsp_Mono8:
                    // 单通道灰度图像
                    HOperatorSet.GenImage1(out image, "byte", width, height, pData);
                    break;

                case MvGvspPixelType.PixelType_Gvsp_RGB8_Packed:
                    // 彩色图像，3通道打包（RGBRGB...）
                    HOperatorSet.GenImageInterleaved(out image, pData, "rgb", width, height,
                        -1, "byte", width, height, 0, 0, -1, 0);
                    break;
                    
                default:
                    throw new NotSupportedException($"暂不支持该像素格式: {pixelType}");
            }

            return image;
        }
    }
}
