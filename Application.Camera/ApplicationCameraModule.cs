namespace Application.Camera
{
    public class ApplicationCameraModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ICameraManager, HIKCameraManager>();
            containerRegistry.RegisterSingleton<ICameraFactory, HikCameraFactory>();
            containerRegistry.RegisterSingleton<ICameraController, HIKCameraController>();
        }
    }
}
