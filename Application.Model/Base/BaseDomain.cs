namespace Application.Model
{
    public class BaseDomain : BindableBase
    {
        protected int _id;
        protected string? _creator = "admin";
        protected DateTime? _createTime = DateTime.Now;
        protected string? _updater = "admin";
        protected DateTime? _updateTime = DateTime.Now;

        public virtual int Id
        {
            get => _id; 
            set => _id = value;
        }

        public virtual string? Creator
        {
            get => _creator;
            set => _creator = value;
        }

        public virtual DateTime? CreateTime
        {
            get => _createTime;
            set => _createTime = value;
        }

        public virtual string? Updater
        {
            get => _updater;    
            set => _updater = value;
        }

        public virtual DateTime? UpdateTime
        {
            get => _updateTime; 
            set => _updateTime = value;
        }
    }
}
