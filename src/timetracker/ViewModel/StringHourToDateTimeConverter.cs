using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;

namespace Beerman006.TimeTracker.ViewModel
{
    public class StringHourToDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime time = (DateTime)value;
            if (time == DateTime.MinValue)
            {
                return string.Empty;
            }
            return time.ToShortTimeString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime converted;
            string stringTime = (string)value;
            var formats = new[] { "t", "%H", "HH", "h tt", "h:m" };
            var styles = DateTimeStyles.NoCurrentDateDefault;
            if (!DateTime.TryParseExact(stringTime, formats, culture, styles, out converted))
            {
                converted = new DateTime();
            }
            return converted;
        }
    }
}
