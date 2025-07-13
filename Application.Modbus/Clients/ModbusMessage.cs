using Modbus.Data;
using System.Runtime.InteropServices;

namespace Application.Modbus
{
    public class ModbusMessage
    {
        protected ushort _dataLength;
        public ushort StartAddress { get; set; }
        public ushort DataLength { get => _dataLength; }
        public List<ModbusVariable> Variables => _variables;

        public ModbusDataType DataType { get; set; }
        protected List<ModbusVariable> _variables;

        public ModbusMessage(ModbusDataType dataType, ushort startAddress, ushort dataLength, List<ModbusVariable> variables)
        {
            DataType = dataType;
            StartAddress = startAddress;
            _dataLength = dataLength;
            _variables = variables;
        }
    }

    public class ModbusMessage<T> : ModbusMessage where T : IComparable
    {
        public ModbusMessage(ModbusDataType dataType, ushort startAddress, ushort dataLength, List<ModbusVariable> variables)
            : base(dataType, startAddress, dataLength, variables)
        {
            for (int i = 0; i < variables.Count; i++)
            {
                if (typeof(bool) != typeof(T))
                    if (variables[i].DataType == Common.ValueDataType.Unicode)
                        variables[i].Index = (variables[i].Model.StartAddress - variables[0].Model.StartAddress);
                    else
                        variables[i].Index = (variables[i].Model.StartAddress - variables[0].Model.StartAddress) * Marshal.SizeOf<T>();
                else
                    variables[i].Index = (variables[i].Model.StartAddress - variables[0].Model.StartAddress);
                variables[i].Message = this;
            }
        }
    }

    public class RegisterMessage : ModbusMessage<ushort>
    {
        public byte[] Data;
        public RegisterMessage(ModbusDataType dataType, ushort startAddress, ushort dataLength, List<ModbusVariable> variables)
            : base(dataType, startAddress, dataLength, variables)
        {
            Data = new byte[dataLength * 2];
        }
        public void SetData(ushort[] value)
        {
            foreach (var variable in _variables)
            {
                if (variable.DataType == Common.ValueDataType.Unicode)
                    variable.SetUnicodeValue(value, variable.Index);
                else if (variable.DataType == Common.ValueDataType.Single || variable.DataType == Common.ValueDataType.UInt32)
                    variable.SetSingleValue(value, variable.Index);
                else
                {
                    Buffer.BlockCopy(value, 0, Data, 0, Data.Length);
                    variable.SetValue(Data, variable.Index);
                }
            }
        }
    }

    public class BitMessage : ModbusMessage<bool>
    {
        public BitMessage(ModbusDataType dataType, ushort startAddress, ushort dataLength, List<ModbusVariable> variables)
            : base(dataType, startAddress, dataLength, variables)
        {
        }

        public void SetData(bool[] value)
        {
            foreach (var variable in _variables)
                variable.SetValue(value, variable.Index);
        }
    }
}