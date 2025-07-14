using Application.Common;
using Application.Model;
using CommonServiceLocator;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;

namespace Application.Modbus
{
    public class ModbusVariable : IVariable
    {
        public int Id => Model == null ? 0 : Model.Id;
        public string Path => Model?.RegisterUri;
        public ValueDataType DataType => Model.ValueDataType;
        public int Index { get; set; }
        public ModbusMessage Message { get; set; }
        public ModbusRegister Model { get; set; }
        public ModbusDevice DeviceModel { get; set; }
        [Dependency] public ILogger Logger { get; set; }
        public ModbusClient Client => _client ?? ServiceLocator.Current.GetInstance<ModbusClient>(DeviceModel.DeviceUri.Trim());
        public string ValueString => GetValueString();
        public string Description => Model?.Description;
        public Type ValueType => GetValueType();
        public string DeviceName => DeviceModel?.DeviceName;
        public bool IsEnabled => Model?.IsEnabled == true;
        public bool Readable => Model?.Readable == true;
        public bool Writeable => Model?.Writeable == true;
        public int StartAddress => Model == null ? 0 : Model.StartAddress;
        public int NumberOfPoints => Model == null ? 0 : Model.NumberOfPoints;
        public ModbusDataType RegisterType => Model == null ? ModbusDataType.HoldingRegister : Model.ModbusType;
        public object AnyValue { get => ReadAnyValue(); set => SetAnyValue(value); }
        public string VariableName => Model?.RegisterName;
        public bool IsConnected => Client?.Connected == true;
        public int DbNumber => 0; 

        public bool Writable => throw new NotImplementedException();

        public event EventHandler<ValueReadedEventArgs<object>> ValueReadedEvent;

        public event EventHandler<ValueChangedEventArgs<object>> ValueChangedEvent;

        public ModbusVariable(ModbusRegister register, ModbusDevice deviceModel)
        {
            Model = register;
            DeviceModel = deviceModel;
            Logger = ServiceLocator.Current.GetInstance<ILogger>();
        }

        public virtual Type GetValueType() => typeof(object);

        public virtual string GetValueString() => "";

        public virtual void SetValue(bool[] data, int index) { }

        public virtual void SetValue(byte[] data, int index) { }

        public virtual void SetUnicodeValue(ushort[] data, int index) { }

        public virtual void SetSingleValue(ushort[] data, int index) { }

        public virtual void WriteStringValue(string value) { }

        public virtual void WriteAnyValue(object value, bool updateLocalStoreOption = true) { }

        public virtual T GetValue<T>() => default;

        public virtual T ReadValue<T>() => default;

        Task<T> IVariable.ReadValueAsync<T>() => default;
        public virtual object ReadAnyValue() => default;
        public virtual void SetAnyValue(object value) { }

        protected void PublishChangedEvent(object oldVal, object newVal)
        {
            try
            {
                ValueChangedEvent?.Invoke(this, new ValueChangedEventArgs<object>(this, oldVal, newVal));
            }
            catch (Exception e)
            {
                Logger.LogError( e.Message);
            }
        }

        protected void PublishReadedEvent(object val)
        {
            try
            {
                ValueReadedEvent?.Invoke(this, new ValueReadedEventArgs<object>(this, val));
            }
            catch (Exception e)
            {
                Logger.LogError( e.Message);
            }
        }

        private ModbusClient _client;

    }

    public class ModbusVariable<T> : ModbusVariable, IVariable<T> where T : IComparable
    {
        public T Value { get => _value; set => _value = value; }
        public bool IsBoolType => typeof(bool) == typeof(T);
        public int TypeSize => IsBoolType ? 1 : Marshal.SizeOf<T>();

        public event EventHandler<ValueChangedEventArgs<T>> ValueTChangedEvent;
        public event EventHandler<ValueReadedEventArgs<T>> ValueTReadedEvent;

        public ModbusVariable(ModbusRegister register,
            ModbusDevice deviceModel) : base(register, deviceModel) { }

