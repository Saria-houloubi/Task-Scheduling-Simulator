
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace TishreenUniversity.ParallelPro.IValueConverters
{
    /// <summary>
    /// Base Generic class for the converters
    /// </summary>
    /// <typeparam name="T"> the type of the value that we want to convert</typeparam>
    public abstract class BaseValueConverters<T> : MarkupExtension, IValueConverter
        where T : class, new()
    {

        #region Private Members

        private static T mConverter = null;
        #endregion

        #region MarkUp Extension Methods

        /// <summary>
        /// the methods that will help us call the value converter from xaml  (Provides a static instance of the value converter)
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {          
            return mConverter ?? (mConverter = new T());
        }

        #endregion

        #region Converter Methods

        /// <summary>
        /// Convert to the wanted type method
        /// </summary>
        /// <param name="value">The value that we want to convert from</param>
        /// <param name="targetType">The type that we want to convert to</param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// Converts back the value to its origenal type
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        #endregion
    }
}
