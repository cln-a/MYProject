namespace Application.Model
{
    public class SystemUser : BaseDomain
    {
        private string? _username;
        private string? _userpass;
        private int _accountLevel;

        public string? UserName
        {
            get => _username;
            set => _username = value;
        }

        public string? UserPass
        {
            get => _userpass;
            set => _userpass = value;
        }

        public int AccountLevel
        {
            get => _accountLevel;   
            set => _accountLevel = value;
        }
    }
}
