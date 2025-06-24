using Application.Common;
using Application.Model;

namespace Application.S7net
{
    public class S7netVariable : IVariable
    {
        public int Index { get; set; }
        public S7netMessage Message { get; set; }
        public S7netRegister RegisterModel { get; set; }
        public S7netDevice DeviceModel { get; set; }
        
        public int Id { get; }
        public string Path { get; }
        public int DbNumber { get; }
        public Type ValueType { get; }
        public object AnyValue { get; }
        public string DeviceName { get; }
        public string ValueString { get; }
        public bool IsEnabled { get; }
        public bool Readable { get; }
        public bool Writable { get; }
        public int StartAddress { get; }
        public int NumberOfPoints { get; }
        public string Description { get; }
        public bool IsConnected { get; }
        public ValueDataType DataType { get; }
        public ModbusDataType RegisterType { get; }
        
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

        public virtual void SetValue(byte[] value, int index) { }
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
