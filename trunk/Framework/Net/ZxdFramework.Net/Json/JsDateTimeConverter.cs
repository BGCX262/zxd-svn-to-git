using System;
using System.Globalization;
using Common.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Spring.Util;

namespace ZxdFramework.Json
{
    /// <summary>
    /// 定义转换Json的时间值, 重写了服务器获取到的时间反序列化时参考自己的时间位移.
    /// </summary>
    public class JsDateTimeConverter : JavaScriptDateTimeConverter
    {
        private readonly long _tick = 621356256000000000L;

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {

            Type t = (ReflectionUtils.IsNullableType(objectType))
              ? Nullable.GetUnderlyingType(objectType)
              : objectType;

            if (reader.TokenType == JsonToken.Null)
            {
                if (!ReflectionUtils.IsNullableType(objectType))
                    throw new Exception("Cannot convert null value to {0}.");

                return null;
            }

            if (reader.TokenType != JsonToken.StartConstructor || string.Compare(reader.Value.ToString(), "Date", StringComparison.Ordinal) != 0)
                throw new Exception("Unexpected token or value when parsing date. Token: {0}, Value: {1}");

            reader.Read();

            if (reader.TokenType != JsonToken.Integer)
                throw new Exception("Unexpected token parsing date. Expected Integer, got {0}.");

            long ticks = (long)reader.Value;


            var d = new DateTime((ticks * 10000) + _tick, DateTimeKind.Local);


            reader.Read();

            if (reader.TokenType != JsonToken.EndConstructor)
                throw new Exception("Unexpected token parsing date. Expected EndConstructor, got {0}.");

            return d;
        }
    }
}