using System;
using System.Globalization;
using System.Windows;

namespace TishreenUniversity.ParallelPro.IValueConverters
{
    /// <summary>
    /// Boolean to visiblity value converter
    /// </summary>
    public class BooleanToVisibilityValueConverter : BaseValueConverters<BooleanToVisibilityValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty((string)parameter))
                if ((bool)value)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            else
                if ((bool)value)
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;

        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
