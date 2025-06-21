using Application.Common;
using Application.Model;
using CommonServiceLocator;

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

        public int Index {  get; set; }

        public ModbusMessage Message { get; set; }

        public ModbusRegister Model { get; set; }

        public ModbusDevice DeviceModel { get; set; }

        public string VariableName => Model.RegisterName;

        public ModbusClient Client => _client ?? ServiceLocator.Current.GetInstance<ModbusClient>(DeviceModel.DeviceUri.Trim());

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

        internal void SetSingleValue(ushort[] value, int index)
        {
            throw new NotImplementedException();
        }

        internal void SetUnicodeValue(ushort[] value, int index)
        {
            throw new NotImplementedException();
        }

        internal void SetValue(bool[] data, int index)
        {
            throw new NotImplementedException();
        }

        internal void SetValue(byte[] data, int index)
        {
            throw new NotImplementedException();
        }

        private string GetValueString()
        {
            throw new NotImplementedException();
        }

        private Type GetValueType()
        {
            throw new NotImplementedException();
        }

        private object ReadAnyValue()
        {
            throw new NotImplementedException();
        }

        private void SetAnyValue(object value)
        {
            throw new NotImplementedException();
        }
    }
}
