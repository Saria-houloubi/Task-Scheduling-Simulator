using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using ThishreenUniversity.ParallelPro.Enums.Instructions;
using TishreenUniversity.ParallelPro.Controls;
using TishreenUniversity.ParallelPro.Controls.Login;

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
