using System;

namespace ZxdFramework.DataContract.Page
{
    /// <summary>
    /// Razor ��ͼģ��
    /// </summary>
    public class RazorView : Entity<Guid>
    {
        /// <summary>
        /// ���û�ȡ��ͼ����
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public virtual string Name { set; get; }

        /// <summary>
        /// ���û�ȡ����������, Ψһ����ͼ����. ����Ϊ��ȡģ����ͼ������
        /// </summary>
        /// <value>
        /// The name of the controller.
        /// </value>
        public virtual string ControllerName { set; get; }


        /// <summary>
        /// ��ͼģ���˵��
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public virtual string Description { set; get; }


        /// <summary>
        /// ���û�ȡģ����µ�ʱ��
        /// </summary>
        /// <value>
        /// The update time.
        /// </value>
        public virtual DateTime UpdateTime { set; get; }


        /// <summary>
        /// �������߸����û�
        /// </summary>
        /// <value>
        /// The create user.
        /// </value>
        public virtual Guid UpdateUser { set; get; }

        /// <summary>
        /// ���û�ȡ��ͼ������
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public virtual string Content { set; get; }

        /// <summary>
        /// ģ������
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public virtual ViewCategory Category { set; get; }
    }


    /// <summary>
    /// ��ͼ����
    /// </summary>
    public enum ViewCategory
    {
        /// <summary>
        /// ģ��
        /// </summary>
        Master,
        /// <summary>
        /// ��ͼģ��
        /// </summary>
        View,
        /// <summary>
        /// ����
        /// </summary>
        Partial,
        /// <summary>
        /// ����
        /// </summary>
        Shared
    }
}