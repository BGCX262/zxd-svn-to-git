namespace ZxdFramework
{
    /// <summary>
    /// ͨ�õļ�������ܵĽӿ�
    /// </summary>
    public interface IEncrypt
    {

        /// <summary>
        /// ʹ����ֵ�����ַ���
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        string Encrypt(string str, string key);
        /// <summary>
        /// ����һ���ַ���
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <returns></returns>
        string Encrypt(string str);

        /// <summary>
        /// ʹ����ֵ�����ַ���
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        string Decryptor(string str, string key);

        /// <summary>
        /// ����һ���ַ���
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <returns></returns>
        string Decryptor(string str);
    }
}