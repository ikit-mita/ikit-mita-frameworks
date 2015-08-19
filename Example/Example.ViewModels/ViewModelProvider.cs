using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using IkitMita;
using IkitMita.Mvvm.ViewModels;
using JetBrains.Annotations;
using Microsoft.Practices.ServiceLocation;

namespace Example.ViewModels
{
    [Export]
    public class ViewModelProvider
    {
        private readonly IServiceLocator _serviceLocator;

        [ImportingConstructor]
        public ViewModelProvider(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        [NotNull]
        public MessageViewModel ShowMessage(IChildViewModel parent, string message, string title, params string[] buttons)
        {
            if (buttons.IsNullOrEmpty())
            {
                buttons = MessageButtons.Ok.ToArray();
            }

            var messageViewModel = _serviceLocator.GetInstance<MessageViewModel>();
            messageViewModel.Parent = parent;
            messageViewModel.SetTitle(title);
            messageViewModel.Message = message;
            messageViewModel.Buttons.AddRange(buttons);
            messageViewModel.Show();

            return messageViewModel;
        }

        [NotNull]
        public MessageViewModel ShowMessage(IChildViewModel parent, string message, string title, IEnumerable<string> buttons = null)
        {
            return ShowMessage(parent, message, title, buttons?.ToArray());
        }
    }
}
