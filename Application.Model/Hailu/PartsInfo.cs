using SqlSugar;

namespace Application.Model
{
    [SugarTable]
    public class PartsInfo : BaseDomain
    {
        protected string? _batchcode; //合同号
        protected string? _batch;     //批次
        protected string? _identity;  //唯一标识符
        protected string? _code;      //编码
        protected string? _name;      //名称
        protected string? _codename;  //代号
        protected ushort _length;        //长度L
        protected ushort _width1;        //宽度W1
        protected ushort _width2;        //宽度W2
        protected ushort _thickness;     //厚度
        protected int _quautity;      //数量
        protected ushort _remark;        //备注
        protected string? _area;      //平方数
        protected int _countinfo;     //数量信息
        protected bool _mcOrNot;      //是否需要铣刀
        protected ushort _holeLengthRight;    //右侧铣刀长度
        protected ushort _holeDistanceRight;  //右侧铣刀距离底部距离
        protected ushort _holeLengthMiddle;   //中间铣刀长度 
        protected ushort _holeDistanceMiddle; //中间铣刀距离底部距离
        protected ushort _holeLengthLeft;     //左侧铣刀长度
        protected ushort _holeDistanceLeft;   //左侧铣刀距离底部距离

        [SugarColumn]
        public string? BatchCode { get => _batchcode; set => _batchcode = value; }

        [SugarColumn]
        public string? Batch { get => _batch; set => _batch = value; }

        [SugarColumn]
        public string? Identity { get => _identity; set => _identity = value; }  

        [SugarColumn]
        public string? Code { get => _code; set => _code = value; }

        [SugarColumn]
        public string? Name { get => _name; set => _name = value; }

        [SugarColumn]
        public string? CodeName { get => _codename;set => _codename = value; }

        [SugarColumn]
        public ushort Length { get => _length; set => _length = value; }

        [SugarColumn]
        public ushort Width1 { get => _width1; set => _width1 = value; }

        [SugarColumn]
        public ushort Width2 { get => _width2;set => _width2 = value; }

        [SugarColumn]
        public ushort Thickness { get => _thickness; set => _thickness = value; }

        [SugarColumn]
        public int Quautity { get => _quautity; set => _quautity = value; }

        [SugarColumn]
        public ushort Remark { get => _remark; set => _remark = value; }

        [SugarColumn]
        public string? Area { get => _area; set => _area = value; }

        [SugarColumn]
        public int Countinfo { get => _countinfo; set => _countinfo = value; }

        [SugarColumn]
        public bool McOrNot { get => _mcOrNot; set => _mcOrNot = value; }

        [SugarColumn]
        public ushort HoleLengthRight { get => _holeLengthRight; set => _holeLengthRight = value; }

        [SugarColumn]
        public ushort HoleDistanceRight { get => _holeDistanceRight;set => _holeDistanceRight = value; }

        [SugarColumn]
        public ushort HoleLengthMiddle { get => _holeLengthMiddle; set => _holeLengthMiddle = value; }

        [SugarColumn]
        public ushort HoleDistanceMiddle { get => _holeDistanceMiddle; set => _holeDistanceMiddle = value; }    

        [SugarColumn]
        public ushort HoleLengthLeft { get => _holeLengthLeft; set => _holeLengthLeft = value; }

        [SugarColumn]
        public ushort HoleDistanceLeft { get => _holeDistanceLeft; set => _holeDistanceLeft = value; }  
    }
}
