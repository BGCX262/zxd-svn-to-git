using System;
using System.IO;

namespace ZxdFramework.DataContract
{
    /// <summary>
    /// 获取用户头像的模型
    /// </summary>
    public class BaseUserPhoto : Entity<Guid>
    {
        /// <summary>
        /// 设置获取用户
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public virtual IUser Owner { set; get; }

        /// <summary>
        /// 设置获取图像类型
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public virtual string Type { set; get; }

        /// <summary>
        /// 获取图像大小
        /// </summary>
        public virtual long Lenght
        {
            protected set { }
            get { return Data == null ? 0L : Data.Length; }
        }

        /// <summary>
        /// 获取图像数据
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public virtual byte[] Data { set; get; } 
    }
}