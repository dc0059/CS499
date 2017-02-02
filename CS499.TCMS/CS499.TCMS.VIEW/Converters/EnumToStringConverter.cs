using System;
using System.Globalization;
using System.Windows.Data;

namespace CS499.TCMS.View.Converters
{
    [ValueConversion(typeof(Enum), typeof(string))]
    public sealed class EnumToStringConverter : IValueConverter
    {

        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return value.ToString().Replace('_', ' ');
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {

            Enum returnValue = (Enum)Enum.Parse(targetType, ((string)value).Replace(' ', '_'), true);

            return returnValue;
        }
    }
}
