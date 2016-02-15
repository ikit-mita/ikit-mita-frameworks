using System;
using JetBrains.Annotations;

namespace IkitMita.Mvvm.ViewModels
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public sealed class DependsOnAttribute : Attribute
    {
        public string PropertyName { get; }

        public DependsOnAttribute([InvokerParameterName] string propertyName)
        {
            PropertyName = propertyName;
        }
    }
}
