namespace ZxdFramework.DataContract
{
    /// <summary>
    /// 项目中使用的全局设置属性
    /// </summary>
    public static class Globals
    {
        /// <summary>
        /// 设置获取当前的项目是否处于调试阶段. 这样能够避免调试中不必要的验证等.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is debug; otherwise, <c>false</c>.
        /// </value>
        public static bool IsDebug { set; get; }
    }
}