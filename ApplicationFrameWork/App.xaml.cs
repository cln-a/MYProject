using Application.DAL;
using Application.Login;
using Application.Login.Views;
using Application.Main;
using Application.Main.ViewModels;
using Application.Main.Views;
using ApplicationFrameWork.ViewModels;
using ApplicationFrameWork.Views;
using Prism.Common;
using System.Windows;
using Application.UI;
using Application.Common;
using Application.Logger;
using Application.Image;
using Application.Camera;

namespace ApplicationFrameWork
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        IContainerExtension _containerExtension;

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        protected override Window CreateShell() => null!;

        protected override void Initialize()
        {
            base.Initialize();
            DisPlayShellView();
        }

        private void DisPlayShellView()
        {
            _containerExtension = ContainerLocator.Current;
            var shell = Container.Resolve<ShellView>();
            if (shell != null)
            {
                MvvmHelpers.AutowireViewModel(shell);
                RegionManager.SetRegionManager(shell, _containerExtension.Resolve<IRegionManager>());
                RegionManager.UpdateRegions();
                InitializeShell(shell);
            }
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog
                .AddModule<LoggerModule>()
                .AddModule<DALModule>()
                .AddModule<UIModule>()
                .AddModule<ApplicationLoginModule>()
                .AddModule<ApplicationMainModule>(ConstName.ApplicationMainModule, InitializationMode.OnDemand)
                .AddModule<ApplicationImageModule>(ConstName.ApplicationImageModule, InitializationMode.OnDemand, ConstName.ApplicationMainModule)
                .AddModule<ApplicationCameraModule>();   
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            ViewModelLocationProvider.Register<ShellView, ShellViewModel>();
            ViewModelLocationProvider.Register<LoginView, LoginViewModel>();
            ViewModelLocationProvider.Register<MainView, MainViewModel>();
        }
    }
}
