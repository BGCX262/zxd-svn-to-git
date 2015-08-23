using System.IO;
using System.Security.Cryptography;
using System.Text;
using ZxdFramework.Config;

namespace ZxdFramework
{
    /// <summary>
    /// ʹ�� DES ʵ���ַ��ļ�������ܹ���
    /// </summary>
    public class DESEncrypt : IEncrypt
    {
        private string _iv = "12345678";
        private string _key = "12345678";
        private DES _des = new DESCryptoServiceProvider();
        private Encoding _encoding = Encoding.Unicode;


        /// <summary>
        /// ��ȡʹ�õ�ǰ���ܵ�ʵ��
        /// </summary>
        /// <value>The instance.</value>
        public static IEncrypt Instance
        {
            get { return BaseConfig.RootContext.GetTypedObject<IEncrypt>("encrypt"); }
        }


        /// <summary>
        /// ���û�ȡ���ܼ�
        /// </summary>
        /// <value>The encrypty key.</value>
        public string EncryptyKey
        {
            set { _key = value; }
            get { return _key; }
        }

        /// <summary>
        /// ��ȡ���ñ����ʽ
        /// </summary>
        /// <value>The encoding mode.</value>
        public Encoding EncodingMode
        {
            set { _encoding = value; }
            get { return _encoding; }
        }

        /// <summary>
        /// ʹ��ϵͳ����ֵ����������ַ���
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <returns></returns>
        public string Encrypt(string str)
        {
            return Encrypt(str, EncryptyKey);
        }

        /// <summary>
        /// ʹ����ֵ�����ַ���
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
        /// ����һ���ַ���
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <returns></returns>
        public string Decryptor(string str)
        {
            return Decryptor(str, EncryptyKey);
        }

        /// <summary>
        /// ʹ����ֵ�����ַ���
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