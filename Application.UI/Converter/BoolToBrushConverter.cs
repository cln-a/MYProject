using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace Application.UI
{
    public class BoolToBrushConverter : IValueConverter
    {
        public Brush TrueBrush { get; set; } = Brushes.ForestGreen;
        public Brush FalseBrush { get; set; } = Brushes.Crimson;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is bool b && b) ? TrueBrush : FalseBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
