using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace IkitMita.Mvvm.ViewModels
{
    public static class ViewModelExtensions
    {
        public static TaskAwaiter<bool> GetAwaiter(this IShowableViewModel vmModel)
        {
            var tcs = new TaskCompletionSource<bool>();
            vmModel.Closed += (s, e) => tcs.TrySetResult(false);
            if (vmModel.IsClosed) tcs.TrySetResult(false);
            return tcs.Task.GetAwaiter();
        }

        public static TaskAwaiter<bool> GetAwaiter(this IChildViewModel vmModel)
        {
            var tcs = new TaskCompletionSource<bool>();
            vmModel.Closed += (s, e) => tcs.TrySetResult(vmModel.ModalResult);
            if (vmModel.IsClosed) tcs.TrySetResult(vmModel.ModalResult);
            return tcs.Task.GetAwaiter();
        }

    }
}
