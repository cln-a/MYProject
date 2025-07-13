using Application.Common;
using Application.IDAL;
using System.Collections.ObjectModel;

namespace Application.Main
{
    public class MainViewModel : BindableBase
    {
        private readonly ISystemMenuDAL _systemMenuDAL;
        private readonly IRegionManager _regionManager;
        private readonly ILanguageManager _languageManager;
        private ObservableCollection<MenuBar>? _menuBars;
        private DelegateCommand? _loadMenuCommand;
        private DelegateCommand<object> _navigateCommand;
        private DelegateCommand<string>? _checkCommand;

        public ISystemMenuDAL SystemMenuDAL => _systemMenuDAL;
        public IRegionManager RegionManager => _regionManager;
        public ILanguageManager LanguageManager => _languageManager;
        public ObservableCollection<MenuBar>? MenuBars { get => _menuBars; set => SetProperty(ref _menuBars, value); }
        public DelegateCommand LoadMenuCommand => _loadMenuCommand ??= new DelegateCommand(LoadMenu);
        public DelegateCommand<object> NavigateCommand => _navigateCommand ??= new DelegateCommand<object>(NavigateCmd);

        public MainViewModel(ISystemMenuDAL systemMenuDAL, IRegionManager regionManager, ILanguageManager languageManager)
        {
            this._systemMenuDAL = systemMenuDAL;
            this._regionManager = regionManager;
            this._languageManager = languageManager;
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
        });

        private void NavigateCmd(object obj)
        {
            if (obj is MenuBar menubar && !string.IsNullOrWhiteSpace(menubar.View))
                RegionManager.RequestNavigate(ConstName.MainViewRegion, menubar.View);
        }
    }
}
