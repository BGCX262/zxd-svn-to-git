using System;
using System.Collections.Generic;

using Newtonsoft.Json;
using JsonConverter = ZxdFramework.Json.JsonConverter;
#if !SILVERLIGHT
using System.Web;
using System.IO;
#endif


namespace ZxdFramework.Service
{
    /// <summary>
    /// Json 数据请求头部信息
    /// </summary>
    public class JsonRequest : IJsonRequest
    {
        /// <summary>
        /// 只读的请求头部信息的指定建
        /// </summary>
        public const string JsonRequestHeader = "_jsonrequestheader";


        private JsonRequest()
        {
            //ContextLenght = -1;
        }

#if !SILVERLIGHT
        /// <summary>
        /// 检查当前的请求是否是Json格式的请求
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public static bool CheckJsonRequest(HttpRequestBase request)
        {
            return !string.IsNullOrEmpty(request.Headers.Get(JsonRequestHeader));
        }

        /// <summary>
        /// 在请求对象中提取
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public static IJsonRequest GetJsonRequest(HttpRequestBase request)
        {
            if (!CheckJsonRequest(request))
                throw new ZxdFrameworkException("请求对象中没有发现Json的头部信息, 如果确定是Json请求请检查是否使用的框架提供的请求方法.");

            var res = JsonConverter.DeserializeObject<JsonRequest>(request.Headers.Get(JsonRequestHeader));

            if(res.ContextLenght < 0)
            {
                res.ContextLenght = (int)request.InputStream.Length;
            }
                //throw new ZxdFrameworkException("请求对象的长度不能为负数, 有可能是请求过程中没有检查到对象的长度");
            else
            {
                res.ContextLenght = request.InputStream.Length < res.ContextLenght
                                        ? (int)request.InputStream.Length
                                        : res.ContextLenght;
            }
            


            using(var read = new StreamReader(request.InputStream))
            {
                var temp = new char[res.ContextLenght];
                read.Read(temp, 0, res.ContextLenght);
                res.JsonData = new string(temp);
            }
            return res;
        }

#endif
        /// <summary>
        /// 创建一个请求对象, 用于先服务器发送请求. 并携带传入的对象
        /// </summary>
        /// <param name="jsonData">The json data.</param>
        /// <returns></returns>
        public static IJsonRequest CreateJsonRequest(string jsonData)
        {
            var request = new JsonRequest
                              {
                                  ContextLenght = string.IsNullOrEmpty(jsonData) ? 0 : jsonData.Length,
                                  JsonData = jsonData
                              };
            return request;
        }


        /// <summary>
        /// Creates the json request.
        /// </summary>
        /// <param name="jsonObj">The json obj.</param>
        /// <returns></returns>
        public static IJsonRequest CreateJsonRequest(object jsonObj)
        {
            var data = JsonConverter.Serialize(jsonObj);

            var request = new JsonRequest
            {
                ContextLenght = data.Length,
                DataType = jsonObj.GetType().Name,
                JsonData = data
            };
            return request;
        }

        /// <summary>
        /// 获取附加的执行指令
        /// </summary>
        public string Command { set; get; }

        /// <summary>
        /// 请求内容的长度
        /// </summary>
        /// <value>The context lenght.</value>
        public int ContextLenght { get; set; }

        /// <summary>
        /// 设置获取对当前传输的数据是否加密
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is encrypt; otherwise, <c>false</c>.
        /// </value>
        public bool IsEncrypt { get; set; }


        /// <summary>
        /// 描述传输的数据类型
        /// </summary>
        /// <value>The type of the data.</value>
        public string DataType { get; set; }

        /// <summary>
        /// Gets or sets the json data.
        /// </summary>
        /// <value>The json data.</value>
        [JsonIgnore]
        public string JsonData { get; private set; }

        /// <summary>
        /// 设置获取当前请求的类型
        /// </summary>
        /// <value>The category.</value>
        public RequestCategory Category { get; set; }

        /// <summary>
        /// 获取请求用户的ID
        /// </summary>
        /// <value>The username.</value>
        public string UserId { get; set; }


        private IDictionary<string, object> _params;

        /// <summary>
        /// 以键值对的方式获取请求参数
        /// </summary>
        /// <value>The paramters.</value>
        [JsonIgnore]
        public IDictionary<string, object> Paramters
        {
            get
            {
                return _params ?? (_params = GetPostData<Dictionary<string, object>>());
            }
        }

        /// <summary>
        /// 请求携带的对象
        /// </summary>
        [JsonIgnore]
        public object Param { private set; get; }
        
        /// <summary>
        /// 获取转换后的请求数据对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetPostData<T>()
        {
            if(Param == null || !(Param is T))
                Param = JsonConverter.DeserializeObject<T>(JsonData);
            return (T) Param;
        }


        /// <summary>
        /// 获取转换后的请求数据对象
        /// </summary>
        /// <param name="resultType">转换的类型</param>
        /// <returns></returns>
        public object GetPostData(Type resultType)
        {
            if (Param == null || !Param.GetType().IsSubclassOf(resultType))
            {
                Param = JsonConverter.DeserializeObject(resultType, JsonData);
            }
            return Param;
        }

        /// <summary>
        /// 按照键值对的方式获取传输时候的参数对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public T GetPostData<T>(string key)
        {
            return JsonConverter.DeserializeObject<T>(Paramters[key].ToString());
        }

        public override string ToString()
        {
            //ContextLenght = string.IsNullOrEmpty(JsonData) ? -1 : JsonData.Length;

            return JsonConverter.Serialize(this);
        }
    }
}