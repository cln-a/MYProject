using Application.Common;

namespace Application.Login
{
    public class LoginViewModel : BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILanguageManager _languageManager;
        private string? _username;
        private string? _userpass;
        private DelegateCommand? _loginCommand;
        private DelegateCommand? _loginLoadCommand;
        private DelegateCommand<string>? _checkCommand;

        public IEventAggregator EventAggregator => _eventAggregator;
        public ILanguageManager LanguageManager => _languageManager;
        public string? UserName { get => _username; set => SetProperty(ref _username, value); }
        public string? UserPass { get => _userpass; set => SetProperty(ref _userpass, value); }
        public DelegateCommand LoginCommand => _loginCommand ??= new DelegateCommand(() =>
        {
            EventAggregator.GetEvent<LoginInEvents>().Publish();
        });
        public DelegateCommand LoginLoadCommand => _loginLoadCommand ??= new DelegateCommand(() =>
        {
            LanguageManager.SetLanguage(LanguageType.CN);
        });
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
        });

        public LoginViewModel(IEventAggregator eventAggregator, ILanguageManager languageManager)
        {
            this._eventAggregator = eventAggregator;
            this._languageManager = languageManager;
        }
    }
}
