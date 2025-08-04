
namespace Application.Russia
{
    public class ApplicationRussiaModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider.Resolve<IWorkStationManager>();
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            IUnityContainer unityContainer = containerRegistry.GetContainer();

            unityContainer.RegisterSingleton<IWorkStationManager, WorkStationManager>();
        }
    }
}
