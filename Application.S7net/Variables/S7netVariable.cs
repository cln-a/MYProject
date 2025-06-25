using Application.Common;
using Application.Model;
using CommonServiceLocator;
using System.Runtime.InteropServices;
using System.Text;

namespace Application.S7net
{
    public class S7netVariable : IVariable
    {
        private S7netClient _client;

        public int Index { get; set; }
        public S7netMessage Message { get; set; }
        public S7netRegister RegisterModel { get; set; }
        public S7netDevice DeviceModel { get; set; }
        public S7netClient Client => _client ?? ServiceLocator.Current.GetInstance<S7netClient>(DeviceModel.DeviceUri!.Trim());
        public int Id => RegisterModel == null ? 0 : RegisterModel.Id;
        public string Path => RegisterModel?.RegisterUri!;
        public int DbNumber => RegisterModel.DbNumber;
        public Type ValueType => GetValueType();
        public object AnyValue { get => ReadAnyValue(); set => SetAnyValue(value); }
        public string DeviceName => DeviceModel.DeviceName!;
        public string ValueString => GetValueString();
        public bool IsEnabled => RegisterModel.IsEnabled;
        public bool Readable => RegisterModel.Readable == true;
        public bool Writable => RegisterModel.Writeable == true;
        int IVariable.StartAddress => RegisterModel.StartAddress;
        public ushort StartAddress => RegisterModel == null ? (ushort)0 : RegisterModel.StartAddress;
        public int NumberOfPoints => RegisterModel == null ? 0 : RegisterModel.NumberOfPoints;
        public string Description => RegisterModel.Description!;
        public bool IsConnected => Client?.Connected == true;
        public ValueDataType DataType => RegisterModel.ValueDataType;
        public ModbusDataType RegisterType => ModbusDataType.Input;
        public string VariableName => RegisterModel.RegisterName;


        public event EventHandler<ValueChangedEventArgs<object>>? ValueChangedEvent;
        public event EventHandler<ValueReadedEventArgs<object>>? ValueReadedEvent;

        public S7netVariable(S7netRegister register,S7netDevice device)
        {
            this.RegisterModel = register;
            this.DeviceModel = device;
        }
        
        public virtual T GetValue<T>() => default!;
        public virtual T ReadValue<T>() => default!;
        public virtual Task<T> ReadValueAsync<T>() => null!;
        public virtual void WriteAnyValue(object value, bool updateLocalStoreOption = true) { }
        public virtual void WriteStringValue(string value) { }
        public virtual void SetValue(bool[] value, int index) { }
        public virtual void SetValue(byte[] value, int index) { }
        public virtual string GetValueString() => default!;
        public virtual Type GetValueType() => default!;
        public virtual object ReadAnyValue() => default!;
        public virtual void SetAnyValue(object value) { }

        protected void PublishChangedEvent(object oldvalue,object newvalue)
        {
            try
            {
                ValueChangedEvent?.Invoke(this, new ValueChangedEventArgs<object>(this, oldvalue, newvalue));
            }
            catch(Exception e)
            {
                //Logger
            }
        }

        protected void PublishReadedEvent(object value)
        {
            try
            {
                ValueReadedEvent?.Invoke(this, new ValueReadedEventArgs(this, value));
            }
            catch (Exception e)
            {
                //Logger
            }
        }
    }

    public class S7netVariable<T> : S7netVariable, IVariable<T> where T : IComparable
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

