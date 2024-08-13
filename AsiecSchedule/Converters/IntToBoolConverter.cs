using System.Globalization;

namespace AsiecSchedule.Converters
{
    class IntToBoolConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return false;
            return (int)value != 0;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return false;
            return (bool)value ? 1 : 0;
        }
    }
}
