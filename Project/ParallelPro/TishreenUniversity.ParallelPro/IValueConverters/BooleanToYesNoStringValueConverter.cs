using System;
using System.Globalization;
using System.Windows;

namespace TishreenUniversity.ParallelPro.IValueConverters
{
    /// <summary>
    /// Return Yes if the value is true and No if the value is false
    /// </summary>
    public class BooleanToYesNoStringValueConverter : BaseValueConverters<BooleanToYesNoStringValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return "Yes";
            else
                return "No";
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
