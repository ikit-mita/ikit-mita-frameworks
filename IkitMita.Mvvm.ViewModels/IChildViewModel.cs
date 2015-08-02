using System.Threading.Tasks;

namespace IkitMita.Mvvm.ViewModels
{
    public interface IChildViewModel : IShowableViewModel
    {
        IShowableViewModel Parent { get; }

        bool ModalResult { get; }

        Task Close(bool modalResult = false);
    }
}
