using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using IkitMita.Mvvm.ViewModels;

namespace Example.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public sealed class MessageViewModel : ChildViewModelBase
    {
        private ObservableCollection<string> _buttons;
        private ICommand _onButtonClickCommand;

        public string Message { get; set; }

        public string Result { get; private set; }

        public ObservableCollection<string> Buttons
        {
            get { return _buttons ?? (_buttons = new ObservableCollection<string>()); }
        }

        public ICommand OnButtonClickCommand
        {
            get { return _onButtonClickCommand ?? (_onButtonClickCommand = new DelegateCommand<string>(OnButtonClick)); }
        }

        private void OnButtonClick(string button)
        {
            Result = button;
        }

        public void SetTitle(string title)
        {
            Title = title;
        }

        public TaskAwaiter<string> GetAwaiter()
        {
            var tcs = new TaskCompletionSource<string>();
            Closed += (s, e) => tcs.TrySetResult(Result);
            if (IsClosed) tcs.TrySetResult(Result);
            return tcs.Task.GetAwaiter();
        }


    }
}
