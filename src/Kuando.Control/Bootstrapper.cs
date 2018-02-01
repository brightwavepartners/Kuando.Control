using Kuando.Control.RegionAdapters;
using Kuando.Control.Views;
using Microsoft.Practices.ServiceLocation;
using Prism.Mef;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace Kuando.Control
{
    public class Bootstrapper : MefBootstrapper
    {
        #region Methods

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            // a post-build event should be added to each module project that copies the module files
            // to the output directory of the current assembly. that directory is then added to the
            // aggregate catalog so that the mef container can load exports from each module into the
            // container from that directory.
            var applicationPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            if (applicationPath != null)
            {
                this.AggregateCatalog.Catalogs.Add(new DirectoryCatalog(applicationPath));
            }
            else
            {
                // TODO: do something with this error condition
                Debug.WriteLine("Unable to get application path.  No modules loaded.");
            }

            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(Assembly.GetExecutingAssembly()));
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
        }

        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            var mappings = base.ConfigureRegionAdapterMappings();

            mappings.RegisterMapping(typeof(StackPanel), this.Container.GetExportedValue<StackPanelRegionAdapter>());

            return mappings;
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            {
                var viewName = viewType.FullName;
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}ViewModel, {1}", viewName, viewAssemblyName);
                var type = Type.GetType(viewModelName);
                return type;
            });
        }

        protected override DependencyObject CreateShell()
        {
            return ServiceLocator.Current.GetInstance<Shell>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow = (Shell)this.Shell;

            if (Application.Current.MainWindow != null)
            {
                // it looks like the view model wiring doesnt appear
                // to work unless the window is shown first. once the window
                // is shown, the view model is automatically wired to the view
                // and then we can hide the window again.
                Application.Current.MainWindow.Show();
                Application.Current.MainWindow.Hide();
            }
            else
            {
                // TODO: throw some type of exception and shutdown app?
            }
        }

        #endregion
    }
}
