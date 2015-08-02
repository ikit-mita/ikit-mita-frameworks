using System;
using System.Globalization;

namespace IkitMita.Wpf.Converters
{
    public class BooleanConverter : ValueConverter
    {
        protected override Type GetExpectedType()
        {
            return typeof(bool);
        }

        public object OnFalse { get; set; }

        public object OnTrue { get; set; }

        protected override object ConvertInternal(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var b = value as bool? ?? false;
            var converted = b ? OnTrue : OnFalse;
            return converted;
        }

        protected override object ConvertBackInternal(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == OnTrue;
        }
    }
}
