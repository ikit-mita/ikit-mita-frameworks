﻿using System.Collections.Concurrent;
using System.Windows;
using IkitMita.Mvvm.ViewModels;
using Microsoft.Practices.ServiceLocation;

namespace IkitMita.Mvvm.Views
{
    public class ChildViewManager : ViewManagerBase<IChildViewModel>
    {
        private readonly ConcurrentDictionary<IShowableViewModel, ChildViewPresenter> _openedPresenters =
            new ConcurrentDictionary<IShowableViewModel, ChildViewPresenter>();


        public ChildViewManager(IViewModelManager<IChildViewModel> vmManager, IServiceLocator serviceLocator)
            : base(vmManager, serviceLocator)
        {
        }

        public override void ShowViewForViewModel(IChildViewModel viewModel)
        {
            IShowableViewModel parentViewModel = viewModel.Parent;
            ChildViewPresenter parentPresenter = null;

            if (parentViewModel != null)
            {
                _openedPresenters.TryGetValue(parentViewModel, out parentPresenter);
            }

            var view = ResolveView(viewModel.GetType());
            var presenter = new ChildViewPresenter
            {
                View = view,
                DataContext = viewModel
            };

            if (parentPresenter != null)
            {
                //presenter.ShowInTaskbar = false;
                presenter.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                presenter.Owner = parentPresenter;
            }

            _openedPresenters[viewModel] = presenter;

            if (presenter.Owner == null)
            {
                presenter.Show();
            }
            else
            {
                //prevent stream blocking 
                presenter.Dispatcher.InvokeAsync(() => presenter.ShowDialog());
            }
        }

        public override void CloseViewForViewModel(IChildViewModel viewModel)
        {
            ChildViewPresenter presenter;

            if (_openedPresenters.TryRemove(viewModel, out presenter))
            {
                presenter.Close();
            }
        }
    }
}
