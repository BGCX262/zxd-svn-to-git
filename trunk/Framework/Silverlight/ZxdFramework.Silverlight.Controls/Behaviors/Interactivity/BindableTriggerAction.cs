/**
 * 创建者：宗旭东
 *
 */

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interactivity;
using ZxdFramework.Controls.Components.Bindings;

namespace ZxdFramework.Controls.Behaviors.Interactivity
{
    public abstract class BindableTriggerAction<T> : TriggerAction<T>
        where T : FrameworkElement
    {
        Dictionary<DependencyProperty, BindingInfo> _pendingBindings;

        #region Properties

        public Binding IsEnabledBinding
        {
            get { return GetBinding(IsEnabledProperty); }
            set { SetBinding<bool>(IsEnabledProperty, value); }
        }

        #endregion

        #region Binding Helpers

        protected void SetBinding<P>(DependencyProperty bindingProperty, Binding value)
        {
            SetBinding(bindingProperty, typeof(P), value);
        }

        protected void SetBinding(DependencyProperty bindingProperty, Type bindingType, Binding value)
        {
            if (this.AssociatedObject != null)
                this.SetAttachedBinding(AssociatedObject, bindingProperty, bindingType, value);

            else
            {
                if (_pendingBindings == null)
                    _pendingBindings = new Dictionary<DependencyProperty, BindingInfo>();
                _pendingBindings.Add(bindingProperty,
                    new BindingInfo() { BindingProperty = bindingProperty, BindingType = bindingType, Binding = value });
            }
        }

        protected Binding GetBinding(DependencyProperty bindingProperty)
        {
            if (this.AssociatedObject != null)
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
            foreach (var bindingInfo in _pendingBindings.Values)
                this.SetAttachedBinding(AssociatedObject, bindingInfo);

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

        #region Trigger Related

        protected override void OnAttached()
        {
            base.OnAttached();
            ApplyAllPendingBindings();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            ClearAllBindings();
        }

        #endregion
    }
}