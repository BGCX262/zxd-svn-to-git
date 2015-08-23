using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;

namespace ZxdFramework.DataContract
{
    /// <summary>
    /// 视图绑定模型, 支持属性对视图的绑定
    /// </summary>
    public class ViewModel : INotifyPropertyChanged, IDisposable
    {
        public void Notify<TValue>(Expression<Func<TValue>> propertySelector)
        {
            if (PropertyChanged != null)
            {
                var memberExpression = propertySelector.Body as MemberExpression;
                if (memberExpression != null)
                {
                    NotifyPropertyChanged(memberExpression.Member.Name);
                }
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Invoqué lorsque l'objet doit être effacé
        /// </summary>
        public void Dispose()
        {
            Clean();
        }

        /// <summary>
        /// Méthode surchargeable pour y inclure la logique de nettoyage (supprimer les event handlers par ex.)
        /// </summary>
        public virtual void Clean()
        {
        }

#if DEBUG
        /// <summary>
        /// Utile lorsqu'on veut s'assurer que les objets du ViewModel sont bien garbage collectés
        /// </summary>
        ~ViewModel()
        {
            string msg = string.Format("{0} ({1}) Finalized", GetType().Name, GetHashCode());
            Debug.WriteLine(msg);
        }
#endif

        #endregion // IDisposable Members
    }
}