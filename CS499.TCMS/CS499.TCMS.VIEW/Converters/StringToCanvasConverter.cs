using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CS499.TCMS.View.Converters
{
    [ValueConversion(typeof(string), typeof(Canvas))]
    public sealed class StringToCanvasConverter : IValueConverter
    {

        public StringToCanvasConverter()
        {

        }

        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            return Application.Current.TryFindResource(value);
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {

            throw new NotImplementedException();
        }

    }
}
