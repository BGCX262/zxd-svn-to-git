using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ZxdFramework.DataContract.IM
{
    /// <summary>
    /// ��ʱͨѶ�û�����
    /// </summary>
    [DataContract, Serializable]
    public class IMUser : IModel
    {
        [DataMember]
        public Guid Id { set; get; }


        /// <summary>
        /// ���û�ȡͨѶ�û��Ŀͻ���
        /// </summary>
        /// <value>The client.</value>
        public IIMClient Client { set; get; }

        private ICollection<IMUserGroup> _groups;
        /// <summary>
        /// ���û�ȡ�û�����
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
        /// ���û�ȡ�û�IP��ַ
        /// </summary>
        /// <value>The TCP address.</value>
        [DataMember]
        public string TcpAddress { set; get; }
        
    }
}