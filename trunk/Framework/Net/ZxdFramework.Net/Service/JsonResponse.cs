using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using JsonConverter = ZxdFramework.Json.JsonConverter;
using System.IO;

namespace ZxdFramework.Service
{
    /// <summary>
    /// 服务器返回对象实现类. 
    /// </summary>
    public class JsonResponse : IJsonResponse
    {
        public const string ResponseHeader = "_jsonresponseheader";
        private object _param;
        private IDictionary<string, object> _params;

        #region IJsonResponse Members

        /// <summary>
        /// 获取附加的执行指令
        /// </summary>
        public string Command { set; get; }

        /// <summary>
        /// 获取执行结果的类型
        /// </summary>
        /// <value>
        /// The type of the data.
        /// </value>
        public ResultType ResultType { set; get; }



        private string _jsonData;
        /// <summary>
        /// 设置获取 Json 的原始数据
        /// </summary>
        /// <value>
        /// The json data.
        /// </value>
        [JsonIgnore]
        public string JsonData 
        {
            get { return _jsonData; }
            set { 
                ContextLenght = string.IsNullOrEmpty(value) ? 0 : value.Length;
                _jsonData = value;
            }
        }

        /// <summary>
        /// 请求内容的长度, 当设置返回数据后会同时设置数据长度. 如果需要更改内容的特殊长度值. 就可以更改这个值
        /// </summary>
        /// <value>
        /// The context lenght.
        /// </value>
        public int ContextLenght { set; get; }

        /// <summary>
        /// 设置获取对当前传输的数据是否加密
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is encrypt; otherwise, <c>false</c>.
        /// </value>
        public bool IsEncrypt { set; get; }


        /// <summary>
        /// 以键值对的方式获取请求参数
        /// </summary>
        /// <value>The paramters.</value>
        [JsonIgnore]
        public IDictionary<string, object> Paramters
        {
            get { return _params ?? (_params = GetPostData<Dictionary<string, object>>()); }
        }

        /// <summary>
        /// 获取转换后的请求数据对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetPostData<T>()
        {
            if (_param == null || !(_param is T))
                _param = JsonConverter.DeserializeObject<T>(JsonData);
            return (T) _param;
        }


        /// <summary>
        /// 获取转换后的请求数据对象
        /// </summary>
        /// <_param name="resultType">转换的类型</_param>
        /// <returns></returns>
        public object GetPostData(Type resultType)
        {
            if (_param == null || !_param.GetType().IsSubclassOf(resultType))
            {
                _param = JsonConverter.DeserializeObject(resultType, JsonData);
            }
            return _param;
        }

        /// <summary>
        /// 按照键值对的方式获取传输时候的参数对象
        /// </summary>
        /// <type_param name="T"/>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public T GetPostData<T>(string key)
        {
            return JsonConverter.DeserializeObject<T>(Paramters[key].ToString());
        }


        /// <summary>
        /// 从返回对象中提取 JsonResponse 对象
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        public static IJsonResponse GetJsonResponse(HttpWebResponse response)
        {
            var res =  JsonConverter.DeserializeObject<JsonResponse>(response.Headers[ResponseHeader]);

            if (res.ContextLenght < 0)
                throw new ZxdFrameworkException("返回对象的长度不能为负数, 有可能是返回过程中没有检查到对象的长度");
            //提取返回数据
            using (var stream = response.GetResponseStream())
            {
                res.ContextLenght = stream.Length < res.ContextLenght
                                        ? (int)stream.Length
                                        : res.ContextLenght;
                using (var read = new StreamReader(stream))
                {
                    var temp = new char[res.ContextLenght];
                    read.Read(temp, 0, res.ContextLenght);
                    res.JsonData = new string(temp);
                }
            }
            return res;
        }

        #endregion

        public override string ToString()
        {
            //ContextLenght = string.IsNullOrEmpty(JsonData) ? -1 : JsonData.Length;

            return JsonConverter.Serialize(this);
        }

    }
}