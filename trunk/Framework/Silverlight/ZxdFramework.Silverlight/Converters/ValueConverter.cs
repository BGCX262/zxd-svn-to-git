using System;
using System.Globalization;
using System.Windows.Data;

namespace ZxdFramework.Converters
{
    /// <summary>
    /// ֵת���ĳ�����. ��Ժܶ��ֵֻ��Ҫ�����õ�Xaml�ļ���ת���ɶ�Ӧ�Ķ���ֵ. ������Ҫ����ת��. ���Լ̳����������
    /// </summary>
    public abstract class ValueConverter : IValueConverter
    {
        /// <summary>
        /// �ڽ�Դ���ݴ��ݵ�Ŀ������ UI ����ʾ֮ǰ����Դ���ݽ����޸ġ�
        /// </summary>
        /// <param name="value">�����ݵ�Ŀ���Դ���ݡ�</param>
        /// <param name="targetType">Ŀ��������������Ҫ�����ݵ� <see cref="T:System.Type"/>��</param>
        /// <param name="parameter">Ҫ��ת�����߼���ʹ�õĿ�ѡ������</param>
        /// <param name="culture">ת���������ԡ�</param>
        /// <returns>
        /// Ҫ���ݵ�Ŀ�����������Ե�ֵ��
        /// </returns>
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// �ڽ�Ŀ�����ݴ��ݵ�Դ����֮ǰ����Ŀ�����ݽ����޸ġ��˷������� <see cref="F:System.Windows.Data.BindingMode.TwoWay"/> ���н��е��á�
        /// </summary>
        /// <param name="value">�����ݵ�Դ��Ŀ�����ݡ�</param>
        /// <param name="targetType">Դ������Ҫ�����ݵ� <see cref="T:System.Type"/>��</param>
        /// <param name="parameter">Ҫ��ת�����߼���ʹ�õĿ�ѡ������</param>
        /// <param name="culture">ת���������ԡ�</param>
        /// <returns>
        /// Ҫ���ݵ�Դ�����ֵ��
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}