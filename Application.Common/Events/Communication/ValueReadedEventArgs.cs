namespace Application.Common
{
    public class ValueReadedEventArgs<T> : EventArgs
    {
        private readonly T _value;
        protected readonly IVariable _variable;

        public T Value => _value;
        public IVariable Variable => _variable;
        public long VariableID => Variable.Id;

        public ValueReadedEventArgs(IVariable variable, T value)
        {
            this._variable = variable;
            this._value = value;
        }

        public T1? GetValue<T1>() => _value == null ? default : (T1)Convert.ChangeType(_value, typeof(T1));
    }

    public class ValueReadedEventArgs : ValueReadedEventArgs<object>
    {
        public ValueReadedEventArgs(IVariable variable, object value) : base(variable, value) { }
    }
}
