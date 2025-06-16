using Application.Common;

namespace Application.Modbus
{
    public class ModbusVariable : IVariable
    {
        public int Id => throw new NotImplementedException();

        public string Path => throw new NotImplementedException();

        public int DbNumber => throw new NotImplementedException();

        public Type ValueType => throw new NotImplementedException();

        public object AnyValue => throw new NotImplementedException();

        public string DeviceName => throw new NotImplementedException();

        public string ValueString => throw new NotImplementedException();

        public bool IsEnabled => throw new NotImplementedException();

        public bool Readable => throw new NotImplementedException();

        public bool Writable => throw new NotImplementedException();

        public int StartAddress => throw new NotImplementedException();

        public int NumberOfPoints => throw new NotImplementedException();

        public string Description => throw new NotImplementedException();

        public bool IsConnected => throw new NotImplementedException();

        public ValueDataType DataType => throw new NotImplementedException();

        public ModbusDataType RegisterType => throw new NotImplementedException();

        public event EventHandler<ValueChangedEventArgs<object>> ValueChangedEvent;
        public event EventHandler<ValueReadedEventArgs<object>> ValueReadedEvent;

        public T GetValue<T>()
        {
            throw new NotImplementedException();
        }

        public T ReadValue<T>()
        {
            throw new NotImplementedException();
        }

        public Task<T> ReadValueAsync<T>()
        {
            throw new NotImplementedException();
        }

        public void WriteAnyValue(object value, bool updateLocalStoreOption = true)
        {
            throw new NotImplementedException();
        }

        public void WriteStringValue(string value)
        {
            throw new NotImplementedException();
        }
    }
}
