using System;
using System.Globalization;
using System.Windows.Media;
using ThishreenUniversity.ParallelPro.Enums;
namespace TishreenUniversity.ParallelPro.IValueConverters
{

    /// <summary>
    /// A converter the <see cref="ApplicationIcons"/> to an actual icon
    /// </summary>
    public class ApplicationIconValueConverter : BaseValueConverters<ApplicationIconValueConverter>
    {
        #region Converter Methods
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Geometry.Parse((string)(Icons.GetGeometry((ApplicationIcons)value)));
        }
        
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion

     }
}
