using System;
using ZxdFramework.DataContract;

namespace ZxdFramework.Authorization
{
    /// <summary>
    /// �û�����ֵ
    /// </summary>
    public class UserSetting
    {
        /// <summary>
        /// ���û�ȡ�����û�
        /// </summary>
        /// <value>
        /// The owner.
        /// </value>
        public virtual Guid OwnerId { set; get; }


        /// <summary>
        /// �û�ֵ�ļ�
        /// </summary>
        /// <value>
        /// The key.
        /// </value>
        public virtual string Key { set; get; }


        /// <summary>
        /// �û����õ�ֵ
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public virtual string Value { set; get; }


        /// <summary>
        /// ����������
        /// </summary>
        /// <value>
        /// The last update.
        /// </value>
        public virtual DateTime LastUpdate { set; get; }


        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            var t = obj as UserSetting;

            return t != null && t.Key == Key && t.OwnerId == OwnerId;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return (Key != null ? Key.GetHashCode()  : 22)
                + OwnerId.GetHashCode();
        }

        public override string ToString()
        {
            return Key + " = " + Value;
        }
    }
}