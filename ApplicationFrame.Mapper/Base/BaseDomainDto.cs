namespace Application.Mapper
{
    public class BaseDomainDto : BindableBase
    {
        protected int _id;
        protected string? _creator;
        protected DateTime? _createTime;
        protected string? _updater;
        protected DateTime? _updateTime;
        protected bool _isEnabled;
        protected string? _description;
        protected bool _isChecked = true;

        public virtual int Id { get => _id; set => _id = value; }

        public virtual string? Creator { get => _creator; set => _creator = value; }

        public virtual DateTime? CreateTime { get => _createTime; set => _createTime = value; }

        public virtual string? Updater { get => _updater; set => _updater = value; }

        public virtual DateTime? UpdateTime{ get => _updateTime; set => _updateTime = value; }

        public bool IsEnabled{ get => _isEnabled; set => _isEnabled = value; }

        public string? Description{ get => _description; set => _description = value; }

        public bool IsChecked { get => _isChecked; set => SetProperty(ref _isChecked, value); }
    }
}
