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
        protected ushort _length;        //长度L
        protected ushort _width1;        //宽度W1
        protected ushort _thickness;     //厚度
        protected ushort _remark;        //备注
        protected string? _stateinfo;     //状态信息
        protected bool _mcOrNot;          //是否需要铣刀
        protected ushort _holeLengthRight;    //右侧铣刀长度
        protected ushort _holeDistanceRight;  //右侧铣刀距离底部距离
        protected ushort _holeLengthMiddle;   //中间铣刀长度 
        protected ushort _holeDistanceMiddle; //中间铣刀距离底部距离
        protected ushort _holeLengthLeft;     //左侧铣刀长度
        protected ushort _holeDistanceLeft;   //左侧铣刀距离底部距离

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
        public ushort Length { get => _length; set => _length = value; }

        [SugarColumn]
        public ushort Width1 { get => _width1; set => _width1 = value; }

        [SugarColumn]
        public ushort Thickness { get => _thickness; set => _thickness = value; }

        [SugarColumn]
        public ushort Remark { get => _remark; set => _remark = value; }

        [SugarColumn]
        public string? StateInfo { get => _stateinfo; set => _stateinfo = value; }

        [SugarColumn]
        public bool McOrNot { get => _mcOrNot; set => _mcOrNot = value; }

        [SugarColumn]
        public ushort HoleLengthRight { get => _holeLengthRight; set => _holeLengthRight = value; }

        [SugarColumn]
        public ushort HoleDistanceRight { get => _holeDistanceRight; set => _holeDistanceRight = value; }

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
