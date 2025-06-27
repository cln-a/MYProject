using Application.Common;

namespace Application.Image
{
    public class ApplicationImageModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider.Resolve<IRegionManager>()
                .RegisterViewWithRegion<ImageView>(ConstName.MainViewRegion);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ImageView>();
        }
    }
}
