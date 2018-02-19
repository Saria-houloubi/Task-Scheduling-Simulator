using System;
using System.Globalization;
using System.Windows;

namespace TishreenUniversity.ParallelPro.IValueConverters
{
    /// <summary>
    /// Flips value from visible to hiden and so on
    /// </summary>
    public class FlipVisibilityValueConverter : BaseValueConverters<FlipVisibilityValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((Visibility)value == Visibility.Hidden)
                return Visibility.Visible;
            else
                return Visibility.Hidden;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
