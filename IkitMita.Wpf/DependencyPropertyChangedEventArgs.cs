using System;
using System.Windows;

namespace IkitMita.Wpf
{
    public class DependencyPropertyChangedEventArgs<T> : EventArgs
    {
        public DependencyPropertyChangedEventArgs(DependencyPropertyChangedEventArgs e)
        {
            NewValue = (T)e.NewValue;
            OldValue = (T)e.OldValue;
            Property = e.Property;
        }

        public T NewValue { get; }
        public T OldValue { get; }
        public DependencyProperty Property { get; }
    }
}
