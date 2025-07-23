namespace Application.Mapper;

public class SinglePartInfoDto : BaseDomainDto
{
    protected int _countNumber;   //数量
    protected string? _batchcode; //合同号
    protected string? _code;      //编码
    protected string? _name;      //名称
    protected string? _codename;  //代号
    protected int _length;        //长度L
    protected int _width1;        //宽度W1
    protected int _thickness;     //厚度
    protected int _remark;       //备注
    protected string? _stateinfo;     //状态信息
    protected int _holeLengthRight;    //右侧铣刀长度
    protected int _holeDistanceRight;  //右侧铣刀距离底部距离
    protected int _holeLengthMiddle;   //中间铣刀长度 
    protected int _holeDistanceMiddle; //中间铣刀距离底部距离
    protected int _holeLengthLeft;     //左侧铣刀长度
    protected int _holeDistanceLeft;   //左侧铣刀距离底部距离

    public int CountNumber
    {
        get => _countNumber;
        set => SetProperty(ref this._countNumber, value);
    }
    
    public string? BatchCode
    {
        get => _batchcode;
        set => SetProperty(ref this._batchcode, value);
    }
    
    public string? Code
    {
        get => _code;
        set => SetProperty(ref this._code, value);
    }
    
    public string? Name
    {
        get => _name;
        set => SetProperty(ref this._name, value);
    }
    
    public string? CodeName
    {
        get => _codename;
        set => SetProperty(ref this._codename, value);
    }
    
    public int Length
    {
        get => _length;
        set => SetProperty(ref this._length, value);
    }
    
    public int Width1
    {
        get => _width1;
        set => SetProperty(ref this._width1, value);
    }
    
    public int Thickness
    {
        get => _thickness;
        set => SetProperty(ref this._thickness, value);
    }
    
    public int Remark
    {
        get => _remark;
        set => SetProperty(ref this._remark, value);
    }
    
    public string? StateInfo
    {
        get => _stateinfo;
        set => SetProperty(ref this._stateinfo, value);
    }

    public int HoleLengthRight 
    { 
        get => _holeLengthRight;
        set => SetProperty(ref _holeLengthRight, value);
    }

    public int HoleDistanceRight 
    { 
        get => _holeDistanceRight; 
        set => SetProperty(ref _holeDistanceRight, value);
    }

    public int HoleLengthMiddle 
    {
        get => _holeLengthMiddle; 
        set => SetProperty(ref _holeLengthMiddle, value); 
    }

    public int HoleDistanceMiddle
    {
        get => _holeDistanceMiddle; 
        set => SetProperty(ref _holeDistanceMiddle, value);
    }

    public int HoleLengthLeft 
    { 
        get => _holeLengthLeft; 
        set => SetProperty(ref _holeLengthLeft, value);
    }

    public int HoleDistanceLeft
    { 
        get => _holeDistanceLeft; 
        set => SetProperty(ref _holeDistanceLeft, value); 
    }
}