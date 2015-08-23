/**
 * 创建者：宗旭东
 *
 */

using System;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Controls;
using System.Windows.Markup;

namespace ZxdFramework.Controls.Converters
{
    public class ConvertFromStringConverter : TypeConverter
    {
        private readonly Type type;

        /// <summary>
        /// General purpose converter that converts from a string to the specified type.
        /// </summary>
        /// <param name="type"></param>
        public ConvertFromStringConverter(Type type)
        {
            this.type = type;
        }

        /// <summary>
        /// Allow conversion from strings.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return ((sourceType == typeof (string)) || base.CanConvertFrom(context, sourceType));
        }


        /// <summary>
        /// Convert the value
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var stringValue = value as string;
            if (stringValue != null)
            {
                if (type == typeof (bool))
                {
                    return bool.Parse(stringValue);
                }
                if (type.IsEnum)
                {
                    return Enum.Parse(type, stringValue, false);
                }
                var stringBuilder = new StringBuilder();
                stringBuilder.Append("<ContentControl xmlns='http://schemas.microsoft.com/client/2007' xmlns:c='" +
                                     ("clr-namespace:" + type.Namespace + ";assembly=" +
                                      type.Assembly.FullName.Split(new[] {','})[0]) + "'>\n");
                stringBuilder.Append("<c:" + type.Name + ">\n");
                stringBuilder.Append(stringValue);
                stringBuilder.Append("</c:" + type.Name + ">\n");
                stringBuilder.Append("</ContentControl>");

                var instance = XamlReader.Load(stringBuilder.ToString()) as ContentControl;

                if (instance != null)
                {
                    return instance.Content;
                }
            }
            return base.ConvertFrom(context, culture, value);
        }
    }
}