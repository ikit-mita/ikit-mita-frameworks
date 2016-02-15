using System;
using System.Windows;
using System.Windows.Markup;

namespace IkitMita.Wpf
{
    [MarkupExtensionReturnType(typeof(Visibility))]
    public class VisibilityExtension : MarkupExtension
    {
        public Visibility Value { get; }

        public VisibilityExtension(Visibility value)
        {
            Value = value;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Value;

        }
    }
}
