using NUnit.Framework;

namespace ZxdFramework
{
    /// <summary>
    /// 测试加解密类型
    /// </summary>
    public class DESEncryptTest : TestSupport
    {
        [Test]
        public void TestEncrypt()
        {
            string data = "Zong xudong";


            var enc = DESEncrypt.Instance.Encrypt(data);
            Log.Debug(enc);
            var enc2 = DESEncrypt.Instance.Encrypt(data);
            Log.Debug(enc2);


            var res = DESEncrypt.Instance.Decryptor(enc);
            Log.Debug(res);
            Log.Debug(data.Equals(res).ToString());

            

            Assert.IsTrue(enc == enc2);
            
        }

        [Test]
        public void TestEncryptPass()
        {
            string data = "Zong xudong";

            var end = PasswordEncrypt.Instance.Encrypt(data);
            Log.Debug(end);
        }
    }
}