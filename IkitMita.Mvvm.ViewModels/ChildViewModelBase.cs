using System;
using System.ComponentModel.Composition;

namespace IkitMita.Mvvm.ViewModels
{
    public abstract class ChildViewModelBase : ViewModelBase, IChildViewModel
    {
        [Import]
        protected virtual IViewModelManager<IChildViewModel> ViewModelManager { get; set; }

        private bool? _modalResult;
        private string _title;
        private DelegateCommand<bool?> _closeCommand;

        public IChildViewModel Parent { get; set; }

        public bool ModalResult { get { return _modalResult.GetValueOrDefault(); } }

        public bool IsClosed { get; protected set; }

        public string Title
        {
            get { return _title; }
            protected set
            {
                if (value == _title) return;
                _title = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand<bool?> CloseCommand
        {
            get
            {
                return _closeCommand ??
                    (_closeCommand = new DelegateCommand<bool?>(mr => Close(mr.GetValueOrDefault())));
            }
        }

        public void Show()
        {
            ThreadSafeInvoker.Invoke(() => ViewModelManager.ShowViewModel(this));
        }

        public void Close(bool modalResult = false)
        {
            if (OnClosing(modalResult))
            {
                IsClosed = true;
                _modalResult = modalResult;
                ThreadSafeInvoker.Invoke(() => ViewModelManager.CloseViewModel(this));
                OnClosed();
            }
        }

        protected virtual bool OnClosing(bool modalResult)
        {
            return true;
        }

        protected virtual void OnClosed()
        {
            var handler = Closed;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public event EventHandler Closed;
    }
}
