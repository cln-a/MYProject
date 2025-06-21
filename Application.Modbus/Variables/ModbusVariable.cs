using Application.Common;
using Application.Model;
using CommonServiceLocator;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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

        public Task<T> ReadValueAsync<T>() => default!;

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

    public class ModbusVariable<T> : ModbusVariable, IVriable<T> where T : IComparable
    {
        private T _value;

        public T Value { get => _value; set => _value = value; }

        public bool IsBoolType => typeof(bool) == typeof(T);

        public int TypeSize => IsBoolType ? 1 : Marshal.SizeOf<T>();

        public event EventHandler<ValueChangedEventArgs<T>> ValueTChangedEvent;
        public event EventHandler<ValueReadedEventArgs<T>> ValueTReadedEvent;

        public ModbusVariable(ModbusRegister register, ModbusDevice deviceModel) : base(register, deviceModel) { }

        /// <summary>
        /// 将Value强制转换成泛型类型T，并返回这个值
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        public override T1 GetValue<T1>() => (T1)Convert.ChangeType(Value,typeof(T1));

        public override T1 ReadValue<T1>()
        {
            ReadValueAsync();
            return (T1)Convert.ChangeType(Value, typeof(T1));
        }

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

        public void WriteValue(T value, bool updateLocalStoreOption = true)
        {
            throw new NotImplementedException();
        }

        public void WriteValueAsync(T value)
        {
            throw new NotImplementedException();
        }
    }
}
