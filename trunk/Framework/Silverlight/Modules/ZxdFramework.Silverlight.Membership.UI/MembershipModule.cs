using System;
using Microsoft.Practices.Prism.Modularity;
using ZxdFramework.DataContract.System;
using InitializationMode = Microsoft.Practices.Prism.Modularity.InitializationMode;

namespace ZxdFramework.Membership.UI
{

    [ZxdModuleExport("MembershipModule", typeof(MembershipModule),
        Author = "宗旭东",
        Category = "System",
        InitializationMode = InitializationMode.WhenAvailable,
        Description = "权限界面模块. 提供了用户登录, 权限配置等界面处理",
        DependsOnModuleNames = new[] { "shell" })]
    public class MembershipModule : IModule
    {

        public void Initialize()
        {
            
        }
    }
}