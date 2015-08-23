using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using ZxdFramework.DataContract;

namespace ZxdFramework.Shell.UI.Views
{
    /// <summary>
    /// ��ߵ�������ͼģ��
    /// </summary>
    public interface ILeftNavigateViewModel
    {
        /// <summary>
        /// ��ȡ��ͼ
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