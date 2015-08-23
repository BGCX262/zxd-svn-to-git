using System;
using System.Collections.Generic;

namespace ZxdFramework.Service
{
    /// <summary>
    /// 定义 Json 请求的描述信息
    /// </summary>
    public interface IJsonRequest
    {

        /// <summary>
        /// 获取附加的执行指令
        /// </summary>
        string Command { get; }

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
        /// 描述传输的数据类型
        /// </summary>
        /// <value>The type of the data.</value>
        string DataType { get; }
        /// <summary>
        /// Gets or sets the json data.
        /// </summary>
        /// <value>The json data.</value>
        string JsonData { get; }
        /// <summary>
        /// 设置获取当前请求的类型
        /// </summary>
        /// <value>The category.</value>
        RequestCategory Category { get; }
        /// <summary>
        /// 获取请求用户的ID
        /// </summary>
        /// <value>The username.</value>
        string UserId { get; }
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
        /// 获取提交的数据
        /// </summary>
        /// <param name="resultType">Type of the result.</param>
        /// <returns></returns>
        object GetPostData(Type resultType);

    }

    /// <summary>
    /// 定义了请求的类型
    /// </summary>
    public enum RequestCategory
    {
        /// <summary>
        /// 数据请求
        /// </summary>
        Data = 1,
        /// <summary>
        /// 只读数据请求
        /// </summary>
        Readonly,
        /// <summary>
        /// 删除请求
        /// </summary>
        Delete,
        /// <summary>
        /// 更新请求
        /// </summary>
        Update,
        /// <summary>
        /// 未知
        /// </summary>
        Unkonw
    }
}