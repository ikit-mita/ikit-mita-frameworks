using System.ComponentModel.Composition;
using IkitMita.Mvvm.ViewModels;
using IkitMita.Mvvm.Views;
using Microsoft.Practices.ServiceLocation;

namespace IkitMita.Mvvm.MefClient
{
    [Export(typeof(IViewManager<IChildViewModel>))]
    internal class ChildViewManager : Views.ChildViewManager
    {
        [ImportingConstructor]
        public ChildViewManager(IViewModelManager<IChildViewModel> vmManager, IServiceLocator serviceLocator) : base(vmManager, serviceLocator)
        {
        }
    }
}
