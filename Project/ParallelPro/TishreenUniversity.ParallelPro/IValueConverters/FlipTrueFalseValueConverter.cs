using System;
using System.Globalization;

namespace TishreenUniversity.ParallelPro.IValueConverters
{
    /// <summary>
    /// Flips the value from to true to false and the orhter way around
    /// </summary>
    public class FlipTrueFalseValueConverter : BaseValueConverters<FlipTrueFalseValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)value;
        }
    }
}
