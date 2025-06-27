using Application.Common;

namespace Application.UI
{
    public class UIModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            IUnityContainer unityContainer = containerRegistry.GetContainer();
            unityContainer.RegisterSingleton<ILanguageManager, LanguageManager>();
        }
    }
}
