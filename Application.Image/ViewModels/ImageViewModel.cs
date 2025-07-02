using Application.Camera;
using Application.Common.Helper;
using Microsoft.Extensions.Logging;
using System.Windows.Media;
using Application.Common;

namespace Application.Image
{
    public class ImageViewModel : BindableBase
    {
        private readonly ICameraController _cameraController;
        private readonly ILogger _logger;
        private readonly IEventAggregator _eventAggregator;
        private ImageSource _imagesource;

        public ICameraController CameraController => _cameraController;
        public ILogger Logger => _logger;
        public IEventAggregator EventAggregator => _eventAggregator;
        public ImageSource ImageSource
        {
            get => _imagesource;
            set => SetProperty(ref _imagesource, value);
        }
        public ICamera? HIKCamera { get; private set; }

        public ImageViewModel(ICameraController _cameraController, ILogger logger, IEventAggregator eventAggregator) 
        {
            this._cameraController = _cameraController;
            this._logger = logger;
            this._eventAggregator = eventAggregator;

            EventAggregator.GetEvent<CameraConnectEvent>().Subscribe(OnCameraConnected);

            CameraController.InitializeAllCameras();
        }

        private void OnCameraConnected()
        {
            HIKCamera = _cameraController.GetBySerial(ConstName.OptCameraName);
            if (HIKCamera != null) 
                EventAggregator.GetEvent<CameraFrameEvent>().Subscribe(HIKCamera_CameraFrameReceived);
        }

        private void HIKCamera_CameraFrameReceived(CameraFrameEventArgs e)
        {
            Logger.LogDebug($"Received image: {e.Width}x{e.Height}, {e.PixelType}, size={e.Buffer.Length}");

            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                ImageSource = ImageConvertHelper.ConvertToBitmapSource(e.Buffer, e.Width, e.Height, e.PixelType);
            });
        }
    }
}
