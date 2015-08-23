using System;
using System.Globalization;
using System.Windows.Media.Imaging;
using ZxdFramework.Converters;

namespace ZxdFramework.DataContract.Converters
{
    /// <summary>
    /// ת��һ���û�����һ����������ȡ�û�ͼ����Դ
    /// </summary>
    public class AccountPhotoConverter : ValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var temp = value as IUser;
            if (temp != null)
            {
                return new BitmapImage(new Uri(Globals.ServerRoot, "Account.json/GetPhoto/" + temp.Id));
            }

            return null;
        }
    }
}