using Application.Common;
using Application.Model;
using CommonServiceLocator;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;

namespace Application.Modbus
{
    public class ModbusVariable : IVariable
    {
        private ModbusClient _client;

        public int Id => Model == null ? 0 : Model.Id;

        public string Path => Model.RegisterUri;

        public int DbNumber => 0; //not Used;

        public Type ValueType => GetValueType();

        public object AnyValue
        {
            get => ReadAnyValue();
            set => SetAnyValue(value);
        }

        public string DeviceName => DeviceModel.DeviceName;

        public string ValueString => GetValueString();

        public bool IsEnabled => Model.IsEnabled == true;

        public bool Readable => Model.Readable == true;

        public bool Writable => Model.Writeable == true;

        public int StartAddress => Model == null ? 0 : Model.StartAddress;

        public int NumberOfPoints => Model == null ? 0 : Model.NumberOfPoints;

        public string Description => Model.Description;

        public bool IsConnected => Client.Connected == true;

        public ValueDataType DataType => Model.ValueDataType;

        public ModbusDataType RegisterType => Model == null ? ModbusDataType.HoldingRegister : Model.ModbusType;

        public int Index { get; set; }

        public ModbusMessage Message { get; set; }

        public ModbusRegister Model { get; set; }

        public ModbusDevice DeviceModel { get; set; }

        public string VariableName => Model.RegisterName;

        public ModbusClient Client => _client ?? ServiceLocator.Current.GetInstance<ModbusClient>(DeviceModel.DeviceUri.Trim());

        public event EventHandler<ValueChangedEventArgs<object>> ValueChangedEvent;
        public event EventHandler<ValueReadedEventArgs<object>> ValueReadedEvent;

        public ModbusVariable(ModbusRegister register, ModbusDevice deviceModel)
        {
            this.Model = register;
            this.DeviceModel = deviceModel;
        }

        public virtual T GetValue<T>() => default!;

        public virtual T ReadValue<T>() => default!;

        public Task<T> ReadValueAsync<T>() => null!;

        public virtual void WriteAnyValue(object value, bool updateLocalStoreOption = true) { }

        public virtual void WriteStringValue(string value) { }

        public virtual void SetSingleValue(ushort[] value, int index) { }

        public virtual void SetUnicodeValue(ushort[] value, int index) { }

        public virtual void SetValue(bool[] data, int index) { }

        public virtual void SetValue(byte[] data, int index) { }

        public virtual string GetValueString() => "";

        public virtual Type GetValueType() => typeof(object);

        public virtual object ReadAnyValue() => default!;

        public virtual void SetAnyValue(object value) { }

        protected void PublishChangedEvent(object oldVal, object newVal)
        {
            try
            {
                ValueChangedEvent?.Invoke(this, new ValueChangedEventArgs<object>(this, oldVal, newVal));
            }
            catch (Exception ex)
            {
                //Logger
            }
        }

        protected void PublishReadedEvent(object value)
        {
            try
            {
                ValueReadedEvent?.Invoke(this, new ValueReadedEventArgs<object>(this, value));
            }
            catch (Exception ex)
            {
                //Logger
            }
        }
    }

    public class ModbusVariable<T> : ModbusVariable, IVariable<T> where T : IComparable
    {
        private T _value;

        public T Value { get => _value; set => _value = value; }

        public bool IsBoolType => typeof(bool) == typeof(T);

        /// <summary>
        /// Marshal.SizeOf<T>() 用来获取类型T在非托管内存中的大小
        /// </summary>
        public int TypeSize => IsBoolType ? 1 : Marshal.SizeOf<T>();

        public event EventHandler<ValueChangedEventArgs<T>> ValueTChangedEvent;
        public event EventHandler<ValueReadedEventArgs<T>> ValueTReadedEvent;

        public ModbusVariable(ModbusRegister register, ModbusDevice deviceModel) : base(register, deviceModel) { }

        /// <summary>
        /// 将Value强制转换成泛型类型T，并返回这个值
        /// </summary>
        /// <typeparam name="T1"></typeparam>   
        /// <returns></returns>
        public override T1 GetValue<T1>()
        {
            return (T1)Convert.ChangeType(Value, typeof(T1));
        }

        public override T1 ReadValue<T1>()
        {
            ReadValueAsync();
            return (T1)Convert.ChangeType(Value, typeof(T1));
        }

