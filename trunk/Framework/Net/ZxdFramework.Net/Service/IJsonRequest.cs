using System;
using System.Collections.Generic;

namespace ZxdFramework.Service
{
    /// <summary>
    /// ���� Json �����������Ϣ
    /// </summary>
    public interface IJsonRequest
    {

        /// <summary>
        /// ��ȡ���ӵ�ִ��ָ��
        /// </summary>
        string Command { get; }

        /// <summary>
        /// �������ݵĳ���
        /// </summary>
        /// <value>The context lenght.</value>
        int ContextLenght { get; }
        /// <summary>
        /// ���û�ȡ�Ե�ǰ����������Ƿ����
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is encrypt; otherwise, <c>false</c>.
        /// </value>
        bool IsEncrypt { get; }
        /// <summary>
        /// �����������������
        /// </summary>
        /// <value>The type of the data.</value>
        string DataType { get; }
        /// <summary>
        /// Gets or sets the json data.
        /// </summary>
        /// <value>The json data.</value>
        string JsonData { get; }
        /// <summary>
        /// ���û�ȡ��ǰ���������
        /// </summary>
        /// <value>The category.</value>
        RequestCategory Category { get; }
        /// <summary>
        /// ��ȡ�����û���ID
        /// </summary>
        /// <value>The username.</value>
        string UserId { get; }
        /// <summary>
        /// �Լ�ֵ�Եķ�ʽ��ȡ�������
        /// </summary>
        /// <value>The paramters.</value>
        IDictionary<string, object> Paramters { get; }
        /// <summary>
        /// ��ȡת������������ݶ���
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetPostData<T>();
        /// <summary>
        /// ���ռ�ֵ�Եķ�ʽ��ȡ����ʱ��Ĳ�������
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        T GetPostData<T>(string key);

        /// <summary>
        /// ��ȡ�ύ������
        /// </summary>
        /// <param name="resultType">Type of the result.</param>
        /// <returns></returns>
        object GetPostData(Type resultType);

    }

    /// <summary>
    /// ���������������
    /// </summary>
    public enum RequestCategory
    {
        /// <summary>
        /// ��������
        /// </summary>
        Data = 1,
        /// <summary>
        /// ֻ����������
        /// </summary>
        Readonly,
        /// <summary>
        /// ɾ������
        /// </summary>
        Delete,
        /// <summary>
        /// ��������
        /// </summary>
        Update,
        /// <summary>
        /// δ֪
        /// </summary>
        Unkonw
    }
}