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
    /// Returns the page that that we want
    /// </summary>
    public class StringToVisiblityValueConverter : BaseValueConverters<StringToVisiblityValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "Closed")
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
