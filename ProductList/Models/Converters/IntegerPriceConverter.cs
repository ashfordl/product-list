using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ProductList.Models.Converters
{
    [ValueConversion(typeof(int), typeof(String))]
    public class IntegerPriceConverter : IValueConverter
    {
        /// <summary>
        /// Converts a given pence integer to a string of price in pounds.
        /// </summary>
        /// <param name="value">The integer to convert.</param>
        /// <returns>A string of the price in pounds.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return "";
            }

            double val = (double)(int)value / 100;
            return "£" + val.ToString();
        }

        /// <summary>
        /// Converts a given price string to a integer of pennies.
        /// </summary>
        /// <param name="value">The price string to convert.</param>
        /// <returns>The price integer in pennies.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || String.IsNullOrWhiteSpace((string)value))
            {
                return null;
            }

            // Remove £ symbol
            string val = (string)value;

            if (val.StartsWith("£"))
            {
                val = val.Substring(1);
            }

            double price;
            if (double.TryParse(val, out price))
            {
                return (int)(price * 100);
            }
            else
            {
                throw new ArgumentException("Given price string is invalid.");
            }
        }
    }
}
