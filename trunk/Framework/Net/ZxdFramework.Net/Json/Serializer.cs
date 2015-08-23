using System;
using System.Dynamic;
using System.IO;
using Newtonsoft.Json;

namespace ZxdFramework.Json
{
    /// <summary>
    /// 序列或者反序列化Json数据. 固化并封装了序列化需要的基本设置.
    /// </summary>
    public sealed class Serializer : JsonSerializer, ISerializer
    {
        /// <summary>
        /// 
        /// </summary>
        private object _targetObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="Serializer"/> class.
        /// </summary>
        /// <param name="targetObject">序列化的目标对象</param>
        public Serializer(object targetObject)
        {
            _targetObject = targetObject;
            PreserveReferencesHandling = JsonConverter.GlobalSettings.PreserveReferencesHandling;
            MissingMemberHandling = JsonConverter.GlobalSettings.MissingMemberHandling;
            NullValueHandling = JsonConverter.GlobalSettings.NullValueHandling;
            PreserveReferencesHandling = JsonConverter.GlobalSettings.PreserveReferencesHandling;
            ConstructorHandling = JsonConverter.GlobalSettings.ConstructorHandling;
            ReferenceLoopHandling = JsonConverter.GlobalSettings.ReferenceLoopHandling;
            ContractResolver = new CustomerContractResolver();
            foreach (var converter in JsonConverter.GlobalSettings.Converters)
            {
                Converters.Add(converter);
            }
        }

        /// <summary>
        /// Ignores the specified property name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public Serializer Ignore(string propertyName)
        {
            ((CustomerContractResolver) ContractResolver).Ignore(_targetObject.GetType(), propertyName);
            return this;
        }

        /// <summary>
        /// Ignores the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public Serializer Ignore(Type type, string propertyName)
        {
            ((CustomerContractResolver)ContractResolver).Ignore(type, propertyName);
            return this;
        }

        /// <summary>
        /// Ignores the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public Serializer Ignore(Type type)
        {
            ((CustomerContractResolver)ContractResolver).Ignore(type, null);
            return this;
        }

        /// <summary>
        /// Ignores this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Serializer Ignore<T>()
        {
            return Ignore(typeof (T));
        }

        /// <summary>
        /// Ignores the specified property name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public Serializer Ignore<T>(string propertyName)
        {
            return Ignore(typeof (T), propertyName);
        }




        /// <summary>
        /// Serializes this instance.
        /// </summary>
        /// <returns></returns>
        public string Serialize()
        {
            using (var sw = new StringWriter())
            using (var writer = new JsonTextWriter(sw))
            {
                writer.Formatting = Formatting.Indented;
                //JsonSerializer.Serialize(writer, obj);
                Serialize(writer, _targetObject);
                // {"Expiry":new Date(1230375600000),"Price":0}
                //log.Debug(sw.ToString());
                return sw.ToString();
            }
        }

        public override string ToString()
        {
            return Serialize();
        }

    }
}