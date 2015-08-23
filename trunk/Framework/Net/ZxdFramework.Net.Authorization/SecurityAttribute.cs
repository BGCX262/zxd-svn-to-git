using System;

namespace ZxdFramework.Authorization
{
    /// <summary>
    /// Ȩ�ޱ�ǩ, �����ڷ���֮��. ���õ�ǰ�ķ���ִ�а�ȫ���
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class SecurityAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityAttribute"/> class.
        /// </summary>
        public SecurityAttribute()
        {
            CheckScript = true;
        }


        /// <summary>
        /// ���û�ȡ�Ƿ�ִ�н�ɫ�еĽű����, Ĭ��True
        /// </summary>
        /// <value>
        ///   <c>true</c> if [check script]; otherwise, <c>false</c>.
        /// </value>
        public bool CheckScript { set; get; }

        /// <summary>
        /// ���û�ȡ��ǰ���������Ľ�ɫ, ��ɫ�ļ�������ڽڵ�ļ��
        /// </summary>
        /// <value>
        /// The role names.
        /// </value>
        public string[] RoleNames { set; get; }


        /// <summary>
        /// ���û�ȡ�û��Ľ�ɫ���߲����а�����ǰ�Ĳ����ڵ�
        /// </summary>
        /// <value>
        /// The action key.
        /// </value>
        public string ActionKey { set; get; }

        /// <summary>
        /// ���û�ȡ�����֤ʧ�ܺ����ʾ��Ϣ
        /// </summary>
        /// <value>
        /// The fail message.
        /// </value>
        public string FailMessage { set; get; }
    }
}