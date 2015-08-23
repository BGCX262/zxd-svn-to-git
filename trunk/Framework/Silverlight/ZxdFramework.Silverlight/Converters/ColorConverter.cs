using System;
using System.Globalization;
using System.Windows.Media;
using ZxdFramework.Helpers;

namespace ZxdFramework.Converters
{
    /// <summary>
    /// 颜色值的转换
    /// </summary>
	public class ColorConverter : ValueConverter
	{

        /// <summary>
        /// 在将源数据传递到目标以在 UI 中显示之前，对源数据进行修改。
        /// </summary>
        /// <param name="value">正传递到目标的源数据。</param>
        /// <param name="targetType">目标依赖项属性需要的数据的 <see cref="T:System.Type"/>。</param>
        /// <param name="parameter">要在转换器逻辑中使用的可选参数。</param>
        /// <param name="culture">转换的区域性。</param>
        /// <returns>要传递到目标依赖项属性的值。</returns>
	    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	    {

            if (value is String)
            {
                if (value.ToString().StartsWith("#"))
                {
                    var c = GeneralHelper.GetColorFromHex(value.ToString());
                    return new SolidColorBrush(c);
                }

                try
                {
                    var c = GeneralHelper.GetColorFromString(value as string);
                    return new SolidColorBrush(c);
                }
                catch
                {
                    return null;
                }
            }
            else if (value is Color)
            {
                return new SolidColorBrush((Color)value);
            }

            return null;
	    }
	}
}