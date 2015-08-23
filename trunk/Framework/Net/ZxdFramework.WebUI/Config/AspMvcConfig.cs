using ZxdFramework.Mvc.Config;

namespace ZxdFramework.WebUI.Config
{
    /// <summary>
    /// 当前应用的配置
    /// </summary>
    public class AspMvcConfig : MvcConfig
    {
        public const string AspMvcRootResource = @"assembly://ZxdFramework.WebUI/ZxdFramework.WebUI/Config.Application-Context.config";
        public const string AspControllerResource = @"assembly://ZxdFramework.WebUI/ZxdFramework.WebUI/Config.Controller-Context.config";

        public AspMvcConfig()
            : base(AspMvcRootResource, AspControllerResource)
        {
            
        }
    }
}