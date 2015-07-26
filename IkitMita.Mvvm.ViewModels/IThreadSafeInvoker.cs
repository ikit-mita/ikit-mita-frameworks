using System;
using JetBrains.Annotations;

namespace IkitMita.Mvvm.ViewModels
{
    public interface IThreadSafeInvoker
    {
        void Invoke([InstantHandle]Action action);
    }
}
