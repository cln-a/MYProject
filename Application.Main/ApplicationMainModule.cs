using Application.Common;
using Application.Main.Views;

namespace Application.Main
{
    public class ApplicationMainModule : IModule
    {
        private readonly IModuleManager _moduleManager;

        public ApplicationMainModule(IModuleManager moduleManager)
        => _moduleManager = moduleManager;

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _moduleManager.LoadModule(ConstName.ApplicationImageModule);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainView>();
        }
    }
}
