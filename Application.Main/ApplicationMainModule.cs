using Application.Common;

namespace Application.Main
{
    public class ApplicationMainModule : IModule
    {
        private readonly IModuleManager _moduleManager;

        public IModuleManager ModuleManager => _moduleManager;

        public ApplicationMainModule(IModuleManager moduleManager)
        => this._moduleManager = moduleManager;

        public void OnInitialized(IContainerProvider containerProvider)
        {
            ModuleManager.LoadModule(ConstName.ApplicationImageModule);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainView>();
        }
    }
}
