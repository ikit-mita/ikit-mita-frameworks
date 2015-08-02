using System.Runtime.CompilerServices;

namespace Example.ViewModels
{
    public interface IAwaiter<out TResult> : INotifyCompletion
    {
        bool IsCompleted { get; }
        TResult GetResult(); // TResult can also be void
    }
}
