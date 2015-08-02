using System;
using System.Threading.Tasks;

namespace IkitMita.Mvvm.ViewModels
{
    public interface IShowableViewModel : IViewModel
    {
        bool IsClosed { get; }

        string Title { get; }

        void Show();

        Task Close();

        event EventHandler Closed;
    }
}
