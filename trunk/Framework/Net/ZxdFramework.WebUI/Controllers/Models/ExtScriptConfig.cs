using ZxdFramework.DataContract;

namespace ZxdFramework.WebUI.Controllers.Models
{
    /// <summary>
    /// Ext 脚本配置类型
    /// </summary>
    public class ExtScriptConfig
    {
        /// <summary>
        /// 获取服务器根目录地址
        /// </summary>
        /// <value>
        /// The root path.
        /// </value>
        public string RootPath { set; get; }

        public IUser CurrentUser { set; get; }
    }
}