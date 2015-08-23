using System;
using System.Collections.Generic;

namespace ZxdFramework.Service
{
    /// <summary>
    /// �������ݷ��ض���
    /// </summary>
    public interface IJsonResponse
    {
        /// <summary>
        /// ��ȡ���ӵ�ִ��ָ��
        /// </summary>
        string Command { get; }

        /// <summary>
        /// ��ȡִ�н��������
        /// </summary>
        /// <value>
        /// The type of the data.
        /// </value>
        ResultType ResultType { get; }

        /// <summary>
        /// ���û�ȡ Json ��ԭʼ����
        /// </summary>
        /// <value>
        /// The json data.
        /// </value>
        string JsonData { set; get; }
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
        /// Gets the post data.
        /// </summary>
        /// <param name="resultType">Type of the result.</param>
        /// <returns></returns>
        object GetPostData(Type resultType);


    }


    /// <summary>
    /// ��������
    /// </summary>
    public enum ResultType
    {
        /// <summary>
        /// �ɹ�
        /// </summary>
        Success,
        /// <summary>
        /// ʧ��
        /// </summary>
        Fail,
        /// <summary>
        /// ����
        /// </summary>
        Error,
        
    }
}