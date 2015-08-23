using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace ZxdFramework.DataContract.IM
{
    /// <summary>
    /// 通讯用户组
    /// </summary>
    [DataContract]
    public class IMUserGroup : IModel
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        [DataMember]
        public Guid Id { set; get; }


        /// <summary>
        /// 队组名称
        /// </summary>
        /// <value>The name.</value>
        [DataMember]
        public string Name { set; get; }

        /// <summary>
        /// 设置获取用户组的图标
        /// </summary>
        /// <value>The icon.</value>
        [DataMember]
        public string Icon { set; get; }


        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [DataMember]
        public IMGroupTypes Type { set; get; }


        private ICollection<IMUserSetting> _members;
        /// <summary>
        /// 队组成员
        /// </summary>
        /// <value>The members.</value>
        [DataMember]
        public ICollection<IMUserSetting> Members
        {
            get { return _members; }
            set
            {
                if(value != null && value != _members)
                {
                    foreach (var setting in value)
                    {
                        setting.Group = this;
                    }
                }

                _members = value;
            }
        }


        /// <summary>
        /// 设置获取所属对象
        /// </summary>
        /// <value>The owner.</value>
        [DataMember, JsonIgnore]
        public IMUser Owner { set; get; }


        /// <summary>
        /// 最后更新时间
        /// </summary>
        /// <value>The last update.</value>
        [DataMember]
        public DateTime UpdateTime { set; get; }


        //public override bool Equals(object obj)
        //{
        //    if (ReferenceEquals(null, obj)) return false;
        //    if (ReferenceEquals(this, obj)) return true;
        //    if (obj.GetType() != typeof (IMUserGroup)) return false;
        //    return Equals((IMUserGroup) obj);
        //}

        ///// <summary>
        ///// Equalses the specified other.
        ///// </summary>
        ///// <param name="other">The other.</param>
        ///// <returns></returns>
        //public bool Equals(IMUserGroup other)
        //{
        //    if (ReferenceEquals(null, other)) return false;
        //    if (ReferenceEquals(this, other)) return true;
        //    return Equals(other.Name, Name);
        //}

        //public override int GetHashCode()
        //{
        //    return (Name != null ? Name.GetHashCode() : 0);
        //}
    }


    /// <summary>
    /// 用户组的类型
    /// </summary>
    public enum IMGroupTypes
    {
        /// <summary>
        /// 
        /// </summary>
        自己 = -1,
        /// <summary>
        /// 
        /// </summary>
        Common = 1,
        /// <summary>
        /// 
        /// </summary>
        群,
        /// <summary>
        /// 
        /// </summary>
        讨论组
    }
}