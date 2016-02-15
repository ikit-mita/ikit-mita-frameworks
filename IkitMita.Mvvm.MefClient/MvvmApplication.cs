using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Reflection;
using System.Windows;
using IkitMita.Mvvm.ViewModels;
using IkitMita.Mvvm.Views;
using Microsoft.Mef.CommonServiceLocator;
using Microsoft.Practices.ServiceLocation;

namespace IkitMita.Mvvm.MefClient
{
    public class MvvmApplication : Application
    {
        public Type StartupViewModelType { get; set; }

        protected void StartupHandler(object sender, StartupEventArgs e)
        {
            InitializeMef();
        }

        protected void InitializeMef()
        {
            var assembly = new AssemblyCatalog(Assembly.GetEntryAssembly());
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(assembly);
            catalog.Catalogs.Add(new DirectoryCatalog("."));
            var container = new CompositionContainer(catalog);
            var mefServiceLocator = new MefServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => mefServiceLocator);
            container.ComposeExportedValue<IServiceLocator>(mefServiceLocator);
            container.ComposeParts(this);

            if (StartupViewModelType != null)
            {
                var showableViewModel = Locator.GetInstance(StartupViewModelType) as IShowableViewModel;

                showableViewModel?.Show();
            }
        }

        [Import(RequiredCreationPolicy = CreationPolicy.Shared)]
        protected IServiceLocator Locator { get; set; }

        [Import(RequiredCreationPolicy = CreationPolicy.Shared)]
        protected IViewManager<IChildViewModel> ViewManager { get; set; }
    }
}
