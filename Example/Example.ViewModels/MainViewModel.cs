using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows.Input;
using IkitMita;
using IkitMita.Mvvm.ViewModels;

namespace Example.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MainViewModel : ChildViewModelBase
    {
        private bool _allowMessage;
        private ICommand _showChildCommand;
        private string _messageTitle;
        private string _messageText;

        public MainViewModel()
        {
            Title = "Main Window";
        }

        [Import]
        private ViewModelProvider ViewModelProvider { get; set; }

        public string MessageTitle
        {
            get { return _messageTitle; }
            set
            {
                if (value == _messageTitle) return;
                _messageTitle = value;
                OnPropertyChanged();
            }
        }

        [DependsOn(nameof(TitleMaxLength))]
        [DependsOn(nameof(MessageTitle))]
        public int MessageTitleCharsLeft => TitleMaxLength - (MessageTitle ?? string.Empty).Length;

        public int TitleMaxLength { get; } = 25;

        public string MessageText
        {
            get { return _messageText; }
            set
            {
                if (value == _messageText) return;
                _messageText = value;
                OnPropertyChanged();
            }
        }

        [DependsOn(nameof(TextMaxLength))]
        [DependsOn(nameof(MessageText))]
        public int MessageTextCharsLeft => TextMaxLength - (MessageText ?? string.Empty).Length;

        public int TextMaxLength { get; } = 100;

        public bool AllowMessage
        {
            get { return _allowMessage; }
            set
            {
                if (value == _allowMessage) return;
                _allowMessage = value;
                OnPropertyChanged();
            }
        }

        [DependsOn(nameof(AllowMessage))]
        public ICommand ShowMessageCommand
        {
            get { return _showChildCommand ?? (_showChildCommand = new DelegateCommand(ShowMessage, () => AllowMessage)); }
        }

        private async void ShowMessage()
        {
            var msgVm = ViewModelProvider.ShowMessage(this, MessageText, MessageTitle);
            await Task.Delay(500);
            if (MessageText.IsNullOrEmpty())
            {
                msgVm.Message = "HO-HO-HO!!!";
            }

            await msgVm;
        }

        protected override async Task<bool> OnClosing(bool modalResult)
        {
            var messageViewModel = ViewModelProvider.ShowMessage(this, "Do you realy want to close this application?", "Closing", MessageButtons.YesNo);
            var res = await messageViewModel;
            return res == MessageButtons.YesNo[0];
        }
    }
}
