using SqlSugar;

namespace Application.Model
{
    [SugarTable]    
    public class SinglePartInfo : BaseDomain
    {
        protected int _countNumber;   
        protected string? _batchcode; //合同号
        protected string? _code;      //编码
        protected string? _name;      //名称
        protected string? _codename;  //代号
        protected int _length;        //长度L
        protected int _width1;        //宽度W1
        protected int _thickness;     //厚度
        protected int _remark;       //备注
        protected string? _stateinfo;     //状态信息

        [SugarColumn]
        public int CountNumber { get => _countNumber; set => _countNumber = value; }
        [SugarColumn]
        public string? BatchCode { get => _batchcode;set => _batchcode = value; }
        [SugarColumn]
        public string? Code { get => _code; set => _code = value; }
        [SugarColumn]
        public string? Name { get => _name; set => _name = value; }
        [SugarColumn]
        public string? CodeName { get => _codename; set => _codename = value; }
        [SugarColumn]
        public int Length { get => _length; set => _length = value; }
        [SugarColumn]
        public int Width1 { get => _width1; set => _width1 = value; }
        [SugarColumn]
        public int Thickness { get => _thickness; set => _thickness = value; }
        [SugarColumn]
        public int Remark { get => _remark; set => _remark = value; }
        [SugarColumn]
        public string? StateInfo { get => _stateinfo; set => _stateinfo = value; }
    }
}
