using Application.IDAL;
using System.Collections.ObjectModel;

namespace Application.Main
{
    public class MainViewModel : BindableBase
    {
        private readonly ISystemMenuDAL _systemMenuDAL;
        private ObservableCollection<MenuBar>? _menuBars;
        private DelegateCommand? _loadMenuCommand;

        public ISystemMenuDAL SystemMenuDAL => _systemMenuDAL;
        public ObservableCollection<MenuBar>? MenuBars { get => _menuBars; set => SetProperty(ref _menuBars, value); }
        public DelegateCommand LoadMenuCommand => _loadMenuCommand ??= new DelegateCommand(LoadMenu);   

        public MainViewModel(ISystemMenuDAL systemMenuDAL)
        {
            this._systemMenuDAL = systemMenuDAL;
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
    }
}
