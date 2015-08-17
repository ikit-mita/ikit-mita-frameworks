using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace IkitMita.Wpf.Converters
{
    [ContentProperty("NextConverter")]
    public abstract class ValueConverter : IValueConverter
    {
        public IValueConverter NextConverter { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var converted = ConvertInternal(value, GetNextExpectedType() ?? targetType, parameter, culture);

            if (NextConverter != null)
            {
                converted = NextConverter.Convert(converted, targetType, parameter, culture);
            }

            return converted;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var converted = value;

            if (NextConverter != null)
            {
                converted = NextConverter.ConvertBack(converted, targetType, parameter, culture);
            }

            converted = ConvertBackInternal(converted, targetType, parameter, culture);
            return converted;
        }

        protected abstract object ConvertInternal(object value, Type targetType, object parameter, CultureInfo culture);

        protected abstract object ConvertBackInternal(object value, Type targetType, object parameter, CultureInfo culture);

        protected virtual Type GetExpectedType() { return null; }

        protected Type GetNextExpectedType()
        {
            var valueConverter = NextConverter as ValueConverter;
            return valueConverter?.GetExpectedType();
        }
    }
}
