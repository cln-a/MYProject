
namespace Application.RussiaUI
{
    public class ApplicationRussiaUIModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<GeneralControlView>();
            containerRegistry.RegisterForNavigation<RecordedDurationView>();
        }
    }
}
