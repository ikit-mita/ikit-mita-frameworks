using System;
using System.IO;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace IkitMita
{
    /// <summary>
    /// 
    /// </summary>
    public static class Check
    {
        /// <summary>
        /// Check if string argument is not null or empty
        /// </summary>
        /// <param name="argValue">Argument value for checking</param>
        /// <param name="argName">Argument name for correct exception</param>
        /// <param name="methodName">Caller name for better exception message</param>
        /// <param name="filePath">Caller file path for better exception message</param>
        /// <returns>Returns argument value without any changes</returns>
        /// <exception cref="ArgumentException">If argument value is null or empty string</exception>
        [NotNull]
        public static string NotNullOrEmpty(string argValue, [InvokerParameterName] string argName, [CallerMemberName] string methodName = null, [CallerFilePath] string filePath = null)
        {
            if (argValue.IsNullOrEmpty())
            {
                string method = MethodMsg(methodName);
                string @class = ClassMsg(filePath);
                throw new ArgumentException("String argument {0}{1}{2} can't be null or empty.".FormatWith(argName, method, @class), argName);
            }

            return argValue;
        }

        /// <summary>
        /// Check if object argument is not null
        /// </summary>
        /// <param name="argValue">Argument value for checking</param>
        /// <param name="argName">Argument name for correct exception</param>
        /// <param name="methodName">Caller name for better exception message</param>
        /// <param name="filePath">Caller file path for better exception message</param>
        /// <returns>Returns argument value without any changes</returns>
        /// <exception cref="ArgumentNullException">If argument value is null</exception>
        [NotNull]
        public static T NotNull<T>([NoEnumeration]T argValue, [InvokerParameterName] string argName, [CallerMemberName] string methodName = null, [CallerFilePath] string filePath = null) where T : class
        {
            if (argValue == null)
            {
                string method = MethodMsg(methodName);
                string @class = ClassMsg(filePath);
                throw new ArgumentNullException("Argument {0}{1}{2} can't be null.".FormatWith(argName, method, @class), argName);
            }

            return argValue;
        }

        /// <summary>
        /// Check if nullable value type argument is not null
        /// </summary>
        /// <param name="argValue">Argument value for checking</param>
        /// <param name="argName">Argument name for correct exception</param>
        /// <param name="methodName">Caller name for better exception message</param>
        /// <param name="filePath">Caller file path for better exception message</param>
        /// <returns>Returns argument value without any changes</returns>
        /// <exception cref="ArgumentNullException">If argument value is null</exception>
        [NotNull]
        public static T? NotNull<T>(T? argValue, [InvokerParameterName] string argName, [CallerMemberName] string methodName = null, [CallerFilePath] string filePath = null) where T : struct
        {
            if (argValue == null)
            {
                string method = MethodMsg(methodName);
                string @class = ClassMsg(filePath);
                throw new ArgumentNullException("Argument {0}{1}{2} can't be null.".FormatWith(argName, method, @class), argName);
            }

            return argValue;
        }

        /// <summary>
        /// Check if comparable type argument is not less than <paramref name="minValue"/>
        /// </summary>
        /// <param name="argValue">Argument value for checking</param>
        /// <param name="minValue"></param>
        /// <param name="argName">Argument name for correct exception</param>
        /// <param name="methodName">Caller name for better exception message</param>
        /// <param name="filePath">Caller file path for better exception message</param>
        /// <returns>Returns argument value without any changes</returns>
        /// <exception cref="ArgumentOutOfRangeException">If argument value is less than <paramref name="minValue"/></exception>
        public static T Min<T>(T argValue, T minValue, [InvokerParameterName] string argName, [CallerMemberName] string methodName = null, [CallerFilePath] string filePath = null) where T : IComparable<T>
        {
            int res = argValue.CompareTo(minValue);

            if (res < 0)
            {
                string method = MethodMsg(methodName);
                string @class = ClassMsg(filePath);
                throw new ArgumentOutOfRangeException(argName, "Min value for {0}{1}{2} is {3}".FormatWith(argName, method, @class, minValue));
            }

            return argValue;
        }

        private static string ClassMsg(string filePath)
        {
            return filePath.IsNullOrEmpty() ? string.Empty : (" of class " + Path.GetFileNameWithoutExtension(filePath));
        }

        private static string MethodMsg(string methodName)
        {
            return methodName.IsNullOrEmpty() ? string.Empty : (" in method " + methodName);
        }

    }
}
