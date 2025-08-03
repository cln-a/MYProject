using System.Configuration;

namespace Application.Mapper;

public class PartsInfoDto : BaseDomainDto
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
    protected ushort _remark;       //备注
    protected string? _area;      //平方数
    protected int _countinfo;     //数量信息
    protected ushort _holeLengthRight;    //右侧铣刀长度
    protected ushort _holeDistanceRight;  //右侧铣刀距离底部距离
    protected ushort _holeLengthMiddle;   //中间铣刀长度 
    protected ushort _holeDistanceMiddle; //中间铣刀距离底部距离
    protected ushort _holeLengthLeft;     //左侧铣刀长度
    protected ushort _holeDistanceLeft;   //左侧铣刀距离底部距离
    protected bool _mcOrNot;          //是否需要铣刀

    public string? BatchCode
    {
        get => _batchcode;
        set => SetProperty(ref this._batchcode, value);
    }

    public string? Batch
    {
        get => _batch;
        set => SetProperty(ref _batch, value);
    }

    public string? Identity
    {
        get => _identity;
        set => SetProperty(ref _identity, value);
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
    
    public ushort Length
    {
        get => _length;
        set => SetProperty(ref this._length, value);
    }
    
    public ushort Width1
    {
        get => _width1;
        set => SetProperty(ref this._width1, value);
    }
    
    public ushort Width2
    {
        get => _width2;
        set => SetProperty(ref this._width2, value);
    }
    
    public ushort Thickness
    {
        get => _thickness;
        set => SetProperty(ref this._thickness, value);
    }

    public int Quautity
    {
        get => _quautity;
        set => SetProperty(ref this._quautity, value);
    }
    
    public ushort Remark
    {
        get => _remark;
        set => SetProperty(ref this._remark, value);
    }

    public string? Area
    {
        get => _area;
        set => SetProperty(ref this._area, value);
    }
    
    public int Countinfo
    {
        get => _countinfo;
        set => SetProperty(ref this._countinfo, value);
    }

    public ushort HoleLengthRight 
    { 
        get => _holeLengthRight; 
        set => SetProperty(ref _holeLengthRight, value);
    }

    public ushort HoleDistanceRight 
    { 
        get => _holeDistanceRight; 
        set => SetProperty(ref _holeDistanceRight, value);
    }

    public ushort HoleLengthMiddle 
    { 
        get => _holeLengthMiddle; 
        set => SetProperty(ref _holeLengthMiddle, value); 
    }

    public ushort HoleDistanceMiddle 
    { 
        get => _holeDistanceMiddle; 
        set => SetProperty(ref _holeDistanceMiddle, value);
    }

    public ushort HoleLengthLeft 
    { 
        get => _holeLengthLeft; 
        set => SetProperty(ref _holeLengthLeft, value); 
    }

    public ushort HoleDistanceLeft 
    { 
        get => _holeDistanceLeft; 
        set => SetProperty(ref _holeDistanceLeft, value); 
    }

    public bool McOrNot 
    { 
        get => _mcOrNot;
        set => SetProperty(ref _mcOrNot, value); 
    }
}