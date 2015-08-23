using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using ZxdFramework.DataContract;

namespace ZxdFramework.Shell.UI.Views
{

    /// <summary>
    /// ���������ݽ���ģ��
    /// </summary>
    public interface IContentViewModel
    {
        /// <summary>
        /// Gets the view.
        /// </summary>
        /// <value>The view.</value>
        IContentView View { get; }
    }


    [Export(typeof(IContentViewModel))]
    public class ContentViewModel : ViewModel<IContentView>, IContentViewModel
    {
        [ImportingConstructor]
        public ContentViewModel(IContentView view, IEventAggregator aggregator) : base(view, aggregator)
        {
            View.Model = this;
        }
    }
}