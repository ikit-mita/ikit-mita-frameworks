﻿using System;
using System.Collections.Generic;

namespace IkitMita.Mvvm.ViewModels
{
    public class ViewModelManagerBase<TVm> : IViewModelManager<TVm> where TVm : IShowableViewModel
    {
        private readonly HashSet<TVm> _openedViewModels = new HashSet<TVm>();

        public void ShowViewModel(TVm viewModel)
        {
            if (_openedViewModels.Add(viewModel))
            {
                OnViewModelShown(viewModel);
            }
        }

        public void CloseViewModel(TVm viewModel)
        {
            if (_openedViewModels.Remove(viewModel))
            {
                OnViewModelClosed(viewModel);
            }
        }

        public event EventHandler<ViewModelEventArgs<TVm>> ViewModelShown;
        public event EventHandler<ViewModelEventArgs<TVm>> ViewModelClosed;

        protected virtual void OnViewModelShown(TVm vm)
        {
            ViewModelShown?.Invoke(this, new ViewModelEventArgs<TVm>(vm));
        }

        protected virtual void OnViewModelClosed(TVm vm)
        {
            ViewModelClosed?.Invoke(this, new ViewModelEventArgs<TVm>(vm));
        }
    }
}
