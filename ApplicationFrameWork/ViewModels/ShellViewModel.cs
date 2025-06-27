using Application.Common;
using Application.Login;
using Application.Login.Views;
using Application.Main.Views;
using Application.UI.Dialog;

namespace ApplicationFrameWork.ViewModels;

public class ShellViewModel : BindableBase
{
    private readonly IModuleManager _moduleManager;
    private readonly IRegionManager _regionManager;
    private readonly IEventAggregator _eventAggregator;
    private DelegateCommand _shellLoadCommand = null!;

    public IModuleManager ModuleManager => _moduleManager;
    public IRegionManager RegionManager => _regionManager;
    public IEventAggregator EventAggregator => _eventAggregator;
    public DelegateCommand ShellLoadCommand => _shellLoadCommand ??= new DelegateCommand(() =>
    {
        this.ModuleManager.LoadModule(ConstName.ApplicationLoginModule);
        this.RegionManager.RequestNavigate(ConstName.MainRegion, nameof(LoginView));
        this.EventAggregator.GetEvent<LoginInEvents>().Subscribe(() =>
        {
            //加载主界面模块
            ModuleManager.LoadModule(ConstName.ApplicationMainModule);
            //导航到主区域
            RegionManager.RequestNavigate(ConstName.MainRegion, nameof(MainView));
            PopupBox.Show("Welcome to ApplicationFrameWork");
        },ThreadOption.UIThread);
    });

    public ShellViewModel(IModuleManager moduleManager, IRegionManager regionManager, IEventAggregator eventAggregator)
    {
        this._moduleManager = moduleManager;
        this._regionManager = regionManager;
        this._eventAggregator = eventAggregator;
    }
}