using System;
using System.Reflection;
using JetBrains.Annotations;

namespace IkitMita
{
    public static class ObjectExtensions
    {
        public static void RaiseEvent<TEventArgs>([NotNull]this object source, [NotNull]string eventName, TEventArgs eventArgs) where TEventArgs : EventArgs
        {
            source = Check.NotNull(source, "source");
            eventName = Check.NotNullOrEmpty(eventName, "eventName");

            var fieldInfo = source.GetType().GetField(eventName, BindingFlags.Instance | BindingFlags.NonPublic);

            if (fieldInfo == null)
            {
                throw new MissingFieldException(source.GetType().FullName, eventName);
            }

            var eventDelegate = (MulticastDelegate)fieldInfo.GetValue(source);
            if (eventDelegate != null)
            {
                foreach (var handler in eventDelegate.GetInvocationList())
                {
                    handler.Method.Invoke(handler.Target, new[] { source, eventArgs });
                }
            }
        }
    }
}
