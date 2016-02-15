using System;
using JetBrains.Annotations;

namespace IkitMita.Mvvm.ViewModels
{
    public interface IViewModelManager<TVm> where TVm : IShowableViewModel
    {
        void ShowViewModel([NotNull]TVm viewModel);
        void CloseViewModel([NotNull]TVm viewModel);

        event EventHandler<ViewModelEventArgs<TVm>> ViewModelShown;
        event EventHandler<ViewModelEventArgs<TVm>> ViewModelClosed;
    }
}
