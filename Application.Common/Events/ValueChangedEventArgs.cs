namespace Application.Common
{
    public class ValueChangedEventArgs<T> : EventArgs
    {
        protected readonly IVariable _variable;
        private readonly T _oldValue;
        private readonly T _newValue;
        
        public IVariable Variable => _variable;
        public long VariableID => Variable.Id;
        public T OldValue => _oldValue;
        public T NewValue => _newValue;

        public ValueChangedEventArgs(IVariable variable, T oldValue, T newValue)
        { 
            this._variable = variable;
            this._oldValue = oldValue;
            this._newValue = newValue;
        }

        public T1? GetOldValue<T1>() => _oldValue == null ? default : (T1)Convert.ChangeType(_oldValue, typeof(T1));
        public T1? GetNewValue<T1>() => _newValue == null ? default : (T1)Convert.ChangeType(_newValue, typeof(T1));
    }

    public class ValueChangedEventArgs : ValueChangedEventArgs<object>
    {
        public ValueChangedEventArgs(IVariable variable, object oldValue, object newValue) : base(variable, oldValue, newValue) { }
    }
}
