using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;

namespace Beerman006.TimeTracker.ViewModel
{
    public class HoursAndTenthsToTimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan span = (TimeSpan)value;
            double fractionalHours = (double)span.Minutes / 60d;

            if (fractionalHours > .94)
            {
                fractionalHours = .94;
            }
            else if (fractionalHours < .05 && fractionalHours > .0001)
            {
                fractionalHours = .05;
            }

            double hours = span.Hours;
            hours += fractionalHours;
            return hours.ToString("F1");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string timeString = (string)value;
            var parts = timeString.Split('.');
            int hours = int.Parse(parts[0]);
            TimeSpan span = new TimeSpan(hours, 0, 0);
            if (parts.Length > 1)
            {
                double fractionalMinutes = double.Parse(parts[1]) / 10d;
                int minutes = (int)(fractionalMinutes * 60d);
                span.Add(new TimeSpan(0, minutes, 0));
            }
            return span;
        }
    }
}
