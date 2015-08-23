using System;
using System.Runtime.Serialization;

namespace ZxdFramework.DataContract.IM
{
    /// <summary>
    /// 信息
    /// </summary>
    [DataContract]
    public class Message
    {
        /// <summary>
        /// 发送人
        /// </summary>
        [DataMember]
        public Guid Owner;
        /// <summary>
        /// 接收对象
        /// </summary>
        [DataMember]
        public Guid To;

        /// <summary>
        /// 短信类型
        /// </summary>
        [DataMember]
        public int Type;

        /// <summary>
        /// 短信数据
        /// </summary>
        [DataMember]
        public string Data;
        /// <summary>
        /// 短信内容
        /// </summary>
        [DataMember]
        public byte[] Content;

        /// <summary>
        /// 携带的脚步信息
        /// </summary>
        [DataMember]
        public string Script;
    }
}