        public override Type GetValueType() => typeof(T);

        public override string GetValueString() => Value?.ToString()!;

        public async void ReadValueAsync()
        {
            try
            {
                if (Model.ModbusType == ModbusDataType.Coil)
                {
                    bool[] data = await Client.ReadMultipleCoilsAsync(Model);
                    SetValue(data, 0);
                }
                else if (Model.ModbusType == ModbusDataType.HoldingRegister) 
                {
                    byte[] temp = new byte[Model.NumberOfPoints * 2];
                    ushort[] data = await Client.ReadHoldingRegistersAsync(Model);
                    SetValue(temp, 0);
                }
            }
            catch(Exception ex)
            {
                //Logger
                throw;
            }
        }

        public override object ReadAnyValue() => Value;

        public override void SetValue(bool[] data, int index) 
        {
            try
            {
                // 计算需要的字节数并初始化(因为bool值是bit级别的，要按8位一组装成byte数组)
                var b_data = new byte[Model.NumberOfPoints / 8 + (Model.NumberOfPoints % 8 == 0 ? 0 : 1)];
                //将bool打包成字节（bit编码）
                //在Modbus中，线圈/离散输入是按位bit表示的，但数据在Modbus通讯中需要打包成字节（byte[]进行传输）
                //外层循环：一个字节一组，每个字节最多可容纳8个bool值
                for (int i = 0; i < b_data.Length; i++)
                {
                    b_data[i] = 0;
                    //内层循环：一个字节的8个bit（这里每次处理一个字节中的bit位，最多8位）
                    for (int j = 0; j < data.Length && j < 8 && index + i * 8 + j < data.Length; j++)
                    {
                        //如果这个bool值为true，就设置当前字节的第j位为1
                        //（1<<j）是位移操作，表示第j位为1，其余为0
                        // |=是按位与：将第j位设置为1，而其它位保持原样
                        if (data[index + i * 8 + j])
                            b_data[i] |= (byte)(1 << j);
                    }
                    if(typeof(T) == typeof(string))
                        SetByteValue(b_data);
                    else
                    {
                        if (!IsBoolType)
                        {
                            var tempData = new byte[TypeSize];
                            int copyLength = new int[] { tempData.Length, b_data.Length }.Where(x => x != 0).Min();
                            if (copyLength > 0)
                            {
                                Buffer.BlockCopy(data, index, tempData, 0, copyLength);
                                SetByteValue(tempData);
                            }
                        }
                        else
                            SetValue((T)Convert.ChangeType(data[index], typeof(bool)));
                    }
                }
            }
            catch (Exception ex) 
            {
                //Logger
            }
        }

        public override void SetValue(byte[] data, int index)
        {
            try
            {
                if(typeof (T) == typeof(string))
                {
                    //NumberOfPoints表示读取了多少个寄存器
                    //字符串在Modbus中通常是定长的ASCII和Unicode字节数组
                    //没有固定的结束标志，可能以空字符\0结尾，也可能没有
                    //为了完整的获取字符串，需要将所有相关寄存器的数据都读取出来
                    var tempData = new byte[Model.NumberOfPoints * 2];
                    Buffer.BlockCopy(data, index, tempData, 0, new int[] { tempData.Length, data.Length - index }.Min());
                    SetByteValue(tempData);
                }
                else
                {
                    var tempData = new byte[TypeSize];
                    int copyLength = new int[] { tempData.Length, Model.NumberOfPoints * 2, data.Length - index }.Where(x => x != 0).Min();
                    if(copyLength > 0)
                    {
                        Buffer.BlockCopy(data, index, tempData, 0, copyLength);
                        SetByteValue(tempData);
                    }
                }
            }
            catch(Exception ex)
            {
                //Logger
            }
        }

        /// <summary>
        /// Modbus是基于16位(ushort)寄存器的协议
        /// Modbus通讯中：
        /// 每个寄存器是2个字节（16bit）
        /// 所以ushort[] data就是寄存器数组
        /// 而 Unicode（UTF-16）编码中，每个字符通常也是 2 字节（比如英文字符），对应刚好一个 ushort；
        /// 因此，只要按顺序读取这些寄存器的原始内容，再还原为 byte[]，就可以直接用于 Unicode 解码。
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index"></param>
        public override void SetUnicodeValue(ushort[] data, int index)
        {
            var tempData = new byte[Model.NumberOfPoints * 2];
            Buffer.BlockCopy(data, index * 2, tempData, 0, Model.NumberOfPoints * 2);
            SetByteValue(tempData);
        }

