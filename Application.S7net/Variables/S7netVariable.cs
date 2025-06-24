using Application.Common;
using Application.Model;
using CommonServiceLocator;
using System.Data.OleDb;
using System.Windows.Automation;

namespace Application.S7net
{
    public class S7netVariable : IVariable
    {
        private S7netClient _client;

        public int Index { get; set; }
        public S7netMessage Message { get; set; }
        public S7netRegister RegisterModel { get; set; }
        public S7netDevice DeviceModel { get; set; }
        public S7netClient Client => _client ?? ServiceLocator.Current.GetInstance<S7netClient>(DeviceModel.DeviceUri.Trim());
        public int Id => RegisterModel == null ? 0 : RegisterModel.Id;
        public string Path => RegisterModel?.RegisterUri!;
        public int DbNumber => RegisterModel.DbNumber;
        public Type ValueType => GetValueType();
        public object AnyValue { get => ReadAnyValue(); set => SetAnyValue(value); }
        public string DeviceName => DeviceModel.DeviceName;
        public string ValueString => GetValueString();
        public bool IsEnabled => RegisterModel.IsEnabled;
        public bool Readable => RegisterModel.Readable == true;
        public bool Writable => RegisterModel.Writeable == true;
        int IVariable.StartAddress => RegisterModel.StartAddress;
        public ushort StartAddress => RegisterModel == null ? (ushort)0 : RegisterModel.StartAddress;
        public int NumberOfPoints => RegisterModel == null ? 0 : RegisterModel.NumberOfPoints;
        public string Description => RegisterModel.Description;
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
        public T Value { get; set; }
        
        public event EventHandler<ValueChangedEventArgs<T>>? ValueTChangedEvent;
        public event EventHandler<ValueReadedEventArgs<T>>? ValueTReadedEvent;
        
        public S7netVariable(S7netRegister register, S7netDevice device) : base(register, device) { }
        
        public void WriteValue(T value, bool updateLocalStoreOption = true) { }
        public Task WriteValueAsync(T value) => null!;
    }
}
