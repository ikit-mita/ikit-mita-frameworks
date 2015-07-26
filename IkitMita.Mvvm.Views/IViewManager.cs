using IkitMita.Mvvm.ViewModels;
using JetBrains.Annotations;

namespace IkitMita.Mvvm.Views
{
    public interface IViewManager<in TVm> where TVm : IViewModel
    {
        void ShowViewForViewModel([NotNull]TVm viewModel);

        void CloseViewForViewModel([NotNull]TVm viewModel);
    }
}
