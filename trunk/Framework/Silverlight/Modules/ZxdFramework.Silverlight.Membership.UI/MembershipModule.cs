using System;
using Microsoft.Practices.Prism.Modularity;
using ZxdFramework.DataContract.System;
using InitializationMode = Microsoft.Practices.Prism.Modularity.InitializationMode;

namespace ZxdFramework.Membership.UI
{

    [ZxdModuleExport("MembershipModule", typeof(MembershipModule),
        Author = "����",
        Category = "System",
        InitializationMode = InitializationMode.WhenAvailable,
        Description = "Ȩ�޽���ģ��. �ṩ���û���¼, Ȩ�����õȽ��洦��",
        DependsOnModuleNames = new[] { "shell" })]
    public class MembershipModule : IModule
    {

        public void Initialize()
        {
            
        }
    }
}