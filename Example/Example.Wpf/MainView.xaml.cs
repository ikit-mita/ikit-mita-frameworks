using System.ComponentModel.Composition;
using IkitMita.Mvvm.Views;

namespace Example.Wpf
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    [Export("MainView", typeof(IView))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class MainView : IView
    {
        public MainView()
        {
            InitializeComponent();
        }
    }
}
