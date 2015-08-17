using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace IkitMita
{
    /// <summary>
    /// Extension methods for IDictionary[TKey, TValue] interface.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        ///     Returns stored value if <paramref name="key"/> exists in dictionary.
        ///     Otherwise add <paramref name="@default"/> to dictionary and return it.
        /// </summary>
        /// <typeparam name="TKey">
        ///     The type of keys in the dictionary.
        /// </typeparam>
        /// <typeparam name="TValue">
        ///     The type of values in the dictionary. By default using default value fot type <typeparamref name="TValue"/>
        /// </typeparam>
        /// <param name="dictionary">
        ///     Dictionary instance from with getting value.
        /// </param>
        /// <param name="key">
        ///     The key of the element to get.
        /// </param>
        /// <param name="default">
        ///     Value that adds and returns if dictionary doesn't have specified <paramref name="key"/>.
        /// </param>
        /// <returns>
        ///     The element with the specified <paramref name="key"/> or <paramref name="@default"/> 
        ///     if key does not exists in dictionary.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     <paramref name="key"/> is null.
        /// </exception>
        public static TValue GetOrAdd<TKey, TValue>([NotNull]this IDictionary<TKey, TValue> dictionary, TKey key, TValue @default = default(TValue))
        {
            return dictionary.GetOrAdd(key, k => @default);
        }

        public static TValue GetOrAdd<TKey, TValue>([NotNull]this IDictionary<TKey, TValue> dictionary, TKey key, [NotNull]Func<TValue> getDefault)
        {
            getDefault = Check.NotNull(getDefault, nameof(getDefault));
            return dictionary.GetOrAdd(key, k => getDefault());
        }

        public static TValue GetOrAdd<TKey, TValue>([NotNull]this IDictionary<TKey, TValue> dictionary, TKey key, [NotNull]Func<TKey, TValue> getDefault)
        {
            dictionary = Check.NotNull(dictionary, nameof(dictionary));
            getDefault = Check.NotNull(getDefault, nameof(getDefault));
            TValue value;

            if (!dictionary.TryGetValue(key, out value))
            {
                value = getDefault(key);
                dictionary.Add(key, value);
            }

            return value;
        }

        /// <summary>
        ///     Returns stored value if <paramref name="key"/> exists in dictionary.
        ///     Otherwise returns <paramref name="@default"/>.
        /// </summary>
        /// <typeparam name="TKey">
        ///     The type of keys in the dictionary.
        /// </typeparam>
        /// <typeparam name="TValue">
        ///     The type of values in the dictionary. By default using default value fot type <typeparamref name="TValue"/>
        /// </typeparam>
        /// <param name="dictionary">
        ///     Dictionary instance from with getting value.
        /// </param>
        /// <param name="key">
        ///     The key of the element to get.
        /// </param>
        /// <param name="default">
        ///     Value that returns if dictionary doesn't have specified <paramref name="key"/>.
        /// </param>
        /// <returns>
        ///     The element with the specified <paramref name="key"/> or <paramref name="@default"/> 
        ///     if key does not exists in dictionary.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        ///     <paramref name="key"/> is null.
        /// </exception>
        public static TValue GetValueSafe<TKey, TValue>([NotNull]this IDictionary<TKey, TValue> dictionary, TKey key, TValue @default = default(TValue))
        {
            return dictionary.GetOrAdd(key, k => @default);
        }

        public static TValue GetValueSafe<TKey, TValue>([NotNull]this IDictionary<TKey, TValue> dictionary, TKey key, [NotNull]Func<TValue> getDefault)
        {
            getDefault = Check.NotNull(getDefault, nameof(getDefault));
            return dictionary.GetOrAdd(key, k => getDefault());
        }

        public static TValue GetValueSafe<TKey, TValue>([NotNull]this IDictionary<TKey, TValue> dictionary, TKey key, [NotNull]Func<TKey, TValue> getDefault)
        {
            dictionary = Check.NotNull(dictionary, nameof(dictionary));
            getDefault = Check.NotNull(getDefault, nameof(getDefault));

            TValue value;

            if (!dictionary.TryGetValue(key, out value))
            {
                value = getDefault(key);
            }

            return value;
        }

    }
}
