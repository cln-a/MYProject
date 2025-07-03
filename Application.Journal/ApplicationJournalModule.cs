
namespace Application.Journal
{
    public class ApplicationJournalModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<JournalView>();
        }
    }
}
