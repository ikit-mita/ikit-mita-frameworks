using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using IkitMita.Mvvm.ViewModels;
using IkitMita.Mvvm.Views;
using Microsoft.Mef.CommonServiceLocator;
using Microsoft.Practices.ServiceLocation;

namespace IkitMita.Mvvm.MefClient
{
    public class MvvmApplication : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            FillAppResources();
            var assembly = new AssemblyCatalog(Assembly.GetEntryAssembly());
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(assembly);
            catalog.Catalogs.Add(new DirectoryCatalog("."));
            var container = new CompositionContainer(catalog);
            var mefServiceLocator = new MefServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => mefServiceLocator);
            container.ComposeExportedValue<IServiceLocator>(mefServiceLocator);
            container.ComposeParts(this);
        }

        [Import(RequiredCreationPolicy = CreationPolicy.Shared)]
        protected IServiceLocator Locator { get; set; }

        [Import(RequiredCreationPolicy = CreationPolicy.Shared)]
        protected IViewManager<IChildViewModel> ViewManager { get; set; }

        private void FillAppResources()
        {
            var buttonStyle = new Style(typeof(Button));
            buttonStyle.Setters.Add(new Setter(FrameworkElement.MinHeightProperty, 21d));
            buttonStyle.Setters.Add(new Setter(Control.PaddingProperty, new Thickness(15, 0, 15, 0)));
            Resources.Add(typeof(Button), buttonStyle);

            var textBoxStyle = new Style(typeof(TextBox));
            textBoxStyle.Setters.Add(new Setter(FrameworkElement.MinHeightProperty, 21d));
            Resources.Add(typeof(TextBox), textBoxStyle);

        }

    }
}
