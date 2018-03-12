using System;
using System.Diagnostics;
using System.Globalization;
using ThishreenUniversity.ParallelPro.Enums;
using TishreenUniversity.ParallelPro.Controls;

namespace TishreenUniversity.ParallelPro.IValueConverters
{
    /// <summary>
    /// Returns the page that that we want
    /// </summary>
    public class ApplicationPageValueConverter : BaseValueConverters<ApplicationPageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((ApplicationPages)value)
            {
                case ApplicationPages.MainWindow:
                    return new MainWindowInstructionAndAlgoControl();
                case ApplicationPages.LoginPage:
                    return new LoginUserControl();
                case ApplicationPages.AdminSettings:
                    return new AdminSettingsControl();
                default:
                    Debugger.Break();
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
