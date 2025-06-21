using Application.Common;
using Application.Model;
using CommonServiceLocator;
using System.Runtime.InteropServices;

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

        public ModbusVariable(ModbusRegister register, ModbusDevice deviceModel) : base(register, deviceModel) { }

        public event EventHandler<ValueChangedEventArgs<T>> ValueTChangedEvent;
        public event EventHandler<ValueReadedEventArgs<T>> ValueTReadedEvent;

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
