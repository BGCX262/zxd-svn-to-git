using System;
using System.Globalization;
using System.Windows.Data;

namespace ZxdFramework.Converters
{
    /// <summary>
    /// 值转换的抽象类. 针对很多的值只需要在配置的Xaml文件中转换成对应的对象值. 而不需要反向转换. 可以继承这个辅助类
    /// </summary>
    public abstract class ValueConverter : IValueConverter
    {
        /// <summary>
        /// 在将源数据传递到目标以在 UI 中显示之前，对源数据进行修改。
        /// </summary>
        /// <param name="value">正传递到目标的源数据。</param>
        /// <param name="targetType">目标依赖项属性需要的数据的 <see cref="T:System.Type"/>。</param>
        /// <param name="parameter">要在转换器逻辑中使用的可选参数。</param>
        /// <param name="culture">转换的区域性。</param>
        /// <returns>
        /// 要传递到目标依赖项属性的值。
        /// </returns>
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// 在将目标数据传递到源对象之前，对目标数据进行修改。此方法仅在 <see cref="F:System.Windows.Data.BindingMode.TwoWay"/> 绑定中进行调用。
        /// </summary>
        /// <param name="value">正传递到源的目标数据。</param>
        /// <param name="targetType">源对象需要的数据的 <see cref="T:System.Type"/>。</param>
        /// <param name="parameter">要在转换器逻辑中使用的可选参数。</param>
        /// <param name="culture">转换的区域性。</param>
        /// <returns>
        /// 要传递到源对象的值。
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}