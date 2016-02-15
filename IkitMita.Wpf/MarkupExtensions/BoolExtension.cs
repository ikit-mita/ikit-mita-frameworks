using System;
using System.Windows.Markup;

namespace IkitMita.Wpf
{
    [MarkupExtensionReturnType(typeof(bool))]
    public sealed class BoolExtension : MarkupExtension
    {
        public bool Value { get; }

        public BoolExtension(bool value)
        {
            Value = value;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Value;
        }
    }
}
