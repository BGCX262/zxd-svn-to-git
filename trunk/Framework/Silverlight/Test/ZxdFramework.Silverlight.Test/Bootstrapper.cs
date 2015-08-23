using System;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Modularity;
using ZxdFramework.App;
using ZxdFramework.DataContract;
using ZxdFramework.Logging;
using ZxdFramework.Module;
using ZxdFramework.Service;

namespace ZxdFramework.Test
{
    /// <summary>
    /// ³ÌÐòÈË¿Ú
    /// </summary>
    public class Bootstrapper : MefBootstrapper
    {

        /// <summary>
        /// Creates the shell or main window of the application.
        /// </summary>
        /// <returns>The shell of the application.</returns>
        /// <remarks>
        /// If the returned instance is a <see cref="T:System.Windows.DependencyObject"/>, the
        /// <see cref="T:Microsoft.Practices.Prism.Bootstrapper"/> will attach the default <seealso cref="T:Microsoft.Practices.Prism.Regions.IRegionManager"/> of
        /// the application in its <see cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionManagerProperty"/> attached property
        /// in order to be able to add regions by using the <seealso cref="F:Microsoft.Practices.Prism.Regions.RegionManager.RegionNameProperty"/>
        /// attached property from XAML.
        /// </remarks>
        protected override DependencyObject CreateShell()
        {
            return Container.GetExportedValue<Shell>();
        }

        /// <summary>
        /// Initializes the shell.
        /// </summary>
        /// <remarks>
        /// The base implementation ensures the shell is composed in the container.
        /// </remarks>
        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.RootVisual = (UIElement)Shell;
        }

        protected override CompositionContainer CreateContainer()
        {
            Globals.Container = base.CreateContainer();
            return Globals.Container;
        }

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Globals).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Bootstrapper).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(PrismExtensions).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ServiceModule).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ModuleManagerModule).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(LoggingModule).Assembly));
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {

            var catalog = Microsoft.Practices.Prism.Modularity.ModuleCatalog
                .CreateFromXaml(
                    new Uri("/ZxdFramework.Silverlight.Test;component/ModulesCatalog.xaml", UriKind.Relative)
                    );

            //var c = (IModuleCatalog) catalog;

            //new ModuleCatalog())
            return catalog;
        }

    }
}