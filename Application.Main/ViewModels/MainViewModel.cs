using Application.Common;
using Application.IDAL;
using System.Collections.ObjectModel;

namespace Application.Main
{
    public class MainViewModel : BindableBase
    {
        private readonly ISystemMenuDAL _systemMenuDAL;
        private readonly IRegionManager _regionManager;
        private ObservableCollection<MenuBar>? _menuBars;
        private DelegateCommand? _loadMenuCommand;
        private DelegateCommand<object> _navigateCommand;

        public ISystemMenuDAL SystemMenuDAL => _systemMenuDAL;
        public IRegionManager RegionManager => _regionManager;
        public ObservableCollection<MenuBar>? MenuBars { get => _menuBars; set => SetProperty(ref _menuBars, value); }
        public DelegateCommand LoadMenuCommand => _loadMenuCommand ??= new DelegateCommand(LoadMenu);
        public DelegateCommand<object> NavigateCommand => _navigateCommand ??= new DelegateCommand<object>(NavigateCmd);

        public MainViewModel(ISystemMenuDAL systemMenuDAL, IRegionManager regionManager)
        {
            this._systemMenuDAL = systemMenuDAL;
            this._regionManager = regionManager;
        }

        private void LoadMenu()
        {
            MenuBars = [];
            var menus = SystemMenuDAL.GetAllEnabled().OrderBy(x => x.MenuSort);
            foreach (var menu in menus) 
            {
                var menubar = new MenuBar()
                {
                    MenuName = menu.MenuName,
                    Icon = menu.MenuIcon,
                    View = menu.MenuView,
                };
                MenuBars.Add(menubar);
            }
        }

        private void NavigateCmd(object obj)
        {
            if (obj is MenuBar menubar && !string.IsNullOrWhiteSpace(menubar.View))
                RegionManager.RequestNavigate(ConstName.MainViewRegion, menubar.View);
        }
    }
}
