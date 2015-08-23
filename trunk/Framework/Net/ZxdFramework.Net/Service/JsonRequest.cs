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
    /// Json ��������ͷ����Ϣ
    /// </summary>
    public class JsonRequest : IJsonRequest
    {
        /// <summary>
        /// ֻ��������ͷ����Ϣ��ָ����
        /// </summary>
        public const string JsonRequestHeader = "_jsonrequestheader";


        private JsonRequest()
        {
            //ContextLenght = -1;
        }

#if !SILVERLIGHT
        /// <summary>
        /// ��鵱ǰ�������Ƿ���Json��ʽ������
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public static bool CheckJsonRequest(HttpRequestBase request)
        {
            return !string.IsNullOrEmpty(request.Headers.Get(JsonRequestHeader));
        }

        /// <summary>
        /// �������������ȡ
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public static IJsonRequest GetJsonRequest(HttpRequestBase request)
        {
            if (!CheckJsonRequest(request))
                throw new ZxdFrameworkException("���������û�з���Json��ͷ����Ϣ, ���ȷ����Json���������Ƿ�ʹ�õĿ���ṩ�����󷽷�.");

            var res = JsonConverter.DeserializeObject<JsonRequest>(request.Headers.Get(JsonRequestHeader));

            if(res.ContextLenght < 0)
            {
                res.ContextLenght = (int)request.InputStream.Length;
            }
                //throw new ZxdFrameworkException("�������ĳ��Ȳ���Ϊ����, �п��������������û�м�鵽����ĳ���");
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
        /// ����һ���������, �����ȷ�������������. ��Я������Ķ���
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
        /// ��ȡ���ӵ�ִ��ָ��
        /// </summary>
        public string Command { set; get; }

        /// <summary>
        /// �������ݵĳ���
        /// </summary>
        /// <value>The context lenght.</value>
        public int ContextLenght { get; set; }

        /// <summary>
        /// ���û�ȡ�Ե�ǰ����������Ƿ����
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is encrypt; otherwise, <c>false</c>.
        /// </value>
        public bool IsEncrypt { get; set; }


        /// <summary>
        /// �����������������
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
        /// ���û�ȡ��ǰ���������
        /// </summary>
        /// <value>The category.</value>
        public RequestCategory Category { get; set; }

        /// <summary>
        /// ��ȡ�����û���ID
        /// </summary>
        /// <value>The username.</value>
        public string UserId { get; set; }


        private IDictionary<string, object> _params;

        /// <summary>
        /// �Լ�ֵ�Եķ�ʽ��ȡ�������
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
        /// ����Я���Ķ���
        /// </summary>
        [JsonIgnore]
        public object Param { private set; get; }
        
        /// <summary>
        /// ��ȡת������������ݶ���
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
        /// ��ȡת������������ݶ���
        /// </summary>
        /// <param name="resultType">ת��������</param>
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
        /// ���ռ�ֵ�Եķ�ʽ��ȡ����ʱ��Ĳ�������
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