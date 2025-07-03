namespace Application.Main
{
    public class MenuBar : BindableBase
    {
        private string? _menuName;
        private string? _icon;
        private string? _view;

        public string? MenuName
        {
            get => _menuName;
            set => SetProperty(ref _menuName, value);   
        }

        public string? Icon
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        public string? View
        {
            get => _view; 
            set => SetProperty(ref _view, value);
        }
    }
}
