using System;
using Microsoft.Practices.Prism.Modularity;
using ZxdFramework.DataContract.System;
using InitializationMode = Microsoft.Practices.Prism.Modularity.InitializationMode;

namespace ZxdFramework.Silverlight.IM.UI
{
    /// <summary>
    /// 即时通讯模块人口
    /// </summary>
    [ZxdModuleExport("im", typeof(IMModule),
        Author = "宗旭东",
        Category = "Customer",
        InitializationMode = InitializationMode.WhenAvailable,
        Description = "用户即时通讯模块")]
    public class IMModule : IModule
    {
        public void Initialize()
        {
            
        }
    }
}