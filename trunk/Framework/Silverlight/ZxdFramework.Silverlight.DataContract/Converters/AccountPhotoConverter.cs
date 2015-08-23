using System;
using System.Globalization;
using System.Windows.Media.Imaging;
using ZxdFramework.Converters;

namespace ZxdFramework.DataContract.Converters
{
    /// <summary>
    /// 转换一个用户对象到一个服务器获取用户图像资源
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