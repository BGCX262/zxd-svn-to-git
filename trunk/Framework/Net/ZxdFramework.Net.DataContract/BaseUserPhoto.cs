using System;
using System.IO;

namespace ZxdFramework.DataContract
{
    /// <summary>
    /// ��ȡ�û�ͷ���ģ��
    /// </summary>
    public class BaseUserPhoto : Entity<Guid>
    {
        /// <summary>
        /// ���û�ȡ�û�
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public virtual IUser Owner { set; get; }

        /// <summary>
        /// ���û�ȡͼ������
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public virtual string Type { set; get; }

        /// <summary>
        /// ��ȡͼ���С
        /// </summary>
        public virtual long Lenght
        {
            protected set { }
            get { return Data == null ? 0L : Data.Length; }
        }

        /// <summary>
        /// ��ȡͼ������
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public virtual byte[] Data { set; get; } 
    }
}