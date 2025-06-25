using Application.Common;
using Application.Login.Views;

namespace Application.Login
{
    /// <summary>
    /// 登录模块-按需延迟加载
    /// </summary>
    [Module(ModuleName = ConstName.ApplicationLoginModule,OnDemand = true)]
    public class ApplicationLoginModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<LoginView>();
        }
    }
}
