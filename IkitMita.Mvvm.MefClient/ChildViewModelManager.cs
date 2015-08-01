using System.ComponentModel.Composition;
using IkitMita.Mvvm.ViewModels;

namespace IkitMita.Mvvm.MefClient
{
    [Export(typeof(IViewModelManager<IChildViewModel>))]
    internal class ChildViewModelManager : ViewModelManagerBase<IChildViewModel>
    {
    }
}
