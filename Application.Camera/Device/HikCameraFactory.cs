using Microsoft.Extensions.Logging;
using MvCamCtrl.NET;

namespace Application.Camera
{
    public class HikCameraFactory : ICameraFactory
    {
        private readonly ILogger _logger;
        private readonly IEventAggregator _eventAggregator;

        public ILogger Logger => _logger;
        public IEventAggregator EventAggregator => _eventAggregator;

        public HikCameraFactory(ILogger logger, IEventAggregator eventAggregator)
        {
            this._logger = logger;  
            this._eventAggregator = eventAggregator;    
        }
        public ICamera Create(MyCamera.MV_CC_DEVICE_INFO device) 
            => new HIKCamera(device, _logger, _eventAggregator);
    }
}
