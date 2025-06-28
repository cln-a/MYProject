using Application.Common;
using Application.Image.Views;
using Prism.Navigation.Regions;

namespace Application.Image
{
    public class ApplicationImageModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider.Resolve<IRegionManager>().RegisterViewWithRegion<IamgeView>(ConstName.MainViewRegion);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<IamgeView>();
        }
    }
}