        /// <summary>
        /// Modbus每个寄存器是16位（2字节）
        /// float和uint32类型占32位（4字节）
        /// 所以读取一个float或uint32类型，需要连续读取2个寄存器，即4字节
        /// 故此处产生大小端问题
        /// </summary>
        /// <param name="value"></param>
        /// <param name="index"></param>
        public override void SetSingleValue(ushort[] data, int index)
        {
            var tempData = new byte[TypeSize];
            int copyLength = new int[] { tempData.Length, Model.NumberOfPoints * 2, data.Length - index / 2 }.Where(x => x != 0).Min();
            if (copyLength > 0)
            {
                Buffer.BlockCopy(data, index, tempData, 0, copyLength);
                byte[] temp = new byte[tempData.Length];
                temp[0] = tempData[2];
                temp[1] = tempData[3];
                temp[2] = tempData[0];
                temp[3] = tempData[1];

                SetByteValue(temp);
            }
        }

        private void SetByteValue(byte[] b_data)
        {
            try
            {
                if (typeof(T) == typeof(string))
                    SetValue(ConvertTo(ToString(b_data)));
                else
                {
                    if (typeof(T) == typeof(bool))
                        SetValue(ConvertTo(ToBoolean(b_data)));
                    else if (typeof(T) == typeof(char))
                        SetValue(ConvertTo((char)b_data[0]));
                    else if (typeof(T) == typeof(byte))
                        SetValue(ConvertTo(b_data[0]));
                    else if (typeof(T) == typeof(sbyte))
                        SetValue(ConvertTo((sbyte)b_data[0]));
                    else if (typeof(T) == typeof(short))
                        SetValue(ConvertTo(ToInt16(b_data)));
                    else if (typeof(T) == typeof(ushort))
                        SetValue(ConvertTo(ToUInt16(b_data)));
                    else if (typeof(T) == typeof(int))
                        SetValue(ConvertTo(ToInt32(b_data)));
                    else if (typeof(T) == typeof(uint))
                        SetValue(ConvertTo(ToUInt32(b_data)));
                    else if (typeof(T) == typeof(long))
                        SetValue(ConvertTo(ToInt64(b_data)));
                    else if (typeof(T) == typeof(ulong))
                        SetValue(ConvertTo(ToUInt64(b_data)));
                    else if (typeof(T) == typeof(float))
                        SetValue(ConvertTo(ToSingle(b_data)));
                    else if (typeof(T) == typeof(double))
                        SetValue(ConvertTo(ToDouble(b_data)));
                    else
                        throw new NotSupportedException("");
                }
            }
            catch (Exception e)
            {
                //Logger
                throw;
            }
        }

        protected T ConvertTo<T2>(T2 newValue)
        {
            try
            {
                return (T)Convert.ChangeType(newValue, typeof(T))!;
            }
            catch(Exception ex)
            {
                //Logger
            }
            return default!;
        }

        protected virtual bool ToBoolean(byte[] data) => BitConverter.ToBoolean(data, 0);
        protected virtual short ToInt16(byte[] data) => BitConverter.ToInt16(data, 0);
        protected virtual ushort ToUInt16(byte[] data) => BitConverter.ToUInt16(data, 0);
        protected virtual int ToInt32(byte[] data) => BitConverter.ToInt32(data, 0);
        protected virtual uint ToUInt32(byte[] data) => BitConverter.ToUInt32(data, 0);
        protected virtual long ToInt64(byte[] data) => BitConverter.ToInt64(data, 0);
        protected virtual ulong ToUInt64(byte[] data) => BitConverter.ToUInt64(data, 0);
        protected virtual float ToSingle(byte[] data) => BitConverter.ToSingle(data, 0);
        protected virtual double ToDouble(byte[] data) => BitConverter.ToDouble(data, 0);
        protected virtual string ToString(byte[] data)
        {
            var temp = new List<byte>();
            for (int i = 0; i < data.Length; i++)
            {
                //从byte中提取有效的非空字符（遇到\0截断）
                if (data[i] != '\0')
                    temp.Add(data[i]);
                else
                    break;
            }

            if (DataType == ValueDataType.Unicode)
                return Encoding.Unicode.GetString(temp.ToArray());
            else
                return Encoding.ASCII.GetString(temp.ToArray());
        }