        public override T1 GetValue<T1>()
        {
            return (T1)Convert.ChangeType(Value, typeof(T1));
        }
        public override T1 ReadValue<T1>()
        {
            ReadValueAsync();
            return (T1)Convert.ChangeType(Value, typeof(T1));
        }

        public override Type GetValueType()
        {
            return typeof(T);
        }

        public override string GetValueString()
        {
            return Value?.ToString();
        }

        public override void SetValue(bool[] data, int index)
        {
            try
            {
                var b_data = new byte[Model.NumberOfPoints / 8 + (Model.NumberOfPoints % 8 == 0 ? 0 : 1)];
                for (int i = 0; i < b_data.Length; i++)
                {
                    b_data[i] = 0;
                    for (int j = 0; j < data.Length && j < 8 && index + i * 8 + j < data.Length; j++)
                    {
                        if (data[index + i * 8 + j])
                            b_data[i] |= (byte)(1 << j);
                    }
                }
                if (typeof(T) == typeof(string))
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
            catch (Exception e)
            {
                Logger.LogError( e.Message);
            }
        }

        public override void SetUnicodeValue(ushort[] data, int index)
        {
            var tempData = new byte[Model.NumberOfPoints * 2];
            Buffer.BlockCopy(data, Index * 2, tempData, 0, Model.NumberOfPoints * 2);
            SetByteValue(tempData);
        }

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

        public override void SetValue(byte[] data, int index)
        {
            try
            {
                if (typeof(T) == typeof(string))
                {
                    var tempData = new byte[Model.NumberOfPoints * 2];
                    Buffer.BlockCopy(data, index, tempData, 0, new int[] { tempData.Length, data.Length - index }.Min());
                    SetByteValue(tempData);
                }
                else
                {
                    var tempData = new byte[TypeSize];
                    int copyLength = new int[] { tempData.Length, Model.NumberOfPoints * 2, data.Length - index }.Where(x => x != 0).Min();
                    if (copyLength > 0)
                    {
                        Buffer.BlockCopy(data, index, tempData, 0, copyLength);
                        SetByteValue(tempData);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.LogError( e.Message);
            }
        }

        public override object ReadAnyValue()
        {
            return Value;
        }

        public override void SetAnyValue(object value)
        {
            WriteStringValue(value.ToString());
        }

        public override void WriteStringValue(string value)
        {
            WriteValue((T)Convert.ChangeType(value, typeof(T)));
        }

        public override void WriteAnyValue(object value, bool updateLocalStoreOption = true)
        {
            try
            {
                if (value != null)
                    WriteValue((T)Convert.ChangeType(value, typeof(T)), updateLocalStoreOption);
                else
                    WriteValue(default, updateLocalStoreOption);
            }
            catch (Exception e)
            {
                Logger.LogError( e.Message);
                throw;
            }
        }

        public async void ReadValueAsync()
        {
            try
            {
                if (Model.ModbusType == ModbusDataType.Coil)
                {
                    bool[] date = await Client.ReadMultipleCoilsAsync(Model);
                    SetValue(date, 0);
                }
                else if (Model.ModbusType == ModbusDataType.HoldingRegister)
                {
                    byte[] temp = new byte[Model.NumberOfPoints * 2];
                    ushort[] date = await Client.ReadHoldingRegistersAsync(Model);
                    Buffer.BlockCopy(date, 0, temp, 0, temp.Length);
                    SetValue(temp, 0);
                }
            }
            catch (Exception e)
            {
                Logger.LogError( e.Message);
                throw;
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
                        if (!Writeable)
                            throw new UnwriteableException(this);
                        if (Model.ModbusType == ModbusDataType.Coil)
                            Client.WriteValue(Model, GetBits(value));
                        else if (Model.ModbusType == ModbusDataType.HoldingRegister)
                        {
                            if (Model.ValueDataType == ValueDataType.Unicode)
                                Client.WriteUnicodeValue(Model, GetUnicodeBytes(value), NumberOfPoints, value.ToString().Length);
                            else if (Model.ValueDataType == ValueDataType.Single || Model.ValueDataType == ValueDataType.UInt32)
                                Client.WriteSingleValue(Model, GetSingleBytes(value));
                            else
                                Client.WriteValue(Model, GetDoubleBytes(value));
                        }
                        if (updateLocalStoreOption)
                            SetValue(value);
                    }
                }
                else
                    throw new Exception($"PLC设备未连接：设备名称---{Client.DeviceName}");
            }
            catch (Exception e)
            {
                Logger?.LogError( e.Message);
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
                return null;
            }
            catch (Exception e)
            {
                Logger.LogError( e.Message);
                throw;
            }
        }

        protected ushort[] GetUnicodeBytes(T value)
        {
            var str = value.ToString();
            var data = new byte[NumberOfPoints * 2];
            Buffer.BlockCopy(Encoding.Unicode.GetBytes(str), 0, data, 0, str.Length * 2);
            var ret = new ushort[data.Length / 2];
            Buffer.BlockCopy(data, 0, ret, 0, ret.Length * 2);
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

        protected ushort[] GetDoubleBytes(T value)
        {
            byte[] data = ConvertToBytes(value);
            ushort[] ret = new ushort[data.Length / 2 + data.Length % 2];
            Buffer.BlockCopy(data, 0, ret, 0, new int[] { data.Length, ret.Length * 2 }.Min());
            return ret;
        }

        protected bool[] GetBits(T value)
        {
            if (IsBoolType)
                return new bool[1] { (bool)Convert.ChangeType(value, typeof(bool)) };
            var bitArray = new BitArray(ConvertToBytes(value));
            bool[] ret = new bool[bitArray.Length];
            for (int i = 0; i < bitArray.Length; i++)
                ret[i] = bitArray[i];
            return ret;
        }

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
                    return GetBytes(Convert.ToString(value));
                return null;
            }
            catch (Exception e)
            {
                Logger.LogError( e.Message);
                throw e;
            }
        }

