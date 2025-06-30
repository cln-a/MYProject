using Application.Common;

namespace Application.Camera
{
    public class ApplicationCameraModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var controller = containerProvider.Resolve<ICameraController>();
            controller.InitializeAllCameras();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ICameraManager, HIKCameraManager>();
            containerRegistry.RegisterSingleton<ICameraFactory, HikCameraFactory>();
            containerRegistry.RegisterSingleton<ICameraController, HIKCameraController>();
        }
    }
}
