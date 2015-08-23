using System;

namespace ZxdFramework.DataContract.UI
{
    /// <summary>
    /// �ṩ�ڴ�����ͼ��Ҫ���õ��ķ���, һ�����ͼ�ӿڼ̳�
    /// </summary>
    public interface IWindow
    {
        /// <summary>
        /// ������رմ������¼�
        /// </summary>
        /// <value>The closed.</value>
        event EventHandler Closed;

        /// <summary>
        /// ��ʾ����
        /// </summary>
        void Show();

        /// <summary>
        /// �رմ���
        /// </summary>
        void Close();

    }
}