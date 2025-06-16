namespace Application.Model
{
    public class BaseDomain<T> : BaseModel
    {
        protected T _id;
        protected bool _isenabled;
        protected string _creator;
        protected DateTime _createtime;
        protected string _updator;
        protected DateTime _updatetime;
        protected string _description;

        public virtual T Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public virtual bool IsEnabled
        {
            get => _isenabled;
            set => SetProperty(ref _isenabled, value);
        }

        public virtual string Creator
        {
            get => _creator;
            set => SetProperty(ref _creator, value);
        }

        public virtual DateTime CreateTime
        {
            get => _createtime;
            set => SetProperty(ref _createtime, value);
        }

        public virtual string Updator
        {
            get => _updator;
            set => SetProperty(ref _updator, value);
        }

        public virtual DateTime UpdateTime
        {
            get => _updatetime;
            set => SetProperty(ref _updatetime, value);
        }

        public virtual string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
    }
}
