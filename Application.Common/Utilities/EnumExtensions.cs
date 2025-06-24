using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;

namespace Application.Common
{
    public static class EnumExtensions
    {
        public static ObservableCollection<EnumPair<T>> GetPairs<T>() where T : Enum
        {
            var r = new ObservableCollection<EnumPair<T>>();
            foreach (T item in Enum.GetValues(typeof(T)))
                r.Add(new EnumPair<T> { Value = item, Description = item.GetDescription() });
            return r;
        }

        public static Dictionary<string, T> GetEnumMaps<T>() where T : Enum
        {
            var r = new Dictionary<string, T>();
            foreach (T item in Enum.GetValues(typeof(T)))
            {
                var description = item.GetDescription();
                if (!r.ContainsKey(description))
                    r.Add(item.GetDescription(), item);
            }
            return r;
        }

        public static Dictionary<string, T> GetEnumNameMaps<T>() where T : Enum
        {
            var r = new Dictionary<string, T>();
            try
            {
                foreach (T item in Enum.GetValues(typeof(T)))
                {
                    var name = item.GetName();
                    if (!r.ContainsKey(name))
                        r.Add(name, item);
                }
            }
            catch (Exception e)
            {

                throw;
            }
            return r;
        }

        public static T GetValueByName<T>(this string name) where T : Enum
        {
            var maps = GetEnumNameMaps<T>();
            if (!maps.ContainsKey(name)) return maps.Values.First();
            return maps[name];
        }

        public static T GetValueByDescription<T>(this string description) where T : Enum
        {
            var maps = GetEnumMaps<T>();
            if (!maps.ContainsKey(description)) return maps.Values.First();
            return maps[description];
        }

        public static string GetName(this Enum value)
        {
            Type enumType = value.GetType();
            return Enum.GetName(enumType, value);
        }

        public static string GetDescription(this Enum value)
        {
            Type enumType = value.GetType();
            string name = Enum.GetName(enumType, value);
            if (name != null)
            {
                FieldInfo fieldInfo = enumType.GetField(name);
                if (fieldInfo != null)
                {
                    if (Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute), false) is DescriptionAttribute attr)
                        return attr.Description;
                }
            }
            return null;
        }
    }

    public class EnumPair<T> where T : Enum
    {
        public T Value { get; set; }
        public string Description { get; set; }
    }
}
