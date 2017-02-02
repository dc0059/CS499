using System;
using System.Globalization;
using System.Windows.Data;


namespace CS499.TCMS.View.Converters
{
    [ValueConversion(typeof(TimeSpan), typeof(string))]
    public sealed class StringToTimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value == null)
            {
                return null;
            }

            return new DateTime().Add((TimeSpan)value).ToShortTimeString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value == null)
            {
                return null;
            }

            return DateTime.ParseExact((string)value, "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
        }
    }
}
