using System.Globalization;
using System.Windows.Data;

namespace Application.RussiaUI
{
    public class UShortToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is ushort ushortVal) && ushortVal == 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is bool boolVal) && boolVal ? (ushort)1 : (ushort)0;
        }
    }
}
