using System;
using System.Runtime.Serialization;

namespace ZxdFramework.DataContract.IM
{
    /// <summary>
    /// 通讯用户设置模型
    /// </summary>
    
    [DataContract]
    public class IMUserSetting
    {
        /// <summary>
        /// Id, 当前的主键等同于用户主键
        /// </summary>
        [DataMember]
        public Guid Id { set; get; }


        /// <summary>
        /// Gets or sets the group.
        /// </summary>
        /// <value>The group.</value>
        public IMUserGroup Group { set; get; }

        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        public States State { set; get; }

        /// <summary>
        /// 是否支持脚步运行
        /// </summary>
        [DataMember]
        public bool IsScriptSupport { set; get; }

        /// <summary>
        /// 个性签名
        /// </summary>
        [DataMember]
        public string Signing { set; get; }

        /// <summary>
        /// 用户设置名称
        /// </summary>
        [DataMember]
        public string Name { set; get; }

        /// <summary>
        /// 设置获取更新时间
        /// </summary>
        /// <value>The last update.</value>
        [DataMember]
        public DateTime UpdateTime { set; get; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (IMUserSetting)) return false;
            return Equals((IMUserSetting) obj);
        }

        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        public bool Equals(IMUserSetting other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id.Equals(Id) && Equals(other.Group, Group);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id.GetHashCode()*397) ^ (Group != null ? Group.GetHashCode() : 0);
            }
        }
    }
}