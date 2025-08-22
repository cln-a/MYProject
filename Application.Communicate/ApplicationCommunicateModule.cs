namespace Application.Communicate
{
    public class ApplicationCommunicateModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ModbusMonitorView>();
            containerRegistry.RegisterForNavigation<S7netMonitorView>();
        }
    }
}