using Application.Common;

namespace Application.Mapper
{
    public class ModbusRegisterDto : BaseDomainDto
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

        protected object _value;
        protected object _writevalue;

        public int DeviceId
        {
            get => _deviceId;
            set => SetProperty(ref _deviceId, value);
        }

        public string? RegisterUri
        {
            get => _registerUri;
            set => SetProperty(ref _registerUri, value);
        }

        public string? RegisterName
        {
            get => _registerName;
            set => SetProperty(ref _registerName, value);
        }

        public ValueDataType ValueDataType
        {
            get => _valueDataType;
            set => SetProperty(ref _valueDataType, value);
        }

        public string ValueDataTypeText => ValueDataType.GetDescription();

        public ModbusDataType ModbusType
        {
            get => _modbusType;
            set => SetProperty(ref _modbusType, value);
        }

        public string ModbusTypeText => ModbusType.GetDescription();

        public ushort StartAddress
        {
            get => _startAddress;
            set => SetProperty(ref _startAddress, value);
        }

        public ushort NumberOfPoints
        {
            get => _numberOfPoints;
            set => SetProperty(ref _numberOfPoints, value);
        }

        public bool Readable
        {
            get => _readable;
            set => SetProperty(ref _readable, value);
        }

        public bool Writeable
        {
            get => _writeable;
            set => SetProperty(ref _writeable, value);
        }

        public object Value
        {
            get => _value;
            set
            {
                SetProperty(ref _value, value);
                WriteValue = Value;
            }
        }

        public object WriteValue
        {
            get => _writevalue;
            set
            {
                SetProperty(ref _writevalue, value);
                switch (ValueDataType)
                {
                    case ValueDataType.Boolean:
                        BooleanValue = Convert.ToBoolean(value);
                        break;
                    case ValueDataType.Byte:
                        ByteValue = Convert.ToByte(value);
                        break;
                    case ValueDataType.Int16:
                        Int16Value = Convert.ToInt16(value);
                        break;
                    case ValueDataType.UInt16:
                        UInt16Value = Convert.ToUInt16(value);
                        break;
                    case ValueDataType.Word:
                        WordValue = string.Format("{0:X4}", Convert.ToUInt16(value));
                        break;
                    case ValueDataType.Int32:
                        Int32Value = Convert.ToInt32(value);
                        break;
                    case ValueDataType.UInt32:
                        UInt32Value = Convert.ToUInt32(value);
                        break;
                    case ValueDataType.DWord:
                        DWordValue = string.Format("{0:X8}", Convert.ToUInt32(value));
                        break;
                    case ValueDataType.Int64:
                        Int64Value = Convert.ToInt64(value);
                        break;
                    case ValueDataType.UInt64:
                        UInt64Value = Convert.ToUInt64(value);
                        break;
                    case ValueDataType.Single:
                        SingleValue = Convert.ToSingle(value);
                        break;
                    case ValueDataType.Double:
                        DoubleValue = Convert.ToDouble(value);
                        break;
                    case ValueDataType.String:
                        break;
                    case ValueDataType.Ascii:
                        break;
                    default:
                        break;
                }
            }
        }

        public bool BooleanValue
        {
            get => Convert.ToBoolean(_writevalue);
            set => SetProperty(ref _writevalue, value);
        }

        public byte ByteValue
        {
            get => Convert.ToByte(_writevalue);
            set => SetProperty(ref _writevalue, value);
        }

        public short Int16Value
        {
            get => Convert.ToInt16(_writevalue);
            set => SetProperty(ref _writevalue, value);
        }

        public ushort UInt16Value
        {
            get => Convert.ToUInt16(_writevalue);
            set => SetProperty(ref _writevalue, value);
        }

        public int Int32Value
        {
            get => Convert.ToInt32(_writevalue);
            set => SetProperty(ref _writevalue, value);
        }

        public uint UInt32Value
        {
            get => Convert.ToUInt32(_writevalue);
            set => SetProperty(ref _writevalue, value);
        }

        public long Int64Value
        {
            get => Convert.ToInt64(_writevalue);
            set => SetProperty(ref _writevalue, value);
        }

        public ulong UInt64Value
        {
            get => Convert.ToUInt64(_writevalue);
            set => SetProperty(ref _writevalue, value);
        }

        public float SingleValue
        {
            get => Convert.ToSingle(_writevalue);
            set => SetProperty(ref _writevalue, value);
        }

        public double DoubleValue
        {
            get => Convert.ToDouble(_writevalue);
            set => SetProperty(ref _writevalue, value);
        }

        public string WordValue
        {
            get => _writevalue.ToString()!;
            set => SetProperty(ref _writevalue, value);
        }

        public string DWordValue
        {
            get => _writevalue.ToString()!;
            set => SetProperty(ref _writevalue, value);
        }
    }
}
