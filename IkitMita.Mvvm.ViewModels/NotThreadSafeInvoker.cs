using System;

namespace IkitMita.Mvvm.ViewModels
{
    internal class NotThreadSafeInvoker : IThreadSafeInvoker
    {
        private static readonly NotThreadSafeInvoker _instance = new NotThreadSafeInvoker();

        public static NotThreadSafeInvoker Instance
        {
            get { return _instance; }
        }

        public void Invoke(Action action)
        {
            action.Invoke();
        }
    }
}