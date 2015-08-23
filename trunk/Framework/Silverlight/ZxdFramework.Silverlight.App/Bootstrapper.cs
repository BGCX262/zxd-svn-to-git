using System;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Modularity;
using ZxdFramework.DataContract;
using ZxdFramework.Logging;
using ZxdFramework.Module;
using ZxdFramework.Service;

namespace ZxdFramework.App
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
            Globals.EventAggregator = Globals.Container.GetExportedValue<IEventAggregator>();
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

        /// <summary>
        /// Creates the <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer"/> that will be used as the default container.
        /// </summary>
        /// <returns>
        /// A new instance of <see cref="T:System.ComponentModel.Composition.Hosting.CompositionContainer"/>.
        /// </returns>
        /// <remarks>
        /// The base implementation registers a default MEF catalog of exports of key Prism types.
        /// Exporting your own types will replace these defaults.
        /// </remarks>
        protected override CompositionContainer CreateContainer()
        {
            Globals.Container = base.CreateContainer();
            return Globals.Container;
        }




        /// <summary>
        /// Configures the <see cref="P:Microsoft.Practices.Prism.MefExtensions.MefBootstrapper.AggregateCatalog"/> used by MEF.
        /// </summary>
        /// <remarks>
        /// The base implementation does nothing.
        /// </remarks>
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

        /// <summary>
        /// Creates the <see cref="T:Microsoft.Practices.Prism.Modularity.IModuleCatalog"/> used by Prism.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// The base implementation returns a new ModuleCatalog.
        /// </remarks>
        protected override IModuleCatalog CreateModuleCatalog()
        {

            var catalog = Microsoft.Practices.Prism.Modularity.ModuleCatalog
                .CreateFromXaml(
                    new Uri("/ZxdFramework.Silverlight.App;component/ModulesCatalog.xaml", UriKind.Relative)
                    );
            return catalog;
        }


        /// <summary>
        /// Create the <see cref="T:Microsoft.Practices.Prism.Logging.ILoggerFacade"/> used by the bootstrapper.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// The base implementation returns a new TextLogger.
        /// </remarks>
        protected override ILoggerFacade CreateLogger()
        {
            Globals.LoggerFacade = new EventLoggerFacade();
            return Globals.LoggerFacade;
        }
    }
}