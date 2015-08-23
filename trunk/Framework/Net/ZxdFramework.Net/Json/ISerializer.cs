using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ZxdFramework.Json
{
    /// <summary>
    /// 按照指定的方式序列化对象
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// 忽略类型中的一个属性
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        Serializer Ignore(string propertyName);

        /// <summary>
        /// 忽略指定类型的一个属性
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        Serializer Ignore(Type type, string propertyName);
        /// <summary>
        /// 忽略指定类型
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        Serializer Ignore(Type type);
        /// <summary>
        /// 忽略类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Serializer Ignore<T>();
        /// <summary>
        /// 忽略类型中的的属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        Serializer Ignore<T>(string propertyName);
        /// <summary>
        /// 序列化
        /// </summary>
        /// <returns></returns>
        string Serialize();
        /// <summary>
        /// 
        /// </summary>
        TypeNameHandling TypeNameHandling { get; set; }
        /// <summary>
        /// 
        /// </summary>
        FormatterAssemblyStyle TypeNameAssemblyFormat { get; set; }
        /// <summary>
        /// 
        /// </summary>
        PreserveReferencesHandling PreserveReferencesHandling { get; set; }
        /// <summary>
        /// 
        /// </summary>
        ReferenceLoopHandling ReferenceLoopHandling { get; set; }
        /// <summary>
        /// 
        /// </summary>
        MissingMemberHandling MissingMemberHandling { get; set; }
        /// <summary>
        /// Gets or sets the null value handling.
        /// </summary>
        /// <value>The null value handling.</value>
        NullValueHandling NullValueHandling { get; set; }
        /// <summary>
        /// Gets or sets the default value handling.
        /// </summary>
        /// <value>The default value handling.</value>
        DefaultValueHandling DefaultValueHandling { get; set; }
        /// <summary>
        /// Gets or sets the object creation handling.
        /// </summary>
        /// <value>The object creation handling.</value>
        ObjectCreationHandling ObjectCreationHandling { get; set; }
        /// <summary>
        /// Gets or sets the constructor handling.
        /// </summary>
        /// <value>The constructor handling.</value>
        ConstructorHandling ConstructorHandling { get; set; }
        /// <summary>
        /// Gets the converters.
        /// </summary>
        /// <value>The converters.</value>
        JsonConverterCollection Converters { get; }
        /// <summary>
        /// Gets or sets the contract resolver.
        /// </summary>
        /// <value>The contract resolver.</value>
        IContractResolver ContractResolver { get; set; }
        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>The context.</value>
        StreamingContext Context { get; set; }
    }
}