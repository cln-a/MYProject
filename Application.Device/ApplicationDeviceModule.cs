
namespace Application.Device
{
    public class ApplicationDeviceModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<DeviceView>();
        }
    }
}
