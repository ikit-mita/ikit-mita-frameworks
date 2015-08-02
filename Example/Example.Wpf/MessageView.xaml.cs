using System.ComponentModel.Composition;
using IkitMita.Mvvm.Views;

namespace Example.Wpf
{
    /// <summary>
    /// Interaction logic for MessageView.xaml
    /// </summary>
    [Export("MessageView", typeof(IView))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class MessageView : IView
    {
        public MessageView()
        {
            InitializeComponent();
        }
    }
}
