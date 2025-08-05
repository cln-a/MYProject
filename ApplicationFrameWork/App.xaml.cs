using Application.Common;
using Application.Communicate;
using Application.DAL;
using Application.Device;
using Application.Dialog;
using Application.Hailu;
using Application.HailuBoard;
using Application.ImportUtil;
using Application.Journal;
using Application.Logger;
using Application.Login;
using Application.Main;
using Application.Mapper;
using Application.Modbus;
using Application.Russia;
using Application.RussiaUI;
using Application.UI;
using ApplicationFrameWork.ViewModels;
using ApplicationFrameWork.Views;
using CommonServiceLocator;
using ControlzEx.Theming;
using Microsoft.Extensions.Logging;
using Prism.Common;
using System.Windows;
using System.Windows.Media;
using Unity.ServiceLocation;

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

        protected override void RegisterTypes(IContainerRegistry containerRegistry) { }

        protected override Window CreateShell() => null!;

        protected override void Initialize()
        {
            base.Initialize();
            DisPlayShellView();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
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

            #region Russia
            moduleCatalog.AddModule<LoggerModule>();
            moduleCatalog.AddModule<DALModule>();
            moduleCatalog.AddModule<ModbusModule>();
            moduleCatalog.AddModule<ApplicationMapperModule>();
            moduleCatalog.AddModule<UIModule>();
            moduleCatalog.AddModule<ApplicationMainModule>();
            moduleCatalog.AddModule<ApplicationDeviceModule>();
            moduleCatalog.AddModule<ApplicationCommunicateModule>();
            moduleCatalog.AddModule<ApplicationJournalModule>();
            moduleCatalog.AddModule<ApplicationDialogModule>();
            moduleCatalog.AddModule<ApplicationRussiaModule>();
            moduleCatalog.AddModule<ApplicationRussiaUIModule>();
            #endregion

            #region HaiLu
            //moduleCatalog.AddModule<LoggerModule>();
            //moduleCatalog.AddModule<DALModule>();
            //moduleCatalog.AddModule<ModbusModule>();
            //moduleCatalog.AddModule<ApplicationMapperModule>();
            //moduleCatalog.AddModule<UIModule>();
            //moduleCatalog.AddModule<ApplicationMainModule>();
            //moduleCatalog.AddModule<ApplicationDeviceModule>();
            //moduleCatalog.AddModule<ApplicationCommunicateModule>();
            //moduleCatalog.AddModule<ApplicationJournalModule>();
            //moduleCatalog.AddModule<ApplicationDialogModule>();
            //moduleCatalog.AddModule<ApplicationHailuModule>();
            //moduleCatalog.AddModule<ApplicationHaiLuBoardModule>();
            //moduleCatalog.AddModule<ApplicationFrameImportUtilModule>();
            #endregion
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
        }

        protected override void OnExit(ExitEventArgs e)
        {
            var logger = ServiceLocator.Current.GetInstance<ILogger>();
            var clients = ServiceLocator.Current.GetAllInstances<ModbusClient>();
            var heartBeatMasters = ServiceLocator.Current.GetAllInstances<HeartBeatMaster>();
            //var importutil = ServiceLocator.Current.GetInstance<IImportUtil>();

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

            //importutil.StopImport();
        }
    }
}
