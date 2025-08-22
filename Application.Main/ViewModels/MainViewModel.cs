using Application.Common;
using Application.IDAL;
using Application.Modbus;
using Application.S7net;
using Application.UI;
using CommonServiceLocator;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace Application.Main
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ISystemMenuDAL _systemMenuDAL;
        private readonly IRegionManager _regionManager;
        private readonly ILanguageManager _languageManager;
        private readonly IEventAggregator _eventAggregator;
        private ObservableCollection<StateBar>? _deviceStateBar;
        private ObservableCollection<MenuBar>? _menuBars;
        private DelegateCommand? _loadMenuCommand;
        private DelegateCommand? _loadDeviceStateCommand;
        private DelegateCommand<object> _navigateCommand;
        private DelegateCommand<string>? _checkCommand;

        public ISystemMenuDAL SystemMenuDAL => _systemMenuDAL;
        public IRegionManager RegionManager => _regionManager;
        public ILanguageManager LanguageManager => _languageManager;
        public IEventAggregator EventAggregator => _eventAggregator;
        public ObservableCollection<MenuBar>? MenuBars { get => _menuBars; set => SetProperty(ref _menuBars, value); }
        public ObservableCollection<StateBar> StateBars { get => _deviceStateBar; set => SetProperty(ref _deviceStateBar, value);  }
        public DelegateCommand LoadMenuCommand => _loadMenuCommand ??= new DelegateCommand(LoadMenu);
        public DelegateCommand LoadDeviceStateCommand => _loadDeviceStateCommand ??= new DelegateCommand(LoadDeviceState);
        public DelegateCommand<object> NavigateCommand => _navigateCommand ??= new DelegateCommand<object>(NavigateCmd);

        public MainViewModel(ISystemMenuDAL systemMenuDAL, 
            IRegionManager regionManager,
            ILanguageManager languageManager, 
            IEventAggregator eventAggregator)
        {
            this._systemMenuDAL = systemMenuDAL;
            this._regionManager = regionManager;
            this._languageManager = languageManager;
            this._eventAggregator = eventAggregator;
        }

        private void LoadMenu()
        {
            MenuBars = [];
            var menus = SystemMenuDAL.GetAllEnabled().OrderBy(x => x.MenuSort);
            foreach (var menu in menus) 
            {
                var menubar = new MenuBar()
                {
                    MenuNames = new Dictionary<LanguageType, string>
                    {
                        { LanguageType.CN, menu.MenuNameCN! },
                        { LanguageType.US, menu.MenuNameUS! },
                        { LanguageType.Russia, menu.MenuNameRussia! }
                    },
                    Icon = menu.MenuIcon,
                    View = menu.MenuView,
                };
                menubar.MenuName = menubar.MenuNames[LanguageManager.CurrentLanguageType];
                MenuBars.Add(menubar);
            }

            LanguageManager.SetLanguage(LanguageType.CN);

            RegionManager.RequestNavigate(ConstName.MainViewRegion, "ModbusMonitorView");
        }

        public DelegateCommand<string> CheckCommand => _checkCommand ??= new DelegateCommand<string>(context =>
        {
            switch (context.ToString())
            {
                case "简体中文":
                    LanguageManager.SetLanguage(LanguageType.CN);
                    break;
                case "English":
                    LanguageManager.SetLanguage(LanguageType.US);
                    break;
                case "Россия":
                    LanguageManager.SetLanguage(LanguageType.Russia);
                    break;
                default:
                    break;
            }
            foreach (var menubar in MenuBars!)
            {
                menubar.MenuName = menubar.MenuNames[LanguageManager.CurrentLanguageType];
            }
            EventAggregator.GetEvent<LanguageChangedEvent>().Publish();
        });

        private void NavigateCmd(object obj)
        {
            if (obj is MenuBar menubar && !string.IsNullOrWhiteSpace(menubar.View))
                RegionManager.RequestNavigate(ConstName.MainViewRegion, menubar.View);
        }

        private void LoadDeviceState()
        {
            StateBars = new ObservableCollection<StateBar>();
            var plcStates = ServiceLocator.Current.GetAllInstances<ICommunicationStateMachine>();
            var s7Clients = ServiceLocator.Current.GetAllInstances<S7netClient>();

            try
            {
                foreach (var plcState in plcStates)
                {
                    if (plcState is PLCState state)
                    {
                        state.ConnectEvent += StateMachine_ConnectEvent;
                        state.DisConnectEvent += StateMachine_DisConnectEvent;
                        StateBars.Add(new()
                        {
                            Tag = state,
                            StateImageSource = state.IsConnected ? "../Image/connection.png" : "../Image/disconnection.png",
                            DeviceName = state.Description
                        });
                    }
                }

                foreach (var client in s7Clients)
                {
                    if (client != null)
                    {
                        client.ConnectEvent += StateMachine_ConnectEvent;
                        client.DisConnectEvent += StateMachine_DisConnectEvent;
                        StateBars.Add(new()
                        {
                            Tag = client,
                            StateImageSource = client.Connected ? "../Image/connection.png" : "../Image/disconnection.png",
                            DeviceName = client.DeviceName
                        });
                    }
                }
            }
            catch(Exception ex)
            {
                Logger.LogDebug(ex.Message);
            }
        }

        private void StateMachine_DisConnectEvent(object? sender, EventArgs e)
        {
            try
            {
                if (sender is ICommunicationStateMachine state)
                {
                    Dispatcher.CurrentDispatcher.Invoke(new System.Action(() =>
                    {
                        StateBar? deviceStateBar = StateBars.Where(x => x.Tag is ICommunicationStateMachine y && y.Name == state.Name).FirstOrDefault();
                        if (deviceStateBar != null)
                        {
                            deviceStateBar.StateImageSource = "../Image/disconnection.png";
                        }
                    }));
                }

                if (sender is S7netClient client)
                {
                    Dispatcher.CurrentDispatcher.Invoke(new System.Action(() =>
                    {
                        StateBar? deviceStateBar = StateBars.Where(x => x.Tag is S7netClient y && y.DeviceModel.DeviceUri == client.DeviceModel.DeviceUri).FirstOrDefault();
                        if (deviceStateBar != null)
                        {
                            deviceStateBar.StateImageSource = "../Image/disconnection.png";
                        }
                    }));
                }
            }
            catch(Exception ex)
            {
                Logger.LogDebug(ex.Message);
            }
        }

        private void StateMachine_ConnectEvent(object? sender, EventArgs e)
        {
            try
            {
                if (sender is ICommunicationStateMachine stateMachine)
                {
                    Dispatcher.CurrentDispatcher.Invoke(new System.Action(() =>
                    {
                        StateBar? deviceStateBar = StateBars.Where(x => x.Tag is ICommunicationStateMachine y && y.Name == stateMachine.Name).FirstOrDefault();
                        if (deviceStateBar != null)
                        {
                            deviceStateBar.StateImageSource = "../Image/connection.png";
                        }
                    }));
                }

                if (sender is S7netClient client)
                {
                    Dispatcher.CurrentDispatcher.Invoke(new System.Action(() =>
                    {
                        StateBar? deviceStateBar = StateBars.Where(x => x.Tag is S7netClient y && y.DeviceModel.DeviceUri == client.DeviceModel.DeviceUri).FirstOrDefault();
                        if (deviceStateBar != null)
                        {
                            deviceStateBar.StateImageSource = "../Image/connection.png";
                        }
                    }));
                }
            }
            catch (Exception ex)
            {
                Logger.LogDebug(ex.Message);
            }
        }
    }
}
