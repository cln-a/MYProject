namespace Application.Mapper
{
    public class BaseDomainDto : BindableBase
    {
        protected int _id;
        protected string? _creator = "admin";
        protected DateTime? _createTime = DateTime.Now;
        protected string? _updater = "admin";
        protected DateTime? _updateTime = DateTime.Now;
        protected bool _isEnabled = true;
        protected string? _description;
        protected bool _isChecked = true;

        public virtual int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public virtual string? Creator
        {
            get => _creator;
            set => SetProperty(ref _creator, value);
        }

        public virtual DateTime? CreateTime
        {
            get => _createTime;
            set => SetProperty(ref _createTime, value);
        }

        public virtual string? Updater
        {
            get => _updater;
            set => SetProperty(ref _updater, value);
        }

        public virtual DateTime? UpdateTime
        {
            get => _updateTime;
            set => SetProperty(ref _updateTime, value);
        }

        public bool IsEnabled
        {
            get => _isEnabled;
            set => SetProperty(ref _isEnabled, value);
        }

        public string? Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public bool IsChecked
        {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }
    }
}
