namespace ApplicationFrameWork.ViewModels;

public class ShellViewModel : BindableBase
{

    private readonly IModuleManager _moduleManager;
    private readonly IRegionManager _regionManager;

    public IModuleManager ModuleManager => _moduleManager;
    public IRegionManager RegionManager => _regionManager;

    public ShellViewModel(IModuleManager moduleManager, IRegionManager regionManager)
    {
        this._moduleManager = moduleManager;
        this._regionManager = regionManager;
    }
}