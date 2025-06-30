using Application.Camera;
using MvCamCtrl.NET;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Application.Image
{
    public class ImageViewModel : BindableBase
    {
        private readonly ICameraController _cameraController;
        private readonly ICamera _hikcamera;
        private ImageSource _imagesource;

        public ICameraController CameraController => _cameraController;
        public ICamera HIKCamera => _hikcamera;
        public ImageSource ImageSource
        {
            get => _imagesource;
            set => SetProperty(ref _imagesource, value);
        }

        public ImageViewModel(ICameraController _cameraController) 
        {
            this._cameraController = _cameraController;
            this._hikcamera = CameraController.GetBySerial("OPT_Camera")!;

            this.HIKCamera.OnImageReceived += HIKCamera_OnImageReceived;
        }

        private void HIKCamera_OnImageReceived(object? sender, CameraFrameEventArgs e)
        {
            
        }
    }
}
