/**
 * 创建者：宗旭东
 *
 */

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;

namespace ZxdFramework.Controls.Components.Bindings
{
    /// <summary>
    /// 对依赖类型的绑定支持
    /// </summary>
    public abstract class BindableDependencyObject : DependencyObject
    {
        private Dictionary<DependencyProperty, BindingInfo> _pendingBindings;

        protected abstract FrameworkElement AttachTarget { get; }

        #region Binding Helpers

        protected void SetBinding<T>(DependencyProperty bindingProperty, Binding value)
        {
            SetBinding(bindingProperty, typeof (T), value);
        }

        protected void SetBinding(DependencyProperty bindingProperty, Type bindingType, Binding value)
        {
            if (AttachTarget != null)
                this.SetAttachedBinding(AttachTarget, bindingProperty, bindingType, value);

            else
            {
                if (_pendingBindings == null)
                    _pendingBindings = new Dictionary<DependencyProperty, BindingInfo>();
                _pendingBindings.Add(bindingProperty,
                                     new BindingInfo
                                         {BindingProperty = bindingProperty, BindingType = bindingType, Binding = value});
            }
        }

        protected Binding GetBinding(DependencyProperty bindingProperty)
        {
            if (AttachTarget != null)
                return this.GetAttachedBinding(bindingProperty);

            else if (_pendingBindings != null && _pendingBindings.ContainsKey(bindingProperty))
                return _pendingBindings[bindingProperty].Binding;

            else
                return null;
        }

        protected void ApplyAllPendingBindings()
        {
            if (_pendingBindings == null || _pendingBindings.Count == 0) return;

            // set all bindings
            foreach (BindingInfo _bindingInfo in _pendingBindings.Values)
                this.SetAttachedBinding(AttachTarget, _bindingInfo);

            // clear the pending
            _pendingBindings.Clear();
            _pendingBindings = null;
        }

        protected void ClearAllBindings()
        {
            if (_pendingBindings != null) _pendingBindings = null;
            this.ClearAttachedBindings();
        }

        #endregion
    }
}