using System;
using Newtonsoft.Json;

namespace ZxdFramework.DataContract
{
    /// <summary>
    /// �������û�����
    /// </summary>
    public abstract class BaseUser : Entity<Guid>, IUser
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// Gets the name of the login.
        /// </summary>
        /// <value>
        /// The name of the login.
        /// </value>
        public virtual string LoginName { get; set; }

        /// <summary>
        /// ��¼����
        /// </summary>
        [JsonIgnore]
        public virtual string Password { set; get; }

        /// <summary>
        /// Gets the login time.
        /// </summary>
        public virtual DateTime? LoginTime { get; set; }
        /// <summary>
        /// ���û�ȡ�û���״̬<br/>
        /// ��ǰ��״ֵ̬������ϵͳ�ж��û��Ŀ���.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public virtual UserStates State { get; set; }

        /// <summary>
        /// ��ǰ�û��Ľ�ɫ����
        /// </summary>
        public virtual string[] RoleNames { set; get; }

        /// <summary>
        /// ���û�ȡ�û��ı�� ID, �������û��Ƿ��ظ���¼�ı��. ÿ��
        /// </summary>
        /// <value>
        /// The token id.
        /// </value>
        public virtual string TokenId { set; get; }

        /// <summary>
        /// ���û�ȡ��¼�Ĵ���
        /// </summary>
        /// <value>The count.</value>
        public virtual int Count { set; get; }

        /// <summary>
        /// ���û�ȡ���һ�ε�¼��IP
        /// </summary>
        /// <value>The last ip.</value>
        public virtual string LastIp { set; get; }

        /// <summary>
        /// ���û�ȡ�û�����
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public virtual Category Category { set; get; }


        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            var temp = obj as BaseUser;

            return temp != null && temp.Id == Id;

        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode() + 344;
        }

    }
}