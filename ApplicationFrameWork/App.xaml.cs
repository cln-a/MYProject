using Application.Common;
using Application.DAL;
using Application.Main;
using ApplicationFrameWork.Views;
using Prism.Common;
using System.Windows;

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
            moduleCatalog.AddModule<DALModule>();
            moduleCatalog.AddModule<ApplicationMainModule>();
        }
    }
}
