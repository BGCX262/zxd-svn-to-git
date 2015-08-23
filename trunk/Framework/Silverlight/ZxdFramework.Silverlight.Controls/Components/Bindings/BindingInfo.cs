/**
 * 创建者：宗旭东
 *
 */

using System;
using System.Windows;
using System.Windows.Data;

namespace ZxdFramework.Controls.Components.Bindings
{
    /// <summary>
    /// 绑定的描述信息
    /// </summary>
    public class BindingInfo
    {
        public DependencyProperty BindingProperty { get; set; }

        public Type BindingType { get; set; }

        public Binding Binding { get; set; }
    }
}