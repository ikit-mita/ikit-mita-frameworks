using System;
using System.Globalization;

namespace IkitMita.Wpf.Converters
{
    public class NullConverter : ValueConverter
    {
        public object OnNull { get; set; }
        
        public object OnNotNull { get; set; }

        protected override object ConvertInternal(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var converted = value == null ? OnNull : OnNotNull;
            return converted;
        }

        protected override object ConvertBackInternal(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
