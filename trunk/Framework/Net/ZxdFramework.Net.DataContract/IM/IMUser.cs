using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ZxdFramework.DataContract.IM
{
    /// <summary>
    /// 即时通讯用户对象
    /// </summary>
    [DataContract, Serializable]
    public class IMUser : IModel
    {
        [DataMember]
        public Guid Id { set; get; }


        /// <summary>
        /// 设置获取通讯用户的客户端
        /// </summary>
        /// <value>The client.</value>
        public IIMClient Client { set; get; }

        private ICollection<IMUserGroup> _groups;
        /// <summary>
        /// 设置获取用户分组
        /// </summary>
        /// <value>The groups.</value>
        [DataMember]
        public ICollection<IMUserGroup> Groups
        {
            get { return _groups; }
            set
            {
                if(value != null && value != _groups)
                {
                    foreach (var userGroup in value)
                    {
                        userGroup.Owner = this;
                    }
                }
                _groups = value;
            }
        }


        /// <summary>
        /// 设置获取用户IP地址
        /// </summary>
        /// <value>The TCP address.</value>
        [DataMember]
        public string TcpAddress { set; get; }
        
    }
}