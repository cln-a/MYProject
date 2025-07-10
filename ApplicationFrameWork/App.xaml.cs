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
using CommonServiceLocator;
using Unity.ServiceLocation;
using ControlzEx.Theming;
using System.Windows.Media;
using Application.Communicate;
using Application.Journal;
using Application.Device;
using Application.Mapper;
using Application.Modbus;
using Microsoft.Extensions.Logging;
using Application.SemiAuto;
using Application.GeneralControl;
using Application.Dialog;

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
            moduleCatalog.AddModule<LoggerModule>();
            moduleCatalog.AddModule<ApplicationMapperModule>();
            moduleCatalog.AddModule<SemiAutoModule>();
            moduleCatalog.AddModule<DALModule>();
            moduleCatalog.AddModule<ModbusModule>();
            moduleCatalog.AddModule<UIModule>();
            moduleCatalog.AddModule<ApplicationLoginModule>();
            moduleCatalog.AddModule<ApplicationMainModule>(ConstName.ApplicationMainModule, InitializationMode.OnDemand);
            moduleCatalog.AddModule<ApplicationGeneralControlModule>();
            moduleCatalog.AddModule<ApplicationDeviceModule>();
            moduleCatalog.AddModule<ApplicationCommunicateModule>();
            moduleCatalog.AddModule<ApplicationJournalModule>();
            moduleCatalog.AddModule<ApplicationDialogModule>();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
            ViewModelLocationProvider.Register<ShellView, ShellViewModel>();
            ViewModelLocationProvider.Register<LoginView, LoginViewModel>();
            ViewModelLocationProvider.Register<MainView, MainViewModel>();
            ViewModelLocationProvider.Register<ModbusMonitorView, ModbusMonitorViewModel>();
            ViewModelLocationProvider.Register<JournalView, JournalViewModel>();
            ViewModelLocationProvider.Register<ModbusDeviceView, ModbusDeviceViewModel>();
            ViewModelLocationProvider.Register<GeneralControlView, GeneralControlViewModel>();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            var logger = ServiceLocator.Current.GetInstance<ILogger>();
            var clients = ServiceLocator.Current.GetAllInstances<ModbusClient>();

            foreach (var client in clients)
            {
                try
                {
                    client.Stop();
                }
                catch (Exception ex)
                {
                    logger.LogError( ex.Message);
                }
            }

            var heartBeatMasters = ServiceLocator.Current.GetAllInstances<HeartBeatMaster>();
            foreach (var master in heartBeatMasters)
            {
                try
                {
                    master.Stop();
                }
                catch (Exception ex)
                {
                    logger.LogError( ex.Message);
                }
            }
        }
    }
}
