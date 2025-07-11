using System.Globalization;
using System.Windows.Data;

namespace Application.UI
{
    public class UshortToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is ushort u && u != 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is bool b && b) ? (ushort)1 : (ushort)0;
        }
    }
}
