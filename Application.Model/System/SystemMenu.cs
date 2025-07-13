using SqlSugar;

namespace Application.Model.System
{
    [SugarTable]
    public class SystemMenu : BaseDomain
    {
        private string? _menuNameCN;
        private string? _menuNameUS;
        private string? _menuNameRussia;
        private string? _menuIcon;
        private string? _menuView;
        private int _menuSort;

        [SugarColumn]
        public string? MenuNameCN { get => _menuNameCN; set => _menuNameCN = value; }

        [SugarColumn]
        public string? MenuNameUS { get => _menuNameUS; set => _menuNameUS = value; }

        [SugarColumn]
        public string? MenuNameRussia { get => _menuNameRussia;set => _menuNameRussia = value; }

        [SugarColumn]
        public string? MenuIcon { get => _menuIcon; set => _menuIcon = value; }

        [SugarColumn]
        public string? MenuView { get => _menuView; set => _menuView = value; }

        [SugarColumn]
        public int MenuSort { get => _menuSort; set => _menuSort = value; } 
    }
}
