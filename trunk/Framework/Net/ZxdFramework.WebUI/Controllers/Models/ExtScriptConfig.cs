using ZxdFramework.DataContract;

namespace ZxdFramework.WebUI.Controllers.Models
{
    /// <summary>
    /// Ext �ű���������
    /// </summary>
    public class ExtScriptConfig
    {
        /// <summary>
        /// ��ȡ��������Ŀ¼��ַ
        /// </summary>
        /// <value>
        /// The root path.
        /// </value>
        public string RootPath { set; get; }

        public IUser CurrentUser { set; get; }
    }
}