using System;
using System.Security.Cryptography;
using System.Text;

namespace ZxdFramework
{
    /// <summary>
    /// �������
    /// </summary>
    public class PasswordEncrypt : IEncrypt
    {
        private Encoding _encoding = Encoding.UTF8;
        private PasswordEncrypt()
        {
            
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
        /// ��ȡ��ǰʵ��
        /// </summary>
        public static IEncrypt Instance { get { return new PasswordEncrypt(); } }


        /// <summary>
        /// ��ȡ��Կ
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
        /// ����һ���ַ���
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