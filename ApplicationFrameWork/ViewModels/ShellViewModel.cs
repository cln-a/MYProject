using Application.Common;
using Application.Login.Views;

namespace ApplicationFrameWork.ViewModels;

public class ShellViewModel : BindableBase
{

    private readonly IModuleManager _moduleManager;
    private readonly IRegionManager _regionManager;
    private DelegateCommand _shellLoadCommand;

    public IModuleManager ModuleManager => _moduleManager;
    public IRegionManager RegionManager => _regionManager;
    public DelegateCommand ShellLoadCommand => _shellLoadCommand ??= new DelegateCommand(() =>
    {
        this.ModuleManager.LoadModule(ConstName.ApplicationLoginModule);
        this.RegionManager.RequestNavigate(ConstName.MainRegion, nameof(LoginView));
    });

    public ShellViewModel(IModuleManager moduleManager, IRegionManager regionManager)
    {
        this._moduleManager = moduleManager;
        this._regionManager = regionManager;
    }
}