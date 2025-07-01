using Application.Camera;
using Application.Common.Helper;
using Microsoft.Extensions.Logging;
using System.Windows.Media;

namespace Application.Image
{
    public class ImageViewModel : BindableBase
    {
        private readonly ICameraController _cameraController;
        private readonly ICamera _hikcamera;
        private readonly ILogger _logger;
        private ImageSource _imagesource;

        public ICameraController CameraController => _cameraController;
        public ICamera HIKCamera => _hikcamera;
        public ILogger Logger => _logger;
        public ImageSource ImageSource
        {
            get => _imagesource;
            set => SetProperty(ref _imagesource, value);
        }

        public ImageViewModel(ICameraController _cameraController, ILogger logger) 
        {
            this._cameraController = _cameraController;
            this._hikcamera = CameraController.GetBySerial("OPT_Camera")!;
            this._logger = logger;

            this.HIKCamera.OnImageReceived += HIKCamera_OnImageReceived;
        }

        private void HIKCamera_OnImageReceived(object? sender, CameraFrameEventArgs e)
        {
            Logger.LogDebug($"Received image: {e.Width}x{e.Height}, {e.PixelType}, size={e.Buffer.Length}");

            System.Windows.Application.Current.Dispatcher.Invoke(() => 
            {
                ImageSource = ImageConvertHelper.ConvertToBitmapSource(e.Buffer, e.Width, e.Height, e.PixelType);
            });
        }
    }
}
