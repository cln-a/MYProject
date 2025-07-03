using SqlSugar;

namespace Application.Model.System
{
    [SugarTable]
    public class SystemMenu : BaseDomain
    {
        private string? _menuName;
        private string? _menuIcon;
        private string? _menuView;
        private int _menuSort;

        [SugarColumn]
        public string? MenuName { get => _menuName; set => _menuName = value; }

        [SugarColumn]
        public string? MenuIcon { get => _menuIcon; set => _menuIcon = value; }

        [SugarColumn]
        public string? MenuView { get => _menuView; set => _menuView = value; }

        [SugarColumn]
        public int MenuSort { get => _menuSort; set => _menuSort = value; } 
    }
}
