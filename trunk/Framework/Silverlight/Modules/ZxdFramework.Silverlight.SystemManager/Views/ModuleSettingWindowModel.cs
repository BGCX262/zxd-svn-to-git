using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using ZxdFramework.DataContract;

namespace ZxdFramework.SystemManager.Views
{
    /// <summary>
    /// ����ģ�����ô��ڵ�ģ��
    /// </summary>
    public interface IModuleSettingWindowModel
    {
        /// <summary>
        /// Gets the view.
        /// </summary>
        /// <value>The view.</value>
        IModuleSettingWindow View { get; }
    }

    [Export(typeof(IModuleSettingWindowModel))]
    public class ModuleSettingWindowModel : ViewModel<IModuleSettingWindow>, IModuleSettingWindowModel
    {
        [ImportingConstructor]
        public ModuleSettingWindowModel(IModuleSettingWindow view, IEventAggregator aggregator) : base(view, aggregator)
        {
            View.Model = this;
        }
    }
}