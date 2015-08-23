using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using ZxdFramework.DataContract;

namespace ZxdFramework.Shell.UI.Views
{
    /// <summary>
    /// 导航视图模型
    /// </summary>
    public interface INavigateMenuViewModel
    {
        /// <summary>
        /// Gets the view.
        /// </summary>
        /// <value>The view.</value>
        INavigateMenuView View { get; }
    }

    [Export(typeof(INavigateMenuViewModel))]
    public class NavigateMenuViewModel : ViewModel<INavigateMenuView>, INavigateMenuViewModel
    {
        [ImportingConstructor]
        public NavigateMenuViewModel(INavigateMenuView view, IEventAggregator aggregator) : base(view, aggregator)
        {
            View.Model = this;
        }
    }
}