using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CS499.TCMS.View.Converters
{
    [ValueConversion(typeof(Visibility), typeof(bool))]
    public sealed class VisibilityToBoolConverter : IValueConverter
    {
        public Visibility TrueValue { get; set; }
        public Visibility FalseValue { get; set; }

        public VisibilityToBoolConverter()
        {
            // set defaults
            TrueValue = Visibility.Visible;
            FalseValue = Visibility.Collapsed;
        }

        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (Equals(value, TrueValue))
                return true;
            if (Equals(value, FalseValue))
                return false;
            return null;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {

            if (!(value is bool))
                return null;
            return (bool)value ? TrueValue : FalseValue;

        }
    }
}