        private void SetValue(T newValue)
        {
            try
            {
                T oldValue = default!;
                if (_value is ICloneable cloneable)
                    oldValue = (T)cloneable.Clone();
                else
                    oldValue = Value;
                _value = newValue;
                PublishReadedEvent(newValue);
                if (oldValue != null)
                {
                    if (oldValue.CompareTo(newValue) != 0)
                        PublishChangedEvent(oldValue, newValue);
                }
                else
                    PublishChangedEvent(oldValue!, newValue);
            }
            catch (Exception ex) 
            {
                //Logger
            }
        }

        protected void PublishReadedEvent(T value)
        {
            try
            {
                ValueTReadedEvent?.Invoke(this, new ValueReadedEventArgs<T>(this, value));
                base.PublishReadedEvent(value);
            }
            catch(Exception ex)
            {
                //Logger
            }
        }

        protected void PublishChangedEvent(T oldvalue, T newvalue)
        {
            try
            {
                ValueTChangedEvent?.Invoke(this, new ValueChangedEventArgs<T>(this, oldvalue, newvalue));
                base.PublishChangedEvent(oldvalue, newvalue);
            }
            catch (Exception ex) 
            {
                //Logger
            }
        }

        public void WriteValue(T value, bool updateLocalStoreOption = true)
        {
            try
            {
                if (Client.Connected && value != null) 
                {
                    if (!value.Equals(Value)) 
                    {
                        if(!Writable)
                            throw new UnwriteableException(this);
                        if (Model.ModbusType == ModbusDataType.Coil)
                            Client.WriteValue(Model, GetBits(value));
                        else if(Model.ModbusType == ModbusDataType.HoldingRegister) 
                        {
                            if (Model.ValueDataType == ValueDataType.Unicode)
                                Client.WriteUnicodeValue(Model, GetUnicodeBytes(value), NumberOfPoints, value.ToString()!.Length);
                            else if (Model.ValueDataType == ValueDataType.Single || Model.ValueDataType == ValueDataType.UInt32)
                                Client.WriteSingleValue(Model, GetSingleBytes(value));
                            else
                                Client.WriteValue(Model, GetDoubleBytes(value));
                        }
                        //将value这个值写给plc之后也会将将这个值写入本地，更新Value属性
                        if (updateLocalStoreOption)
                            SetValue(value);
                    }
                }
                else
                    throw new Exception($"{Client.DeviceName}未连接!");
            }
            catch(Exception ex)
            {
                //Logger
                throw;
            }
        }

        public Task WriteValueAsync(T value)
        {
            try
            {
                if (Model.ModbusType == ModbusDataType.Coil)
                    return Client.WriteValueAsync(Model, GetBits(value));
                else if (Model.ModbusType == ModbusDataType.HoldingRegister)
                    return Client.WriteValueAsync(Model, GetDoubleBytes(value));
                return null!;
            }
            catch (Exception e)
            {
                //Logger
                throw;
            }
        }

        public override void WriteAnyValue(object value, bool updateLocalStoreOption = true)
        {
            try
            {
                if (value != null)
                    WriteValue((T)Convert.ChangeType(value, typeof(T)), updateLocalStoreOption);
                else
                    WriteValue(default!, updateLocalStoreOption);
            }
            catch(Exception ex)
            {
                //Logger
                throw;
            }
        }

        public override void WriteStringValue(string value)
        {
            WriteValue((T)Convert.ChangeType(value, typeof(T)), true);
        }

        public override void SetAnyValue(object value)
        {
            WriteStringValue(value.ToString()!);
        }

        /// <summary>
        /// 将泛型类型T的值Value转换成对应的字节数组byte[]，然后再展开为bool[](位数组)
        /// 适用于Modbus等按位通信或解析场景
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected bool[] GetBits(T value)
        {
            //优化布尔值的处理，无需展开位
            if (IsBoolType)
                return new bool[1] { (bool)Convert.ChangeType(value, typeof(bool)) };
            //BitArray用于按位存储true/false值
            //它的构造函数BitArray(byte[])会将字节数组转换为位数组
            //每个byte都会被拆解成8个二进制位，BitArray将其按位填充至数组
            var bitArray = new BitArray(ConvertToBytes(value));
            bool[] ret = new bool[bitArray.Length];
            for (int i = 0; i < bitArray.Length; i++)
                ret[i] = bitArray[i];
            return ret;
        }

