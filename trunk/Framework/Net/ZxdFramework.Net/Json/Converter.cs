using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ZxdFramework.Json
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonConverter
    {
        /// <summary>
        /// ��ȡȫ�ֵ����л�������
        /// </summary>
        /// <value>The global settings.</value>
        public static JsonSerializerSettings GlobalSettings { private set; get; }
        static JsonConverter()
        {
            GlobalSettings = new JsonSerializerSettings
                                 {
                                     MissingMemberHandling = MissingMemberHandling.Ignore,
                                     //NullValueHandling = NullValueHandling.Include,
                                     NullValueHandling = NullValueHandling.Ignore,
                                     PreserveReferencesHandling = PreserveReferencesHandling.Objects,
                                     ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                                     ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                                     
                                 };
            
            GlobalSettings.Converters.Add(new IsoDateTimeConverter());
        }

        /// <summary>
        /// �����л�Json����
        /// </summary>
        /// <typeparam name="TArgetType">The type of the arget type.</typeparam>
        /// <param name="jsonValue">The json value.</param>
        /// <returns></returns>
        public static TArgetType DeserializeObject<TArgetType>(string jsonValue)
        {
            return JsonConvert.DeserializeObject<TArgetType>(jsonValue, GlobalSettings);
        }

        /// <summary>
        /// Deserializes the object.
        /// </summary>
        /// <param name="toType">To type.</param>
        /// <param name="jsonValue">The json value.</param>
        /// <returns></returns>
        public static object DeserializeObject(Type toType, string jsonValue)
        {
            return JsonConvert.DeserializeObject(jsonValue, toType, GlobalSettings);
        }

        /// <summary>
        /// ���л�����
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.None, GlobalSettings);
        }
    }
}