using Application.Common;

namespace Application.Camera
{
    public class ApplicationCameraModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var cameracontroller = containerProvider.Resolve<ICameraController>();
            cameracontroller.InitializeAllCameras();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ICameraManager, HIKCameraManager>();
            containerRegistry.RegisterSingleton<ICameraFactory, HikCameraFactory>();
            containerRegistry.RegisterSingleton<ICameraController, HIKCameraController>();
        }
    }
}
