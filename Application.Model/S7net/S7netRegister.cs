using Application.Common;

namespace Application.Model
{
    public class S7netRegister : BaseDomain
    {
        protected int _deviceId;
        protected string? _registerName;
        protected string? _registerUri;
        protected int _dbNumber;
        protected ushort _startAddress;
        protected ushort _numberOfPoints;
        protected ValueDataType _valueDataType;
        protected bool _readable;
        protected bool _writeable;

        /// <summary>
        /// 寄存器对应设备
        /// </summary>
        public int DeviceId 
        {
            get => _deviceId; 
            set => _deviceId = value;
        }

        /// <summary>
        /// 寄存器唯一路径标识符
        /// </summary>
        public string? RegisterName 
        {
            get => _registerName;
            set => _registerName = value;
        }

        /// <summary>
        /// 寄存器唯一路径标识符
        /// </summary>
        public string? RegisterUri 
        {
            get => _registerUri;
            set => _registerUri = value;
        }

        /// <summary>
        /// 数据块号
        /// </summary>
        public int DbNumber 
        { 
            get => _dbNumber;
            set => _dbNumber = value;
        }

        /// <summary>
        /// 寄存器起始地址
        /// </summary>
        public ushort StartAddress
        {
            get => _startAddress;
            set => _startAddress = value;
        }

        /// <summary>
        /// 寄存器数量
        /// </summary>
        public ushort NumberOfPoints 
        {
            get => _numberOfPoints;
            set => _numberOfPoints = value;
        }

        /// <summary>
        /// 数据类型
        /// </summary>
        public ValueDataType ValueDataType
        {
            get => _valueDataType;
            set => _valueDataType = value;
        }
        public bool Readable 
        {
            get => _readable;
            set => _readable = value;
        }
        public bool Writeable
        { 
            get => _writeable;
            set => _writeable = value;
        }
    }
}
