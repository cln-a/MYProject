using Application.Common;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Application.Camera
{
    public class HIKCameraController : ICameraController
    {
        private readonly ILogger _logger;
        private readonly ICameraManager _cameraManager;
        private readonly ICameraFactory _cameraFactory;
        private readonly List<ICamera> _cameras = new();
        private readonly Dictionary<string, ICamera> _serialToCamera = new();
        private readonly System.Timers.Timer _retryTimer;

        public ILogger Logger => _logger;
        public IReadOnlyList<ICamera> Cameras => _cameras;

        public HIKCameraController(ILogger logger,ICameraManager cameraManager, ICameraFactory cameraFactory)
        {
            _logger = logger;
            _cameraManager = cameraManager;
            _cameraFactory = cameraFactory;

            _retryTimer = new System.Timers.Timer(1000);
            _retryTimer.Elapsed += _retryTimer_Elapsed;
            _retryTimer.AutoReset = true;   
        }

        private void _retryTimer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            if (TryInitializeCameras())
            {
                Logger.LogDebug("相机连接成功，停止重连定时器");
                _retryTimer?.Stop();    
            }
        }

        public void InitializeAllCameras()
        {
            //初次尝试连接相机
            if (!TryInitializeCameras())
            {
                Logger.LogDebug("相机初次连接失败，启动重连定时器");
                _retryTimer?.Start();
            }
        }

        private bool TryInitializeCameras()
        {
            //枚举设备
            var devices = _cameraManager.EnumerateDevices();
            if(devices.Count == 0)
            {
                Logger.LogDebug("未检测到任何相机设备");
                return false;
            }

            bool success = true;
            foreach (var device in devices) 
            {
                if (_serialToCamera.ContainsKey(ConstName.OptCameraName))
                    continue;

                try
                {
                    var camera = _cameraFactory.Create(device);
                    if (!camera.Open() || !(camera.StartGrabbing()))
                    {
                        Logger.LogDebug($"连接相机失败：{camera.SerialNumber}");
                        success = false;
                        continue;
                    }

                    _cameras.Add(camera);
                    _serialToCamera[camera.SerialNumber] = camera;
                    camera.State = CommunicationStateEnum.Connected;
                }
                catch (Exception ex) 
                {
                    Logger.LogDebug($"初始化相机异常：{Encoding.ASCII.GetString(device.SpecialInfo.stGigEInfo).TrimEnd('\0')}");
                    success = false;
                }
            }

            return success && _cameras.Count > 0;
        }

        public void StopAllCameras() 
        {
            foreach (var camera in _cameras) 
                camera?.Close();
        }

        public ICamera? GetBySerial(string serial) => _serialToCamera.TryGetValue(serial, out var cam) ? cam : null;
    }
}
