using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;

namespace ZxdFramework.Json
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomerContractResolver : DefaultContractResolver
    {

        private class EmptyClass
        {

        }

        /// <summary>
        /// 提供序列化的需要忽略的对象类型中的属性
        /// </summary>
        private IDictionary<Type, ISet<string>> _ignoreDic = new Dictionary<Type, ISet<string>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerContractResolver"/> class.
        /// </summary>
        public CustomerContractResolver()
        {
        }


        /// <summary>
        /// 忽略类型中的属性名称
        /// </summary>
        /// <param name="modelType">类型</param>
        /// <param name="propertyName">如果属性为Null将忽略整个类型</param>
        public CustomerContractResolver Ignore(Type modelType, string propertyName)
        {

            if(!_ignoreDic.ContainsKey(modelType))
            {
                _ignoreDic.Add(modelType, new HashSet<string>());
            }

            if (string.IsNullOrEmpty(propertyName) && _ignoreDic[modelType] != null)
            {
                _ignoreDic[modelType] = null;
            }
                
            if(_ignoreDic[modelType] != null && !_ignoreDic[modelType].Contains(propertyName))
            {
                _ignoreDic[modelType].Add(propertyName);
            }
            return this;
        }

        /// <summary>
        /// Ignores the specified model type.
        /// </summary>
        /// <param name="modelType">Type of the model.</param>
        /// <returns></returns>
        public CustomerContractResolver Ignore(Type modelType)
        {
            return Ignore(modelType, null);
        }

        /// <summary>
        /// Resolves the contract for a given type.
        /// </summary>
        /// <param name="type">The type to resolve a contract for.</param>
        /// <returns>The contract for a given type.</returns>
        public override JsonContract ResolveContract(Type type)
        {
            //如果忽略的集合中没有提供属性的名称. 
            if (_ignoreDic.ContainsKey(type) && _ignoreDic[type] == null)
            {
                return new JsonObjectContract(typeof(EmptyClass));
            }

            return base.ResolveContract(type);
        }

        //protected override JsonObjectContract CreateObjectContract(Type objectType)
        //{
        //    Log.Debug("创建对象 : " + objectType.FullName);
        //    return base.CreateObjectContract(objectType);
        //}


        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            if(_ignoreDic.ContainsKey(type))
            {
                var propertys = _ignoreDic[type];
                if(propertys == null || propertys.Count < 1) 
                    return new List<JsonProperty>();
                var jsonPropertys = base.CreateProperties(type, memberSerialization);
                return (from property in jsonPropertys
                           where !propertys.Contains(property.PropertyName)
                           select property).ToList();
               
            }
            return base.CreateProperties(type, memberSerialization);
            
        }


        protected override JsonProperty CreateProperty(System.Reflection.MemberInfo member, MemberSerialization memberSerialization)
        {
            return base.CreateProperty(member, memberSerialization);
        }
    }


}