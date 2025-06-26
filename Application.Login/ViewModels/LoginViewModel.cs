namespace Application.Login
{
    public class LoginViewModel : BindableBase
    {
        private string? _username;
        private string? _userpass;

        public string? UserName { get => _username; set => SetProperty(ref _username, value); }
        public string? UserPass { get => _userpass; set => SetProperty(ref _userpass, value); }
    }
}
