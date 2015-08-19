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
        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                if (value == _message) return;
                _message = value;
                OnPropertyChanged();
            }
        }

        public string Result { get; private set; }

        public ObservableCollection<string> Buttons => _buttons ?? (_buttons = new ObservableCollection<string>());

        public ICommand OnButtonClickCommand => _onButtonClickCommand ?? (_onButtonClickCommand = new DelegateCommand<string>(OnButtonClick));

        private async void OnButtonClick(string button)
        {
            Result = button;
            await Close(true);
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
