using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace OctaSweeper.Converter
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public Visibility VisibilityOnTrue { get; set; }
        public Visibility VisibilityOnFalse { get; set; }

        /// <summary>
        /// Converts a boolean value to a preset visibility.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool) value)
                {
                    return this.VisibilityOnTrue;
                }

                return this.VisibilityOnFalse;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
