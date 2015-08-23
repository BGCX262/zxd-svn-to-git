using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using ZxdFramework.DataContract;

namespace ZxdFramework.Shell.UI.Views
{
    /// <summary>
    /// 左边导航的视图模型
    /// </summary>
    public interface ILeftNavigateViewModel
    {
        /// <summary>
        /// 获取视图
        /// </summary>
        /// <value>The view.</value>
        ILeftNavigateView View { get; }
    }

    [Export(typeof(ILeftNavigateViewModel))]
    public class LeftNavigateViewModel : ViewModel<ILeftNavigateView>, ILeftNavigateViewModel
    {
        [ImportingConstructor]
        public LeftNavigateViewModel(ILeftNavigateView view, IEventAggregator aggregator) : base(view, aggregator)
        {
            view.Model = this;
        }
    }
}