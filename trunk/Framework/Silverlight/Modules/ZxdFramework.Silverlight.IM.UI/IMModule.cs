using System;
using Microsoft.Practices.Prism.Modularity;
using ZxdFramework.DataContract.System;
using InitializationMode = Microsoft.Practices.Prism.Modularity.InitializationMode;

namespace ZxdFramework.Silverlight.IM.UI
{
    /// <summary>
    /// ��ʱͨѶģ���˿�
    /// </summary>
    [ZxdModuleExport("im", typeof(IMModule),
        Author = "����",
        Category = "Customer",
        InitializationMode = InitializationMode.WhenAvailable,
        Description = "�û���ʱͨѶģ��")]
    public class IMModule : IModule
    {
        public void Initialize()
        {
            
        }
    }
}