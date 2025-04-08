using System;
using System.Globalization;
using System.Windows.Data;

namespace PROG2500_A2_Chinook.Converters
{
    public class DurationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int milliseconds)
            {
                TimeSpan time = TimeSpan.FromMilliseconds(milliseconds);

                if (time.Hours > 0)
                    return $"{time.Hours}:{time.Minutes:D2}:{time.Seconds:D2}";
                else
                    return $"{time.Minutes}:{time.Seconds:D2}";
            }

            return "0:00";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}