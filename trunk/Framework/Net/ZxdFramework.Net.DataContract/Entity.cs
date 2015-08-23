using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ZxdFramework.DataContract
{

    /// <summary>
    /// ����ģ�Ͷ������
    /// </summary>
    /// <typeparam name="TId">The type of the id.</typeparam>
    [DataContract]
    public abstract class Entity<TId> : IModel where TId : IComparable
    {

        /// <summary>
        /// ��ȡģ�Ͷ����Ψһ��ʾ����
        /// </summary>
        /// <value>The id.</value>
        [DataMember]
        [JsonProperty]        
        public virtual TId Id { protected set; get; }

        
        #region ��д����
        /// <summary>
        /// ͨ��ģ�͵��������Ƚ϶����Ƿ����
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with this instance.</param>
        /// <returns>
        /// 	<c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj == null || !(obj is Entity<TId>)) return false;
            return Id.Equals(((Entity<TId>) obj).Id);
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode() + Id.GetHashCode();
        }

        public static bool operator ==(Entity<TId> a, Entity<TId> b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object)a == null || (object)b == null) return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity<TId> a, Entity<TId> b)
        {
            return !(a == b);
        }
        #endregion
    }
}