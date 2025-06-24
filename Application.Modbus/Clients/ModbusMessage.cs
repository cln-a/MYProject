using Modbus.Data;
using System.Runtime.InteropServices;

namespace Application.Modbus
{
    /// <summary>
    /// 包含最基本的Modbus消息结构
    /// </summary>
    public class ModbusMessage
    {
        protected ushort _dataLength;
        protected List<ModbusVariable> _variables;

        public ushort StartAddress { get; set; }

        /// <summary>
        /// 寄存器长度
        /// </summary>
        public ushort DataLength => _dataLength;
        public List<ModbusVariable> Variables => _variables;
        public ModbusDataType DataType {  get; set; }

        /// <summary>
        /// 构造函数只是简单的赋值，不关心变量的偏移计算，也不考虑变量的值的类型
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="startAddress"></param>
        /// <param name="dataLength"></param>
        /// <param name="modbusVariables"></param>
        public ModbusMessage(ModbusDataType dataType, ushort startAddress, ushort dataLength, List<ModbusVariable> modbusVariables)
        {
            this.DataType = dataType;
            this.StartAddress = startAddress;
            this._dataLength = dataLength;
            this._variables = modbusVariables;
        }
    }

    /// <summary>
    /// 这个泛型子类，是基于变量值的类型 T（例如 bool, int, float, string）来初始化变量的偏移量的
    /// 
    /// 处理不同数据类型的Modbus通信变量时，根据类型自动计算每个变量在数据块中的偏移位置（Index），并设置所属的消息（Message）
    /// 这个写法是为了实现一种泛型、高复用、类型安全的Modbus消息封装机制
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ModbusMessage<T> : ModbusMessage where T : IComparable
    {
        public ModbusMessage(ModbusDataType dataType, ushort startAddress, ushort dataLength, List<ModbusVariable> modbusVariables) 
            : base(dataType, startAddress, dataLength, modbusVariables)
        {
            for (int i = 0; i < modbusVariables.Count; i++) 
            {
                //这个判断是用来区分位变量（coil线圈/Discrete Inputs离散输入）和寄存器变量（Holding Register保持寄存器/Input Registers输入寄存器）
                //bool类型：1位不需要乘以类型大小
                //其他类型：如（int,float）占多个字节或寄存器，需要按大小计算偏移
                //Index ：计算变量在当前 Modbus 报文中的偏移位置，后续从数据报文中取值的关键依据
                if (typeof(bool) != typeof(T))
                    //Unicode 字符串按字符或寄存器为单位定位，不用乘以 Marshal.SizeOf<T>()；
                    if (modbusVariables[i].DataType == Common.ValueDataType.Unicode)
                        modbusVariables[i].Index = (modbusVariables[i].Model.StartAddress - modbusVariables[0].Model.StartAddress);
                    //其他如 int, short，按字节大小乘计算偏移。
                    //每个Modbus：Holding Register = 16位 = 2字节，乘以Marshal.SizeOf<T>()的原因是需要从寄存器偏移转换为字节偏移。
                    //操作一格一格的方块内存，每块2字节：你知道要去第N个方块，但不知道每个变量占多少个字节，所以要算
                    else
                        modbusVariables[i].Index = (modbusVariables[i].Model.StartAddress - modbusVariables[0].Model.StartAddress) * Marshal.SizeOf<T>();
                else
                    modbusVariables[i].Index = (modbusVariables[i].Model.StartAddress - modbusVariables[0].Model.StartAddress);
                modbusVariables[i].Message = this;
            }
        }
    }

    /*
     * 选择ushort/bool的原因
     * 
     *       Modbus 数据类型           单元大小          典型类型（T）              对应消息类          
     *  | ------------------------ | ------------  | -------------------- | -----------------  |
     *  | Coil / Discrete Input    | 1 bit（1 位）  | `bool`               | `BitMessage`       |
     *  | Holding / Input Register | 16 bit（2 字节） | `ushort`（16位无符号） | `RegisterMessage`  |
     */

    /// <summary>
    ///  T 是 ushort，即每个单元是一个 16 位无符号整数（寄存器数据标准单位）
    /// </summary>
    public class RegisterMessage : ModbusMessage<ushort>
    {
        public byte[] Data;

        public RegisterMessage(ModbusDataType dataType, ushort startAddress, ushort dataLength, List<ModbusVariable> modbusVariables)
            : base(dataType, startAddress, dataLength, modbusVariables)
        //dataLength代表寄存器长度，一个寄存器长度是2字节（ushort也占用两字节，byte为一字节），所以总共dataLength * 2字节
        //Modbus寄存器传输的数据是连续的ushort[](2字节*N)
        => Data = new byte[dataLength * 2];

        /// <summary>
        /// 接收从Modbus主站/从站读回的原始寄存器数据（ushort[]），并根据变量的数据类型进行解析
        /// </summary>
        /// <param name="value"></param>
        public void SetData(ushort[] value)
        {
            foreach (var variable in _variables)
            {
                //Unicode占用多个寄存器数，字节数可变，故需要特殊处理，Unicode是字符串，解析方式与基本数据完全不同(按字符解析，不能按位处理)
                if (variable.DataType == Common.ValueDataType.Unicode)
                    variable.SetUnicodeValue(value, variable.Index);
                //Single/Uint32占2个寄存器（4字节），需要合并处理
                else if (variable.DataType == Common.ValueDataType.Single || variable.DataType == Common.ValueDataType.UInt32)
                    variable.SetSingleValue(value, variable.Index);
                else
                {
                    //其它数据类型具有固定的字节长度 + 已知的起始偏移量
                    //所以只需要把ushort[]转换为byte[]所有变量就可以通过Index精准提取
                    Buffer.BlockCopy(value, 0, Data, 0, Data.Length);
                    variable.SetValue(Data, variable.Index);
                }
            }
        }
    }

    /// <summary>
    /// T 是 bool，即每个单元是一个 1位的bool量（线圈数据标准单位）
    /// </summary>
    public class BitMessage : ModbusMessage<bool>
    {
        public BitMessage(ModbusDataType dataType, ushort startAddress, ushort dataLength, List<ModbusVariable> modbusVariables) 
            : base(dataType, startAddress, dataLength, modbusVariables)
        {
        }

        public void SetData(bool[] value) 
        {
            foreach (var variable in _variables)
                variable.SetValue(value, variable.Index);
        }
    }
}