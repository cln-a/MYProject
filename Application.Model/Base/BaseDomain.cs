using SqlSugar;

namespace Application.Model
{
    public class BaseDomain : BindableBase
    {
        protected int _id;
        protected string? _creator = "admin";
        protected DateTime? _createTime = DateTime.Now;
        protected string? _updater = "admin";
        protected DateTime? _updateTime = DateTime.Now;
        protected bool _isEnabled;
        protected string? _description;

        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public virtual int Id
        {
            get => _id; 
            set => _id = value;
        }

        [SugarColumn]
        public virtual string? Creator
        {
            get => _creator;
            set => _creator = value;
        }

        [SugarColumn]
        public virtual DateTime? CreateTime
        {
            get => _createTime;
            set => _createTime = value;
        }

        [SugarColumn]
        public virtual string? Updater
        {
            get => _updater;    
            set => _updater = value;
        }

        [SugarColumn]
        public virtual DateTime? UpdateTime
        {
            get => _updateTime; 
            set => _updateTime = value;
        }

        [SugarColumn]
        public bool IsEnabled
        {
            get => _isEnabled;
            set => _isEnabled = value;
        }

        [SugarColumn]
        public string? Description
        {
            get => _description;
            set => _description = value;
        }
    }
}
