using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace IkitMita
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Determines the index of a item which is matched with <paramref name="predicate"/> in the System.Collections.Generic.ICollection[T].
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="collection">Source collection for search in.</param>
        /// <param name="predicate">Condition of matching.</param>
        /// <returns>Index of a item which is matched with <paramref name="predicate"/></returns>
        public static int IndexOf<T>([NotNull]this ICollection<T> collection, [NotNull]Func<T, bool> predicate)
        {
            collection = Check.NotNull(collection, "collection");
            predicate = Check.NotNull(predicate, "predicate");


            int index = -1;

            foreach (var item in collection)
            {
                index++;

                if (predicate(item))
                {
                    return index;
                }
            }

            return -1;
        }

        /// <summary>
        ///     Determines whether the specified collection is <value>NULL</value> 
        ///     or contains no elements.
        /// </summary>
        /// <typeparam name="T">Collection items type.</typeparam>
        /// <param name="collection">Collection to test.</param>
        /// <returns><value>TRUE</value> if the value parameter is null or an empty collection; otherwise, <value>FALSE</value>.</returns>
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
        {
            return collection == null || collection.Count == 0;
        }

        public static TCollection AddRange<TCollection, TItem>([NotNull]this TCollection collection, IEnumerable<TItem> items)
            where TCollection : class, ICollection<TItem>
        {
            collection = Check.NotNull(collection, "collection");

            if (items == null)
            {
                return collection;
            }

            List<TItem> list = collection as List<TItem>;

            if (list != null)
            {
                list.AddRange(items);
            }
            else
            {
                foreach (var item in items)
                {
                    collection.Add(item);
                }
            }

            return collection;
        }

        public static TCollection RemoveRange<TCollection, TItem>([NotNull]this TCollection collection, IEnumerable<TItem> items)
            where TCollection : class, ICollection<TItem>
        {
            collection = Check.NotNull(collection, "collection");

            if (items == null)
            {
                return collection;
            }

            foreach (var item in items)
            {
                collection.Remove(item);
            }

            return collection;
        }

        [StringFormatMethod("format")]
        public static TCollection AddFormat<TCollection>([NotNull]this TCollection collection, string format, params object[] args)
            where TCollection : class, ICollection<string>
        {
            collection = Check.NotNull(collection, "collection");

            collection.Add(format.FormatWith(args));
            return collection;
        }
    }
}