        public S7netVariable(S7netRegister register, S7netDevice device) : base(register, device)
        {
        }


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
            return (T1)Convert.ChangeType(Value, typeof(T1));
        }

        public override Type GetValueType() => typeof(T);

        public override string GetValueString() => Value?.ToString()!;

        public override object ReadAnyValue() => Value;

        public override void SetValue(byte[] data, int index)
        {
            try
            {
                if (typeof(T) == typeof(string))
                {
                    var tempData = new byte[RegisterModel.NumberOfPoints]; //数据块以字节为单位
                    Buffer.BlockCopy(data, index, tempData, 0, RegisterModel.NumberOfPoints);
                    SetByteValue(tempData);
                }
                else
                {
                    var tempData = new byte[TypeSize];
                    Buffer.BlockCopy(data, index, tempData, 0, TypeSize);
                    SetByteValue(tempData);
                }
            }
            catch (Exception ex)
            {
                //Logger
            }
        }

        private void SetByteValue(byte[] data)
        {
            try
            {
                //(1) 第0字节：是MaxLen，PLC设计的时候就已经设定，一般不改
                //(2) 第1字节：是CurLen，告诉PLC字符串当前长度是多少（必须写这个）
                //(3) 第2字节：才是字符串的内容
                if (typeof(T) == typeof(string))
                {
                    if (data.Length > 2)
                    {
                        byte valueBytesCount = data[1];
                        if (valueBytesCount > 0)
                        {
                            byte[] remainingData = new byte[valueBytesCount];
                            Array.Copy(data, 2, remainingData, 0, remainingData.Length);
                            SetValue(ConvertTo(Encoding.UTF8.GetString(remainingData)));
                        }
                        else
                            SetValue(ConvertTo(string.Empty));
                    }
                    else
                        SetValue(ConvertTo(string.Empty));
                }
                else
                {
                    Array.Reverse(data);
                    if (typeof(T) == typeof(bool))
                        SetValue(ConvertTo(ToBoolean(data)));
                    else if (typeof(T) == typeof(char))
                        SetValue(ConvertTo((char)data[0]));
                    else if (typeof(T) == typeof(byte))
                        SetValue(ConvertTo(data[0]));
                    else if (typeof(T) == typeof(sbyte))
                        SetValue(ConvertTo((sbyte)data[0]));
                    else if (typeof(T) == typeof(short))
                        SetValue(ConvertTo(ToInt16(data)));
                    else if (typeof(T) == typeof(ushort))
                        SetValue(ConvertTo(ToUInt16(data)));
                    else if (typeof(T) == typeof(int))
                        SetValue(ConvertTo(ToInt32(data)));
                    else if (typeof(T) == typeof(uint))
                        SetValue(ConvertTo(ToUInt32(data)));
                    else if (typeof(T) == typeof(long))
                        SetValue(ConvertTo(ToInt64(data)));
                    else if (typeof(T) == typeof(ulong))
                        SetValue(ConvertTo(ToUInt64(data)));
                    else if (typeof(T) == typeof(float))
                        SetValue(ConvertTo(ToSingle(data)));
                    else if (typeof(T) == typeof(double))
                        SetValue(ConvertTo(ToDouble(data)));
                    else
                        throw new NotSupportedException("");
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        protected T ConvertTo<T2>(T2 newValue)
        {
            try
            {
                return (T)Convert.ChangeType(newValue, typeof(T))!;
            }
            catch (Exception ex)
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
            catch (Exception ex)
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
                        if (!Writable)
                            throw new UnwriteableException(this);
                        if (DataType == ValueDataType.Unicode)
                            Client.WriteValue(RegisterModel, GetUnicodeBytes(value));
                        else if (DataType == ValueDataType.Int16 || DataType == ValueDataType.UInt16 || DataType == ValueDataType.UInt32 || DataType == ValueDataType.Int32 || DataType == ValueDataType.Single)
                            Client.WriteValue(RegisterModel, GetReverseBytes(value));
                        else if (DataType == ValueDataType.Ascii)
                            Client.WriteValueForString(RegisterModel, ConvertToBytes(value));
                        else
                            Client.WriteValue(RegisterModel, ConvertToBytes(value));
                        if (updateLocalStoreOption)
                            SetValue(value);
                    }
                }
                else
                    throw new Exception($"{Client.DeviceName}未连接!");
            }
            catch (Exception ex)
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
            catch (Exception ex)
            {
                //Logger
                throw;
            }
        }

        public Task WriteValueAsync(T value) => null!;

        public override void WriteStringValue(string value)
        {
            WriteValue((T)Convert.ChangeType(value, typeof(T)), true);
        }

        public override void SetAnyValue(object value)
        {
            WriteStringValue(value.ToString()!);
        }

        /// <summary>
        /// 将泛型转换为Unicode编码的字节数组
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected byte[] GetUnicodeBytes(T value)
        {
            //将value转化为字符串
            var str = value.ToString();
            //为unicode字节数组分配空间，Unicode（UTF-16）中每个字符占 2 字节，所以 Length * 2
            var data = new byte[str.Length * 2];
            Buffer.BlockCopy(Encoding.Unicode.GetBytes(str), 0, data, 0, str.Length * 2);
            return data;
        }

        /// <summary>
        /// 此处是为了解决大小端问题
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected byte[] GetReverseBytes(T value)
        {
            byte[] data = ConvertToBytes(value);
            Array.Reverse(data);
            return data;
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
