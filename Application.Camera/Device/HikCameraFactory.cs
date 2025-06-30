using Microsoft.Extensions.Logging;
using MvCamCtrl.NET;

namespace Application.Camera
{
    public class HikCameraFactory : ICameraFactory
    {
        private readonly ILogger _logger;

        public ILogger Logger => _logger;

        public HikCameraFactory(ILogger logger) => _logger = logger;

        public ICamera Create(MyCamera.MV_CC_DEVICE_INFO device) => new HIKCamera(device, _logger);
    }
}
