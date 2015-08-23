using System;
using System.Runtime.Serialization;

namespace ZxdFramework.DataContract.IM
{
    /// <summary>
    /// 即时通讯命令
    /// </summary>
    [DataContract]
    public class IMCommand
    {
        /// <summary>
        /// 发送对象
        /// </summary>
        /// <value>To.</value>
        [DataMember]
        public Guid To { set; get; }

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        /// <value>The owner.</value>
        [DataMember]
        public Guid Owner { set; get; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>The type.</value>
        [DataMember]
        public CommandTypes Type { set; get; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        [DataMember]
        public string Description { set; get; }

        /// <summary>
        /// Gets or sets the script.
        /// </summary>
        /// <value>The script.</value>
        [DataMember]
        public string Script { set; get; }
    }
}