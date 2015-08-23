using System;
using System.Reflection;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ZxdFramework.Helpers
{


    /// <summary>
    /// 基本的转换帮助类
    /// </summary>
    public class GeneralHelper
    {



        #region 颜色值处理
        /// <summary>
        /// 获取描述颜色名称的对象
        /// </summary>
        /// <param name="colorString">The color string.</param>
        /// <returns></returns>
        public static Color GetColorFromString(string colorString)
        {
            var colorType = (typeof(Colors));
            if (colorType.GetProperty(colorString) != null)
            {
                var color = colorType.InvokeMember(colorString, BindingFlags.GetProperty, null, null, null);
                if (color != null)
                {
                    return (Color)color;
                }
            }
            else
            {
                try
                {
                    var lne = (Line)XamlReader.Load("<Line xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" Fill=\"" + colorString + "\" />");
                    return (Color)lne.Fill.GetValue(SolidColorBrush.ColorProperty);
                }
                catch { }
            }
            throw new InvalidCastException("Color not defined");
        }

        /// <summary>
        /// 获取16进制的颜色值
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        public static Color GetColorFromHex(string s)
        {
            if (s.StartsWith("#"))
            {
                s = s.Substring(1);
            }
            var a = Convert.ToByte(s.Substring(0, 2), 16);
            var r = Convert.ToByte(s.Substring(2, 2), 16);
            var g = Convert.ToByte(s.Substring(4, 2), 16);
            var b = Convert.ToByte(s.Substring(6, 2), 16);
            return Color.FromArgb(a, r, g, b);
        }

        /// <summary>
        /// 获取颜色的16进制值
        /// </summary>
        /// <param name="c">The c.</param>
        /// <returns></returns>
        public static string GetHexFromColor(Color c)
        {
            return string.Format("#{0}{1}{2}{3}",
                    c.A.ToString("X2"),
                    c.R.ToString("X2"),
                    c.G.ToString("X2"),
                    c.B.ToString("X2"));
        } 
        #endregion
        
    }
}