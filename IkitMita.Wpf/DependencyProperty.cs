using System;
using System.Linq.Expressions;
using System.Windows;

namespace IkitMita.Wpf
{
    /// <summary>
    /// Helper cut down DependencyProperty registration
    /// </summary>
    /// <typeparam name="T">Type of DependencyObject which owns property</typeparam>
    /// <remarks>http://habrahabr.ru/post/149835/</remarks>
    public static class DependencyProperty<T> where T : DependencyObject
    {
        /// <summary>
        /// Register new DependencyProperty by property of exression
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="propertyExpression"></param>
        /// <returns></returns>
        public static DependencyProperty Register<TProperty>(Expression<Func<T, TProperty>> propertyExpression)
        {
            return Register(propertyExpression, default(TProperty), null);
        }

        public static DependencyProperty Register<TProperty>(Expression<Func<T, TProperty>> propertyExpression, TProperty defaultValue)
        {
            return Register(propertyExpression, defaultValue, null);
        }

        public static DependencyProperty Register<TProperty>(Expression<Func<T, TProperty>> propertyExpression, Func<T, PropertyChangedCallback<TProperty>> propertyChangedCallbackFunc)
        {
            return Register(propertyExpression, default(TProperty), propertyChangedCallbackFunc);
        }

        public static DependencyProperty Register<TProperty>(Expression<Func<T, TProperty>> propertyExpression, TProperty defaultValue, Func<T, PropertyChangedCallback<TProperty>> propertyChangedCallbackFunc)
        {
            string propertyName = propertyExpression.RetrieveMemberName();
            PropertyChangedCallback callback = ConvertCallback(propertyChangedCallbackFunc);

            return DependencyProperty.Register(propertyName, typeof(TProperty), typeof(T), new PropertyMetadata(defaultValue, callback));
        }

        private static PropertyChangedCallback ConvertCallback<TProperty>(Func<T, PropertyChangedCallback<TProperty>> propertyChangedCallbackFunc)
        {
            if (propertyChangedCallbackFunc == null)
            {
                return null;
            }

            return (d, e) =>
            {
                PropertyChangedCallback<TProperty> callback = propertyChangedCallbackFunc((T)d);
                if (callback != null)
                {
                    callback(new DependencyPropertyChangedEventArgs<TProperty>(e));
                }
            };
        }

        public delegate void PropertyChangedCallback<TProperty>(DependencyPropertyChangedEventArgs<TProperty> e);
    }
}
