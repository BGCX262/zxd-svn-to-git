using System;

namespace ZxdFramework.Authorization
{
    /// <summary>
    /// ��ȫ��Դ�Ľڵ��ʾ. һ����˵��ʾ��һ�������ֶ�����, 
    /// ����ֶε�Ҳ��������Ȩ�޶���Ĺ���. ������ֵҲ����������Ȩ�޽ṹ�е�Ψһ��
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
        /// ��ʾ������
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { protected set; get; }

        /// <summary>
        /// �ڵ�ͼ��
        /// </summary>
        /// <value>
        /// The icon.
        /// </value>
        public string Icon { set; get; }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { set; get; }
    }
}