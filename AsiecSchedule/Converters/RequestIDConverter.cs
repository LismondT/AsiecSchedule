using AsiecSchedule.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsiecSchedule.Converters
{
    class RequestIDConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (AppSettings.RequestID == string.Empty)
                return "Группа не назначена";

            return AppSettings.RequestID;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return AppSettings.RequestID;
        }
    }
}
