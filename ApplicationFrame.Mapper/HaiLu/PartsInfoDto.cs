namespace Application.Mapper;

public class PartsInfoDto : BaseDomainDto
{
    protected string? _batchcode; //合同号
    protected string? _code;      //编码
    protected string? _name;      //名称
    protected string? _codename;  //代号
    protected int _length;        //长度L
    protected int _width1;        //宽度W1
    protected int _width2;        //宽度W2
    protected int _thickness;     //厚度
    protected int _quautity;      //数量
    protected int _remark;       //备注
    protected string? _area;      //平方数
    protected int _countinfo;     //数量信息
    
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
    
    public int Width2
    {
        get => _width2;
        set => SetProperty(ref this._width2, value);
    }
    
    public int Thickness
    {
        get => _thickness;
        set => SetProperty(ref this._thickness, value);
    }

    public int Quautity
    {
        get => _quautity;
        set => SetProperty(ref this._quautity, value);
    }
    
    public int Remark
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
}