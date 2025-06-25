using Application.Common;
using Application.Main.Views;

namespace Application.Main
{
    public class ApplicationMainModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            //把视图MainView注册到名为ConstName.MainRegion的区域中
            //当这个区域出现时，Prism会自动将MainView实例化并显示在该区域内
            //“被动式”导航，即视图会在区域初始化时自动出现，而不是显示调用导航方法
            containerProvider.Resolve<IRegionManager>().RegisterViewWithRegion<MainView>(ConstName.MainRegion);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainView>();
        }
    }
}
