﻿using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows.Input;
using IkitMita.Mvvm.ViewModels;

namespace Example.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MainViewModel : ChildViewModelBase
    {
        private bool _allowMessage;
        private ICommand _showChildCommand;
        private int _titleMaxLength = 25;
        private string _messageTitle;
        private int _textMaxLength = 100;
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

        [DependsOn("TextMaxLength")]
        [DependsOn("MessageTitle")]
        public int MessageTitleCharsLeft
        {
            get { return TitleMaxLength - (MessageTitle ?? string.Empty).Length; }
        }
        public int TitleMaxLength
        {
            get { return _titleMaxLength; }
        }

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

        [DependsOn("TextMaxLength")]
        [DependsOn("TextMaxLength")]
        public int MessageTextCharsLeft
        {
            get { return TextMaxLength - (MessageText ?? string.Empty).Length; }
        }

        public int TextMaxLength
        {
            get { return _textMaxLength; }
        }

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

        [DependsOn("AllowMessage")]
        public ICommand ShowMessageCommand
        {
            get { return _showChildCommand ?? (_showChildCommand = new DelegateCommand(ShowMessage, () => AllowMessage)); }
        }

        private async void ShowMessage()
        {
           await ViewModelProvider.ShowMessage(MessageText, MessageTitle);
        }

        protected override async Task<bool> OnClosing(bool modalResult)
        {
            var messageViewModel = ViewModelProvider.ShowMessage("Question", "Close?", MessageButtons.OkCancel);
            var res = await messageViewModel;
            return res == "Ok";
        }
    }
}