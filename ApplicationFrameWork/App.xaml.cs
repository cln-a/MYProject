using Application.DAL;
using Application.Login;
using Application.Main;
using ApplicationFrameWork.ViewModels;
using ApplicationFrameWork.Views;
using Prism.Common;
using System.Windows;
using Application.UI;
using Application.Common;
using Application.Logger;
using Application.Image;
using Application.Camera;
using CommonServiceLocator;
using Unity.ServiceLocation;
using Application.ImageProcess;
using ControlzEx.Theming;
using System.Windows.Media;
using Application.Communicate;
using Application.ArtificialIntelligence;

namespace ApplicationFrameWork
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        IContainerExtension _containerExtension;

        protected override IContainerExtension CreateContainerExtension()
        {
            IUnityContainer unityContainer = new UnityContainer();
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(unityContainer));
            return new UnityContainerExtension(unityContainer);
        }

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
            ThemeManager.Current.ChangeTheme(this, ThemeManager.Current.AddTheme(RuntimeThemeGenerator.Current.GenerateRuntimeTheme("Light", Colors.AliceBlue)!));

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
                .AddModule<ImageProcessModule>()
                .AddModule<ApplicationCameraModule>()
                .AddModule<ApplicationLoginModule>()
                .AddModule<ApplicationMainModule>(ConstName.ApplicationMainModule, InitializationMode.OnDemand)
                .AddModule<ApplicationImageModule>(ConstName.ApplicationImageModule, InitializationMode.OnDemand, ConstName.ApplicationMainModule);
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            ViewModelLocationProvider.Register<ShellView, ShellViewModel>();
            ViewModelLocationProvider.Register<LoginView, LoginViewModel>();
            ViewModelLocationProvider.Register<MainView, MainViewModel>();
            ViewModelLocationProvider.Register<ImageView,ImageViewModel>();
            ViewModelLocationProvider.Register<CommunicateView, CommunicateViewModel>();
            ViewModelLocationProvider.Register<ArtificialIntelligenceView, ArtificialIntelligenceViewModel>();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            var consumer = ServiceLocator.Current.GetInstance<IConsumer>();
            consumer.StopConsum();

            var cameracontroller = ServiceLocator.Current.GetInstance<ICameraController>();
            cameracontroller.StopAllCameras();  
        }
    }
}
