using Application.Common;

namespace Application.GeneralControl
{
    public class ApplicationGeneralControlModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider
                .Resolve<IRegionManager>()
                .RegisterViewWithRegion<GeneralControlView>(ConstName.MainViewRegion);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<GeneralControlView>();

            IUnityContainer unityContainer = containerRegistry.GetContainer();
            unityContainer.RegisterSingleton<GeneralControlModel>("GeneralControl");
        }
    }
}
