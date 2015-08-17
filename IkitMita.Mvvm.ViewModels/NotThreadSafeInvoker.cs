using System;

namespace IkitMita.Mvvm.ViewModels
{
    internal class NotThreadSafeInvoker : IThreadSafeInvoker
    {
        public static NotThreadSafeInvoker Instance { get; } = new NotThreadSafeInvoker();

        public void Invoke(Action action)
        {
            action.Invoke();
        }
    }
}