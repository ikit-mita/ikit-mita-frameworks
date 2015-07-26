using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JetBrains.Annotations;

namespace IkitMita
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Execute action for each element in sequence.
        /// </summary>
        /// <typeparam name="T">Type of sequence elements.</typeparam>
        /// <param name="source">Source sequence.</param>
        /// <param name="action">Action for executing.</param>
        public static void Foreach<T>([NotNull]this IEnumerable<T> source, [NotNull]Action<T> action)
        {
            source = Check.NotNull(source, "source");
            action = Check.NotNull(action, "action");

            foreach (T item in source)
            {
                action(item);
            }
        }

        /// <summary>
        /// Execute action for each element in sequence.
        /// </summary>
        /// <typeparam name="T">Type of sequence elements.</typeparam>
        /// <param name="source">Source sequence.</param>
        /// <param name="action">Action for executing.</param>
        public static void ForeachSafe<T>([NotNull]this IEnumerable<T> source, [NotNull]Action<T> action)
        {
            source = Check.NotNull(source, "source");
            action = Check.NotNull(action, "action");

            foreach (T item in source)
            {
                try
                {
                    action(item);
                }
                catch (Exception exc)
                {
                    Debug.WriteLine(exc);
                }
            }
        }

        /// <summary>
        /// Execute action for each element in sequence.
        /// </summary>
        /// <typeparam name="T">Type of sequence elements.</typeparam>
        /// <param name="source">Source sequence.</param>
        /// <param name="action">
        ///     Action for executing. The second parameter
        ///     of the function represents the index of the source element.
        /// </param>
        public static void Foreach<T>([NotNull]this IEnumerable<T> source, [NotNull]Action<T, int> action)
        {
            source = Check.NotNull(source, "source");
            action = Check.NotNull(action, "action");

            int index = 0;
            foreach (T item in source)
            {
                action(item, index++);
            }
        }

        [LinqTunnel]
        public static IEnumerable<T> Do<T>([NotNull]this IEnumerable<T> source, [NotNull]Action<T> action)
        {
            source = Check.NotNull(source, "source");
            action = Check.NotNull(action, "action");

            foreach (T item in source)
            {
                action(item);
                yield return item;
            }
        }

        [LinqTunnel]
        public static IEnumerable<T> DoIf<T>([NotNull]this IEnumerable<T> source, [NotNull]Predicate<T> checker, [NotNull]Action<T> action)
        {
            source = Check.NotNull(source, "source");
            checker = Check.NotNull(checker, "checker");
            action = Check.NotNull(action, "action");

            foreach (T item in source)
            {
                if (checker(item))
                {
                    action(item);
                }

                yield return item;
            }
        }

        [LinqTunnel]
        public static IEnumerable<T> WhereNotNull<T>([NotNull]this IEnumerable<T> source)
        {
            return source.WhereNotNull(item => item);
        }

        [LinqTunnel]
        public static IEnumerable<TSource> WhereNotNull<TSource, TResult>([NotNull]this IEnumerable<TSource> source, [NotNull]Func<TSource, TResult> selector)
        {
            source = Check.NotNull(source, "source");
            selector = Check.NotNull(selector, "selector");
            return source.Where(item => selector(item) != null);
        }

        [LinqTunnel]
        public static IEnumerable<string> WhereNotNullOrEmpty([NotNull]this IEnumerable<string> source)
        {
            source = Check.NotNull(source, "source");
            return source.Where(item => !item.IsNullOrEmpty());
        }

        [LinqTunnel]
        public static IEnumerable<TSource> WhereNotNullOrEmpty<TSource>([NotNull]this IEnumerable<TSource> source, Func<TSource, string> selector)
        {
            source = Check.NotNull(source, "source");
            selector = Check.NotNull(selector, "selector");
            return source.Where(item => !selector(item).IsNullOrEmpty());
        }

        [LinqTunnel]
        public static IOrderedEnumerable<T> Sort<T>([NotNull]this IEnumerable<T> source)
        {
            source = Check.NotNull(source, "source");
            return source.OrderBy(item => item);
        }

        [LinqTunnel]
        public static IOrderedEnumerable<T> SortDescending<T>([NotNull]this IEnumerable<T> source)
        {
            source = Check.NotNull(source, "source");
            return source.OrderByDescending(item => item);
        }

        /// <summary>
        ///     Groups the elements of a sequence according to a specified key selector function 
        ///     and convert it into System.Collections.Generic.Dictionary[TKey, T]
        ///     with key got from <paramref name="keySelector"/> function.
        /// </summary>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An System.Collections.Generic.IEnumerable[T] whose elements to group.</param>
        /// <param name="keySelector">A function to extract the key for each element.</param>
        /// <returns>
        ///     System.Collections.Generic.Dictionary[TKey, T] where keys are results
        ///     of <paramref name="keySelector"/> functions and values are lists of
        ///     grouped by <paramref name="keySelector"/> elements.
        /// </returns>
        public static Dictionary<TKey, List<TSource>> GroupToDictionary<TKey, TSource>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            source = Check.NotNull(source, "source");
            keySelector = Check.NotNull(keySelector, "keySelector");
            var dict = source
                .GroupBy(keySelector)
                .ToDictionary(group => group.Key, group => group.ToList());
            return dict;
        }

        public static TSource MinBy<TSource, TProp>([NotNull]this IEnumerable<TSource> source, [NotNull]Func<TSource, TProp> selector)
            where TProp : IComparable<TProp>
        {
            source = Check.NotNull(source, "source");
            selector = Check.NotNull(selector, "selector");

            var enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException("The source sequence is empty");
            }

            TSource minItem = enumerator.Current;
            TProp minValue = selector(enumerator.Current);

            while (enumerator.MoveNext())
            {
                var value = selector(enumerator.Current);

                if (minValue.CompareTo(value) < 0)
                {
                    minValue = value;
                    minItem = enumerator.Current;
                }
            }


            return minItem;
        }

        public static TSource MaxBy<TSource, TProp>([NotNull]this IEnumerable<TSource> source, [NotNull]Func<TSource, TProp> selector)
            where TProp : IComparable<TProp>
        {
            source = Check.NotNull(source, "source");
            selector = Check.NotNull(selector, "selector");

            var enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException("The source sequence is empty");
            }

            TSource maxItem = enumerator.Current;
            TProp maxValue = selector(enumerator.Current);

            while (enumerator.MoveNext())
            {
                var value = selector(enumerator.Current);

                if (maxValue.CompareTo(value) > 0)
                {
                    maxValue = value;
                    maxItem = enumerator.Current;
                }
            }

            return maxItem;
        }

        public static bool MinMax<T>([NotNull]this IEnumerable<T> source, out T min, out T max)
        {
            source = Check.NotNull(source, "source");
            Comparer<T> comparer = Comparer<T>.Default;
            var enumerator = source.GetEnumerator();

            if (enumerator.MoveNext())
            {
                min = max = enumerator.Current;
            }
            else
            {
                min = max = default(T);
                return false;
            }

            while (enumerator.MoveNext())
            {
                if (comparer.Compare(enumerator.Current, min) < 0)
                {
                    min = enumerator.Current;
                }
                else if (comparer.Compare(enumerator.Current, max) > 0)
                {
                    max = enumerator.Current;
                }
            }

            return true;
        }

        /// <summary>
        ///     Concatenates the members of a collection, using the specified separator between each member.
        /// </summary>
        /// <typeparam name="T">The type of the members of <paramref name="values"/>.</typeparam>
        /// <param name="values">A sequence that contains the objects to concatenate.</param>
        /// <param name="separator">The string to use as a separator.</param>
        /// <returns>
        ///     A string that consists of the members of values delimited by the separator
        ///     string. If values has no members, the method returns System.String.Empty.
        /// </returns>
        public static string Join<T>([NotNull]this IEnumerable<T> values, [NotNull]string separator)
        {
            values = Check.NotNull(values, "values");
            separator = Check.NotNull(separator, "separator");

            return string.Join(separator, values);
        }

        /// <summary>
        ///     Concatenates the members of a collection, using the specified separator between each member.
        ///     <paramref name="format"/> applied for each element before concatenating.
        /// </summary>
        /// <typeparam name="T">The type of the members of <paramref name="values"/>.</typeparam>
        /// <param name="values">A sequence that contains the objects to concatenate.</param>
        /// <param name="separator">The string to use as a separator.</param>
        /// <param name="format">Format applied for each element before concatenating.</param>
        /// <returns>
        ///     A string that consists of the members of values delimited by the separator
        ///     string. If values has no members, the method returns System.String.Empty.
        /// </returns>
        public static string Join<T>([NotNull]this IEnumerable<T> values, [NotNull]string separator, [NotNull]string format)
        {
            values = Check.NotNull(values, "values");
            separator = Check.NotNull(separator, "separator");
            format = Check.NotNullOrEmpty(format, "format");
            
            return string.Join(separator, values.Select(i => format.FormatWith(i)));
        }

        /// <summary>
        ///     Concatenates the members of a collection, using the specified separator between each member.
        ///     <paramref name="toString"/> converts values to string before concatenating.
        /// </summary>
        /// <typeparam name="T">The type of the members of <paramref name="values"/>.</typeparam>
        /// <param name="values">A sequence that contains the objects to concatenate.</param>
        /// <param name="separator">The string to use as a separator.</param>
        /// <param name="toString">Converts values to string before concatenating. </param>
        /// <returns>
        ///     A string that consists of the members of values delimited by the separator
        ///     string. If values has no members, the method returns System.String.Empty.
        /// </returns>
        public static string Join<T>([NotNull]this IEnumerable<T> values, [NotNull]string separator, [NotNull]Func<T, string> toString)
        {
            values = Check.NotNull(values, "values");
            separator = Check.NotNull(separator, "separator");
            toString = Check.NotNull(toString, "toString");
            
            return string.Join(separator, values.Select(i => toString(i)));
        }

        public static HashSet<T> ToHashSet<T>([NotNull]this IEnumerable<T> source)
        {
            source = Check.NotNull(source, "source");

            return new HashSet<T>(source);
        }

        public static HashSet<TResult> ToHashSet<TSource, TResult>([NotNull]this IEnumerable<TSource> source, [NotNull]Func<TSource, TResult> selector)
        {
            source = Check.NotNull(source, "source");
            selector = Check.NotNull(selector, "selector");

            return new HashSet<TResult>(source.Select(selector));
        }

        [LinqTunnel]
        public static IEnumerable<T> Expand<T>([NotNull]this IEnumerable<IEnumerable<T>> values)
        {
            values = Check.NotNull(values, "values");

            // ReSharper disable once PossibleMultipleEnumeration
            return values.SelectMany(items => items);
        }

        [LinqTunnel]
        public static IEnumerable<List<T>> Split<T>([NotNull]this IEnumerable<T> values, int count)
        {
            values = Check.NotNull(values, "values");

            List<T> buffer = new List<T>(count);
            var enumerator = values.GetEnumerator();

            while (enumerator.MoveNext())
            {
                buffer.Add(enumerator.Current);

                if (buffer.Count == count)
                {
                    yield return buffer;
                    buffer = new List<T>(count);
                }
            }

            if (buffer.Count > 0)
            {
                yield return buffer;
            }
        }
    }
}
