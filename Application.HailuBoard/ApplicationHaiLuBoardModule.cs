
namespace Application.HailuBoard
{
    public class ApplicationHaiLuBoardModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            IUnityContainer unityContainer = containerRegistry.GetContainer();
            unityContainer.RegisterSingleton<ViewModelDtoMapepr>();

            containerRegistry.RegisterForNavigation<PartsInfoView>();
            containerRegistry.RegisterForNavigation<SinglePartInfoView>();

            containerRegistry.RegisterDialog<PartsInfoEditView, PartsInfoEditViewModel>("PartsInfoEditDialog");
        }
    }
}
