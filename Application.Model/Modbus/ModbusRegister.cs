using Application.Common;
using SqlSugar;

namespace Application.Model
{
    [SugarTable]
    public class ModbusRegister : BaseDomain
    {
        protected int _deviceId;
        protected string? _registerUri;
        protected string? _registerName;
        protected ValueDataType _valueDataType;
        protected ModbusDataType _modbusType;
        protected ushort _startAddress;
        protected ushort _numberOfPoints;
        protected bool _readable;
        protected bool _writeable;

        /// <summary>
        /// 寄存器对应设备
        /// </summary>
        [SugarColumn]
        public int DeviceId
        {
            get => _deviceId;
            set => _deviceId = value;
        }

        /// <summary>
        /// 寄存器唯一路径标识符
        /// </summary>
        [SugarColumn]
        public string? RegisterUri
        {
            get => _registerUri;
            set => _registerUri = value;
        }

        /// <summary>
        /// 寄存器唯一路径标识符
        /// </summary>
        [SugarColumn]
        public string? RegisterName
        {
            get => _registerName;
            set => _registerName = value;
        }

        /// <summary>
        /// 数据类型
        /// </summary>
        [SugarColumn]
        public ValueDataType ValueDataType
        {
            get => _valueDataType;
            set => _valueDataType = value;
        }

        /// <summary>
        /// Modbus寄存器类别
        /// </summary>
        [SugarColumn]
        public ModbusDataType ModbusType
        {
            get => _modbusType;
            set => _modbusType = value;
        }

        /// <summary>
        /// Modbus寄存器起始地址
        /// </summary>
        [SugarColumn]
        public ushort StartAddress
        {
            get => _startAddress;
            set => _startAddress = value;
        }

        /// <summary>
        /// 寄存器数量
        /// </summary>
        [SugarColumn]
        public ushort NumberOfPoints
        {
            get => _numberOfPoints;
            set => _numberOfPoints = value;
        }

        /// <summary>
        /// 可读
        /// </summary>
        [SugarColumn]
        public bool Readable
        {
            get => _readable;
            set => _readable = value;
        }

        /// <summary>
        /// 可写
        /// </summary>
        [SugarColumn]
        public bool Writeable
        {
            get => _writeable;
            set => _writeable = value;
        }
    }
}
