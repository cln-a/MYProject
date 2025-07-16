
namespace Application.HailuBoard
{
    public class ApplicationHaiLuBoardModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<PartsInfoView>();
            containerRegistry.RegisterForNavigation<SinglePartInfoView>();
        }
    }
}
