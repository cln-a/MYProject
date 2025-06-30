using Microsoft.Extensions.Logging;

namespace Application.Camera
{
    public class HIKCameraController : ICameraController
    {
        private readonly ILogger _logger;
        private readonly ICameraManager _cameraManager;
        private readonly ICameraFactory _cameraFactory;
        private readonly List<ICamera> _cameras = new();
        private readonly Dictionary<string, ICamera> _serialToCamera = new();

        public ILogger Logger => _logger;
        public IReadOnlyList<ICamera> Cameras => _cameras;

        public HIKCameraController(ILogger logger,ICameraManager cameraManager, ICameraFactory cameraFactory)
        {
            _logger = logger;
            _cameraManager = cameraManager;
            _cameraFactory = cameraFactory;
        }

        public void InitializeAllCameras()
        {
            var devices = _cameraManager.EnumerateDevices();
            foreach (var device in devices)
            {
                var camera = _cameraFactory.Create(device);
                camera.Open();
                camera.StartGrabbing();

                _cameras.Add(camera);
                _serialToCamera[camera.SerialNumber] = camera;
            }
        }

        public ICamera? GetBySerial(string serial)
        {
            return _serialToCamera.TryGetValue(serial, out var cam) ? cam : null;
        }
    }
}
