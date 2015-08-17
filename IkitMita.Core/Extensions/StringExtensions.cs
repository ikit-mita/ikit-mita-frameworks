using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace IkitMita
{
    public static class StringExtensions
    {
        /// <summary>
        /// Parse int from string
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns>Integer value, if NaN returns <paramref name="defaultValue"/></returns>
        public static int ToInt(this string str, int defaultValue)
        {
            int i;
            return !int.TryParse(str, out i) ? defaultValue : i;
        }

        /// <summary>
        /// Parse int from string
        /// </summary>
        /// <param name="str"></param>
        /// <returns>Integer value, if NaN returns 0</returns>
        public static int ToInt(this string str)
        {
            int i = ToInt(str, default(int));
            return i;
        }

        public static long ToLong(this string str, long defaultValue = default(long))
        {
            long i;
            return !long.TryParse(str, out i) ? defaultValue : i;
        }

        public static double ToDouble(this string str, double defaultValue)
        {
            double d;
            return !double.TryParse(str, out d) ? defaultValue : d;
        }

        public static double ToDouble(this string str)
        {
            double d = str.ToDouble(default(double));
            return d;
        }

        public static string Wrap(this string str, string startWrapper, string endWrapper = null)
        {
            Check.NotNullOrEmpty(startWrapper, nameof(startWrapper));

            return string.Concat(startWrapper, str, (endWrapper ?? startWrapper));
        }

        public static string WrapByTag(this string source, string tag)
        {
            string tagged = string.Format("<{0}>{1}</{0}>", tag, source);
            return tagged;
        }

        public static string WrapByTag(this string source, string tag, IDictionary<string, string> attrs)
        {
            var sb = new StringBuilder();
            sb.Append("<" + tag);

            foreach (var attr in attrs)
            {
                sb.AppendFormat(" {0}='{1}'", attr.Key, attr.Value);
            }

            sb.AppendFormat(">{0}</{1}>", source, tag);
            return sb.ToString();
        }

        public static bool Contains([NotNull]this string source, [NotNull]string value, bool ignoreCase)
        {
            source = Check.NotNull(source, nameof(source));
            value = Check.NotNull(value, nameof(value));

            if (ignoreCase)
            {
                source = source.ToUpper();
                value = value.ToUpper();
            }

            return source.Contains(value);
        }

        public static string Replace([NotNull]this string source, [NotNull][RegexPattern]string oldValue, [NotNull]string newValue, bool ignoreCase)
        {
            source = Check.NotNull(source, nameof(source));
            oldValue = Check.NotNull(oldValue, nameof(oldValue));
            newValue = Check.NotNull(newValue, nameof(newValue));

            var regex = new Regex(oldValue, ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None);
            var newSentence = regex.Replace(source, newValue);
            return newSentence;
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static string[] Split([NotNull]this string str, [NotNull]string delimeter, StringSplitOptions options = StringSplitOptions.None)
        {
            str = Check.NotNull(str, nameof(str));
            delimeter = Check.NotNull(delimeter, nameof(delimeter));

            string[] parts = str.Split(new[] { delimeter }, options);
            return parts;
        }

        public static string[] Split([NotNull]this string str, char delimeter, StringSplitOptions options = StringSplitOptions.None)
        {
            str = Check.NotNull(str, nameof(str));

            string[] parts = str.Split(new[] { delimeter }, options);
            return parts;
        }

        [StringFormatMethod("format")]
        public static string FormatWith(this string format, params object[] args)
        {
            format = Check.NotNull(format, nameof(format));
            return string.Format(format, args);
        }

        [StringFormatMethod("format")]
        public static string FormatWith(this string format, object arg0)
        {
            format = Check.NotNull(format, nameof(format));
            return string.Format(format, arg0);
        }

        [StringFormatMethod("format")]
        public static string FormatWith(this string format, object arg0, object arg1)
        {
            format = Check.NotNull(format, nameof(format));
            return string.Format(format, arg0, arg1);
        }

        [StringFormatMethod("format")]
        public static string FormatWith(this string format, object arg0, object arg1, object arg2)
        {
            format = Check.NotNull(format, nameof(format));
            return string.Format(format, arg0, arg1, arg2);
        }

        public static string TrimSafe(this string str)
        {
            return (str ?? string.Empty).Trim();
        }

        /// <summary>
        ///     Replace <paramref name="value"/> with empty string.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Remove([NotNull]this string str, string value)
        {
            str = Check.NotNull(str, nameof(str));
            return str.Replace(value, string.Empty);
        }

        /// <summary>
        /// Converts string to bytes array without using Encoding.
        /// </summary>
        /// <param name="str">String to convert.</param>
        /// <returns>Bytes array representation of string.</returns>
        public static byte[] GetBytes([NotNull]this string str)
        {
            str = Check.NotNull(str, nameof(str));
            byte[] bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string FirstLetterToLower(this string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToLower(str[0]) + str.Substring(1);

            return str.ToLower();
        }

        public static string FirstLetterToUpper(this string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
        }

        public static bool StartsWithLetter(this string str)
        {
            return !IsNullOrEmpty(str) && Char.IsLetter(str[0]);
        }

        public static string SubstringSafe(this string str, int startIndex, int length)
        {
            if (IsNullOrEmpty(str) || startIndex >= str.Length || length <= 0)
            {
                return string.Empty;
            }

            length = Math.Min(length, str.Length - startIndex);

            return str.Substring(startIndex, length);
        }
    }
}
