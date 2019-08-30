using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
namespace PPS.UI.Shared.IValueConverters
{
    /// <summary>
    /// Returns the page that that we want
    /// </summary>
    public class BooleanToBrushValueConverter : BaseValueConverters<BooleanToBrushValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return new SolidColorBrush(Colors.Red);
            else
                return new SolidColorBrush(Colors.Transparent);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
