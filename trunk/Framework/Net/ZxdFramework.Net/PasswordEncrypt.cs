using System;
using System.Security.Cryptography;
using System.Text;

namespace ZxdFramework
{
    /// <summary>
    /// 密码加密
    /// </summary>
    public class PasswordEncrypt : IEncrypt
    {
        private Encoding _encoding = Encoding.UTF8;
        private PasswordEncrypt()
        {
            
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
        /// 获取当前实例
        /// </summary>
        public static IEncrypt Instance { get { return new PasswordEncrypt(); } }


        /// <summary>
        /// 获取密钥
        /// </summary>
        /// <returns></returns>
        public static string CreateSalt()
        {
            byte[] data = new byte[8];
            new RNGCryptoServiceProvider().GetBytes(data);
            return Convert.ToBase64String(data);
        }



        /// <summary>
        /// Encrypts the specified STR.
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <param name="salt">The salt.</param>
        /// <returns></returns>
        public string Encrypt(string str, string salt)
        {
            if (salt == null || salt == "")
            {
                return str;
            }
            byte[] bytes = Encoding.Unicode.GetBytes(salt.ToLower().Trim() + str.Trim());
            
            return EncodingMode.GetString(((HashAlgorithm)CryptoConfig.CreateFromName("SHA1")).ComputeHash(bytes));

        }

        /// <summary>
        /// 加密一段字符串
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <returns></returns>
        public string Encrypt(string str)
        {
            return Encrypt(str, CreateSalt());
        }

        public string Decryptor(string str, string key)
        {
            throw new NotImplementedException();
        }

        public string Decryptor(string str)
        {
            throw new NotImplementedException();
        }
    }
}