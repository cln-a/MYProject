using Application.Common;
using Application.Main.Views;

namespace Application.Main
{
    [Module(ModuleName = ConstName.ApplicationMainModule,OnDemand = true)]
    public class ApplicationMainModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainView>();
        }
    }
}
