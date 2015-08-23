using System.IO;
using System.Security.Cryptography;
using System.Text;
using ZxdFramework.Config;

namespace ZxdFramework
{
    /// <summary>
    /// 使用 DES 实现字符的加密与解密过程
    /// </summary>
    public class DESEncrypt : IEncrypt
    {
        private string _iv = "12345678";
        private string _key = "12345678";
        private DES _des = new DESCryptoServiceProvider();
        private Encoding _encoding = Encoding.Unicode;


        /// <summary>
        /// 获取使用当前加密的实例
        /// </summary>
        /// <value>The instance.</value>
        public static IEncrypt Instance
        {
            get { return BaseConfig.RootContext.GetTypedObject<IEncrypt>("encrypt"); }
        }


        /// <summary>
        /// 设置获取加密键
        /// </summary>
        /// <value>The encrypty key.</value>
        public string EncryptyKey
        {
            set { _key = value; }
            get { return _key; }
        }

        /// <summary>
        /// 获取设置编码格式
        /// </summary>
        /// <value>The encoding mode.</value>
        public Encoding EncodingMode
        {
            set { _encoding = value; }
            get { return _encoding; }
        }

        /// <summary>
        /// 使用系统的盐值加密输入的字符串
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <returns></returns>
        public string Encrypt(string str)
        {
            return Encrypt(str, EncryptyKey);
        }

        /// <summary>
        /// 使用盐值加密字符串
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public string Encrypt(string str, string key)
        {
            if (string.IsNullOrEmpty(str)) return "";


            var ivb = Encoding.ASCII.GetBytes(_iv);
            var keyb = Encoding.ASCII.GetBytes(key);
            var tob = EncodingMode.GetBytes(str);
            byte[] encrypted;

            var encryptor = _des.CreateEncryptor(keyb, ivb);
            var msEncrypt = new MemoryStream();
            var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
            csEncrypt.Write(tob, 0, tob.Length);
            csEncrypt.FlushFinalBlock();
            encrypted = msEncrypt.ToArray();
            csEncrypt.Close();
            msEncrypt.Close();
            return EncodingMode.GetString(encrypted);
        }


        /// <summary>
        /// 解密一段字符串
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <returns></returns>
        public string Decryptor(string str)
        {
            return Decryptor(str, EncryptyKey);
        }

        /// <summary>
        /// 使用盐值解密字符串
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public string Decryptor(string str, string key)
        {
            var ivb = Encoding.ASCII.GetBytes(_iv);
            var keyb = Encoding.ASCII.GetBytes(key);
            var tob = EncodingMode.GetBytes(str);
            byte[] encrypted;
            var encryptor = _des.CreateDecryptor(keyb, ivb);
            var msEncrypt = new MemoryStream();
            var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
            csEncrypt.Write(tob, 0, tob.Length);
            csEncrypt.FlushFinalBlock();
            encrypted = msEncrypt.ToArray();
            csEncrypt.Close();
            msEncrypt.Close();
            return EncodingMode.GetString(encrypted);
        }
    }
}