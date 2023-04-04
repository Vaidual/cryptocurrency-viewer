using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace cryptocurrency_viewer.Converters
{
    public class AbbreviatedNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal decimalValue)
            {
                if (decimalValue >= 1_000_000_000)
                {
                    return string.Format(culture, "{0:F2}b", decimalValue / 1_000_000_000);
                }
                else if (decimalValue >= 1_000_000)
                {
                    return string.Format(culture, "{0:F2}m", decimalValue / 1_000_000);
                }
                else
                {
                    return string.Format(culture, "{0:F2}", decimalValue);
                }
            }
            else
            {
                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
