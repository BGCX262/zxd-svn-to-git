namespace ZxdFramework
{
    /// <summary>
    /// 通用的加密与解密的接口
    /// </summary>
    public interface IEncrypt
    {

        /// <summary>
        /// 使用盐值加密字符串
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        string Encrypt(string str, string key);
        /// <summary>
        /// 加密一段字符串
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <returns></returns>
        string Encrypt(string str);

        /// <summary>
        /// 使用盐值解密字符串
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        string Decryptor(string str, string key);

        /// <summary>
        /// 解密一段字符串
        /// </summary>
        /// <param name="str">The STR.</param>
        /// <returns></returns>
        string Decryptor(string str);
    }
}