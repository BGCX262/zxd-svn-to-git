namespace ZxdFramework.DataContract
{
    /// <summary>
    /// 定义支持当前的对象能够获取视图
    /// </summary>
    public interface IViewSupport
    {
        /// <summary>
        /// 获取视图对象
        /// </summary>
        object View { get; }
    }
}