        /// <summary>
        /// 所谓Double Bytes，也就是ushort
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected ushort[] GetDoubleBytes(T value)
        {
            //将值转换为字节数组
            byte[] data = ConvertToBytes(value);
            //声明用于装入Modbus数据的ushort[]（若byte[]长度为奇数，需要额外补一个寄存器装最后一个字节）
            ushort[] ret = new ushort[data.Length / 2 + data.Length % 2];
            Buffer.BlockCopy(data, 0, ret, 0, new int[] { data.Length, ret.Length * 2 }.Min());
            return ret;
        }

        protected ushort[] GetSingleBytes(T value)
        {
            byte[] data = ConvertToBytes(value);
            byte[] temp = new byte[data.Length];
            temp[0] = data[2];
            temp[1] = data[3];
            temp[2] = data[0];
            temp[3] = data[1];
            ushort[] ret = new ushort[data.Length / 2 + data.Length % 2];
            Buffer.BlockCopy(temp, 0, ret, 0, new int[] { temp.Length, ret.Length * 2 }.Min());
            return ret;
        }

        protected ushort[] GetUnicodeBytes(T value)
        {
            //先将value转换为byte[]
            var str = value.ToString();
            var data = new byte[NumberOfPoints * 2];
            Buffer.BlockCopy(Encoding.Unicode.GetBytes(str), 0, data, 0, str.Length * 2);
            //再将bytep[]转换为ushort[]
            var ret = new ushort[data.Length / 2];
            Buffer.BlockCopy(data, 0, ret, 0, ret.Length * 2);
            return ret;
        }


        /// <summary>
        /// 字节转换逻辑，此处的Convert.To我认为是一种保护机制
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected byte[] ConvertToBytes(T value)
        {
            try
            {
                if (typeof(T) == typeof(bool))
                    return GetBytes(Convert.ToBoolean(value));
                else if (typeof(T) == typeof(char))
                    return GetBytes(Convert.ToChar(value));
                else if (typeof(T) == typeof(byte))
                    return GetBytes(Convert.ToByte(value));
                else if (typeof(T) == typeof(sbyte))
                    return GetBytes(Convert.ToSByte(value));
                else if (typeof(T) == typeof(short))
                    return GetBytes(Convert.ToInt16(value));
                else if (typeof(T) == typeof(ushort))
                    return GetBytes(Convert.ToUInt16(value));
                else if (typeof(T) == typeof(int))
                    return GetBytes(Convert.ToInt32(value));
                else if (typeof(T) == typeof(uint))
                    return GetBytes(Convert.ToUInt32(value));
                else if (typeof(T) == typeof(long))
                    return GetBytes(Convert.ToInt64(value));
                else if (typeof(T) == typeof(ulong))
                    return GetBytes(Convert.ToUInt64(value));
                else if (typeof(T) == typeof(float))
                    return GetBytes(Convert.ToSingle(value));
                else if (typeof(T) == typeof(double))
                    return GetBytes(Convert.ToDouble(value));
                else if (typeof(T) == typeof(string))
                    return GetBytes(Convert.ToString(value)!);
                return null!;
            }
            catch (Exception e)
            {
                //Logger
                throw;
            }
        }

        /*
         * 对于所有数值类型，布尔，字符类型，使用BitConverter.GetBytes()生成对应的字节数组
         */

        protected virtual byte[] GetBytes(bool value) => BitConverter.GetBytes(value);
        protected virtual byte[] GetBytes(char value) => BitConverter.GetBytes(value);
        protected virtual byte[] GetBytes(short value) => BitConverter.GetBytes(value);
        protected virtual byte[] GetBytes(ushort value) => BitConverter.GetBytes(value);
        protected virtual byte[] GetBytes(int value) => BitConverter.GetBytes(value);
        protected virtual byte[] GetBytes(uint value) => BitConverter.GetBytes(value);
        protected virtual byte[] GetBytes(long value) => BitConverter.GetBytes(value);
        protected virtual byte[] GetBytes(ulong value) => BitConverter.GetBytes(value);
        protected virtual byte[] GetBytes(float value) => BitConverter.GetBytes(value);
        protected virtual byte[] GetBytes(double value) => BitConverter.GetBytes(value);
        protected virtual byte[] GetBytes(string value) => Encoding.ASCII.GetBytes(value);
    }
}
