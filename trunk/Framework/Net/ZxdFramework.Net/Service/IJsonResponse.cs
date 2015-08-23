using System;
using System.Collections.Generic;

namespace ZxdFramework.Service
{
    /// <summary>
    /// 定义数据返回对象
    /// </summary>
    public interface IJsonResponse
    {
        /// <summary>
        /// 获取附加的执行指令
        /// </summary>
        string Command { get; }

        /// <summary>
        /// 获取执行结果的类型
        /// </summary>
        /// <value>
        /// The type of the data.
        /// </value>
        ResultType ResultType { get; }

        /// <summary>
        /// 设置获取 Json 的原始数据
        /// </summary>
        /// <value>
        /// The json data.
        /// </value>
        string JsonData { set; get; }
        /// <summary>
        /// 请求内容的长度
        /// </summary>
        /// <value>The context lenght.</value>
        int ContextLenght { get; }
        /// <summary>
        /// 设置获取对当前传输的数据是否加密
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is encrypt; otherwise, <c>false</c>.
        /// </value>
        bool IsEncrypt { get; }

        /// <summary>
        /// 以键值对的方式获取请求参数
        /// </summary>
        /// <value>The paramters.</value>
        IDictionary<string, object> Paramters { get; }
        /// <summary>
        /// 获取转换后的请求数据对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetPostData<T>();
        /// <summary>
        /// 按照键值对的方式获取传输时候的参数对象
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
    /// 返回类型
    /// </summary>
    public enum ResultType
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success,
        /// <summary>
        /// 失败
        /// </summary>
        Fail,
        /// <summary>
        /// 出错
        /// </summary>
        Error,
        
    }
}