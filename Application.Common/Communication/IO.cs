namespace Application.Common
{
    public static class IO
    {
        public static void Add(string key, IVariable variable) => IOManager.Instance.Add(key, variable);
        public static bool TryGet(string key, out IVariable variable) => IOManager.Instance.TryGet(key, out variable);
        public static bool TryGet<T>(string key, out T variable) where T : class, IVariable => IOManager.Instance.TryGet<T>(key, out variable);
        public static T GetValue<T>(string key) => IOManager.Instance.GetValue<T>(key);
        public static void SetValue<T>(string key, T value) => IOManager.Instance.SetValue(key, value);
    }

    public class IOManager
    {
        private Dictionary<string, IVariable> _variables = new Dictionary<string, IVariable>();

        public static readonly Lazy<IOManager> IOManagerInstance = new Lazy<IOManager>(() => new IOManager());

        public static IOManager Instance => IOManagerInstance.Value;

        private IOManager() { }

        public void Add(string key, IVariable variable) => _variables[key] = variable;

        public bool TryGet(string key, out IVariable variable)
        {
            if (!string.IsNullOrEmpty(key))
                return _variables.TryGetValue(key, out variable!);
            variable = null!;
            return false;
        }

        public bool TryGet<T>(string key,out T variable) where T:class, IVariable
        {
            if (_variables.TryGetValue(key, out var result)) 
            {
                variable = (result as T)!;
                return variable != null;
            }
            variable = null!;
            return false;
        }

        public T GetValue<T>(string key)
        {
            if (_variables.TryGetValue(key, out var variable)) 
            {
                return variable.GetValue<T>();
            }
            return default!;
        }

        public void SetValue<T>(string key,T value)
        {
            if (_variables.TryGetValue(key, out var variable))
                variable.WriteAnyValue(value!, true); 
        }
    }
}
