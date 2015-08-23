using System;

namespace ZxdFramework.Authorization
{
    /// <summary>
    /// 安全资源的节点标示. 一般来说标示到一个常量字段上面, 
    /// 这个字段的也必须满足权限定义的规则. 常量的值也必须是整个权限结构中的唯一的
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class SecurityActionAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityActionAttribute"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public SecurityActionAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// 显示的名称
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { protected set; get; }

        /// <summary>
        /// 节点图表
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        public string Icon { set; get; }

        /// <summary>
        /// 描述信息
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { set; get; }
    }
}