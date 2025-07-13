using Application.Common;
using Application.Login;
using Application.Main;
using Application.UI.Dialog;

namespace ApplicationFrameWork.ViewModels;

public class ShellViewModel : BindableBase
{
    private readonly IModuleManager _moduleManager;
    private readonly IRegionManager _regionManager;
    private readonly IEventAggregator _eventAggregator;
    private readonly ILanguageManager _languageManager;
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
            ModuleManager.LoadModule(ConstName.ApplicationMainModule);
            RegionManager.RequestNavigate(ConstName.MainRegion, nameof(MainView));
            PopupBox.Show(_languageManager["Welcome to ApplicationFrameWork"]);
        },ThreadOption.UIThread);
    });

    public ShellViewModel(IModuleManager moduleManager, IRegionManager regionManager, IEventAggregator eventAggregator, ILanguageManager languageManager)
    {
        this._moduleManager = moduleManager;
        this._regionManager = regionManager;
        this._eventAggregator = eventAggregator;
        this._languageManager = languageManager;
    }
}