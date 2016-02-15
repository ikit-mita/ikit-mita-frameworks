using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

namespace IkitMita.Mvvm.ViewModels
{
    public abstract class ChildViewModelBase : ViewModelBase, IChildViewModel
    {
        [Import]
        protected virtual IViewModelManager<IChildViewModel> ViewModelManager { get; set; }

        private bool? _modalResult;
        private string _title;
        private DelegateCommand<bool?> _closeCommand;

        public IShowableViewModel Parent { get; set; }

        public bool ModalResult => _modalResult.GetValueOrDefault();

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
            if (ViewModelManager == null)
            {
                throw new InvalidOperationException("You can't call method ChildViewModelBase.Show before ViewModelManager was initialized.");
            }

            ThreadSafeInvoker.Invoke(() => ViewModelManager.ShowViewModel(this));
        }

        public async Task Close()
        {
            await Close(false);
        }

        public async Task Close(bool modalResult)
        {
            if (await OnClosing(modalResult))
            {
                IsClosed = true;
                _modalResult = modalResult;
                ThreadSafeInvoker.Invoke(() => ViewModelManager.CloseViewModel(this));
                OnClosed();
            }
        }

        protected virtual Task<bool> OnClosing(bool modalResult)
        {
            return Task.FromResult(true);
        }

        protected virtual void OnClosed()
        {
            Closed?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler Closed;
    }
}