        protected virtual void SetByteValue(byte[] data)
        {
            try
            {
                if (typeof(T) == typeof(string))
                    SetValue(ConvertTo(ToString(data)));
                else
                {
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
                Logger.LogError( e.Message);
                throw e;
            }
        }

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

        protected T ConvertTo<T2>(T2 newValue)
        {
            try
            {
                return (T)Convert.ChangeType(newValue, typeof(T));
            }
            catch (Exception e)
            {
                Logger?.LogError( e.Message);
            }
            return default;
        }

        protected void SetValue(T newValue)
        {
            try
            {
                T oldVlaue = default;
                if (_value is ICloneable cloneable)
                    oldVlaue = (T)cloneable.Clone();
                else
                    oldVlaue = Value;
                _value = newValue;
                PublishReadedEvent(newValue);
                if (oldVlaue != null)
                {
                    if (oldVlaue.CompareTo(newValue) != 0)
                        PublishChangedEvent(oldVlaue, newValue);
                }
                else
                    PublishChangedEvent(oldVlaue, newValue);
            }
            catch (Exception e)
            {
                Logger.LogError( e.Message);
            }
        }

        protected void PublishChangedEvent(T oldVal, T newVal)
        {
            try
            {
                ValueTChangedEvent?.Invoke(this, new ValueChangedEventArgs<T>(this, oldVal, newVal));
                base.PublishChangedEvent(oldVal, newVal);
            }
            catch (Exception e)
            {
                Logger.LogError( e.Message);
            }
        }

        protected void PublishReadedEvent(T val)
        {
            try
            {
                ValueTReadedEvent?.Invoke(this, new ValueReadedEventArgs<T>(this, val));
            }
            catch (Exception e)
            {
                Logger.LogError( e.Message);
            }
        }

        private T _value;
    }
}
