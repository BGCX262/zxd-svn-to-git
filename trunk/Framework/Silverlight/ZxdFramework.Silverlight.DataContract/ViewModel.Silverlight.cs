using System.ComponentModel.Composition;
using System.Windows;
using Microsoft.Practices.Prism.Events;
using Newtonsoft.Json;

namespace ZxdFramework.DataContract
{
    /// <summary>
    /// 专门用于绑定视图的模型, 提供了对 MVVM 模式的支持
    /// </summary>
    /// <typeparam name="TView">视图接口</typeparam>
    public class ViewModel<TView> : ViewModel, IViewSupport
    {
        [ImportingConstructor]
        public ViewModel(TView view, IEventAggregator aggregator)
        {
            EventAggregator = aggregator;
            View = view;
        }

        [JsonIgnore]
        public IEventAggregator EventAggregator { get; protected set; }


        [JsonIgnore]
        public TView View { get; protected set; }

        /// <summary>
        /// 使用接口获取的视图对象
        /// </summary>
        [JsonIgnore]
        object IViewSupport.View 
        {
            get { return View; }
        }

        public override void Clean()
        {
            base.Clean();

            if (View is FrameworkElement)
            {
                var element = View as FrameworkElement;
                element.RemoveFromParentPanel();
            }
        }
    }
}