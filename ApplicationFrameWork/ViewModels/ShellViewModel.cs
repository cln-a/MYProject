using Application.Common;
using Application.IDAL;
using Application.Main;

namespace ApplicationFrameWork.ViewModels;

public class ShellViewModel : BindableBase
{
    private readonly IModuleManager _moduleManager;
    private readonly IRegionManager _regionManager;
    private readonly IEventAggregator _eventAggregator;
    private readonly IPartsInfoDAL _partsInfoDAL;
    private readonly ISinglePartInfoDAL _singlePartInfoDAL;
    private DelegateCommand _shellLoadCommand = null!;

    public IModuleManager ModuleManager => _moduleManager;
    public IRegionManager RegionManager => _regionManager;
    public IEventAggregator EventAggregator => _eventAggregator;
    public DelegateCommand ShellLoadCommand => _shellLoadCommand ??= new DelegateCommand(() =>
    {
        ModuleManager.LoadModule(ConstName.ApplicationMainModule);
        RegionManager.RequestNavigate(ConstName.MainRegion, nameof(MainView));
    });

    public ShellViewModel(IModuleManager moduleManager, 
        IRegionManager regionManager, 
        IEventAggregator eventAggregator,
        IPartsInfoDAL partsInfoDAL,
        ISinglePartInfoDAL singlePartInfoDAL)
    {
        this._moduleManager = moduleManager;
        this._regionManager = regionManager;
        this._eventAggregator = eventAggregator;
        this._partsInfoDAL = partsInfoDAL;
        this._singlePartInfoDAL = singlePartInfoDAL;
    }
}