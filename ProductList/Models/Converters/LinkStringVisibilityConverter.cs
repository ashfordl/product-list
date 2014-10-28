using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ProductList.Models.Converters
{
    [ValueConversion(typeof(string), typeof(bool))]
    public class LinkStringVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Returns true if the given string is a link.
        /// </summary>
        /// <param name="value">The string to validate.</param>
        /// <returns>Whether the string is a link.</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string link = (string)value;

            // TODO Validate as a link

            return String.IsNullOrWhiteSpace(link);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
