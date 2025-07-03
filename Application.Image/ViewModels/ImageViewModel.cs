using Application.Camera;
using Application.Common.Helper;
using Microsoft.Extensions.Logging;
using System.Windows.Media;
using Application.Common;
using HalconDotNet;
using System.Web;
using System.Diagnostics;

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
        public HWindowControlWPF? HalconControl { get; set; } 

        public ImageViewModel(ICameraController _cameraController, ILogger logger, IEventAggregator eventAggregator) 
        {
            this._cameraController = _cameraController;
            this._logger = logger;
            this._eventAggregator = eventAggregator;

            EventAggregator.GetEvent<CameraConnectEvent>().Subscribe(OnCameraConnected);
            EventAggregator.GetEvent<ImageProcessedEvent>().Subscribe(OnImageProcessed);

            CameraController.InitializeAllCameras();
        }

        private void OnImageProcessed(HObject hobject)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                try
                {
                    HOperatorSet.GetImageSize(hobject, out HTuple width, out HTuple height);
                    HalconControl?.HalconWindow.SetPart(0, 0, width.D, height.D);
                    HalconControl?.HalconWindow.ClearWindow();
                    HalconControl?.HalconWindow.DispObj(hobject);
                }
                catch (Exception ex)
                {
                    Logger.LogError(ex, "Halcon 显示图像失败");
                }
            });
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
