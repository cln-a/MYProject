namespace Application.Common
{
    public interface IVariable
    {
        int Id { get;}
        string Path { get; }
        int DbNumber { get; }
        Type ValueType { get; }
        object AnyValue { get; }
        string DeviceName { get; }
        string ValueString { get; }
        bool IsEnabled { get; }
        bool Readable { get; }
        bool Writable { get; }
        int StartAddress { get; }
        int NumberOfPoints { get; }
        string Description { get; }
        bool IsConnected { get; }

        Common.ValueDataType DataType { get; }
        Common.ModbusDataType RegisterType { get; }

        void WriteAnyValue(object value, bool updateLocalStoreOption = true);
        T GetValue<T>();

        void WriteStringValue(string value);
        T ReadValue<T>();
        Task<T> ReadValueAsync<T>();

        event EventHandler<ValueChangedEventArgs<object>> ValueChangedEvent;
        event EventHandler<ValueReadedEventArgs<object>> ValueReadedEvent;
    }

    public interface IVariable<T> : IVariable where T : IComparable
    {
        T Value { get; set; }
        void WriteValue(T value, bool updateLocalStoreOption = true);
        Task WriteValueAsync(T value);

        event EventHandler<ValueChangedEventArgs<T>> ValueTChangedEvent;
        event EventHandler<ValueReadedEventArgs<T>> ValueTReadedEvent;
    }

    public static class VariableExtensions
    {
        public static T? GetValueEx<T>(this IVariable variable) => variable == null ? default : variable.GetValue<T>();

        public static void WriteAnyValueEx(this IVariable variable, object value, bool updateLocalStoreOption = true)
        {
            if (variable != null && value != null) 
                variable.WriteAnyValue(value, updateLocalStoreOption);
        }
    }
}
