using ZxdFramework.Dao.System;
using ZxdFramework.DataContract.System;
using ZxdFramework.Mvc.Controllers;


namespace ZxdFramework.WebUI.Areas.Service.Controllers
{
    public class SystemController : ZxdFrameworkController
    {

        public SystemController(IModuleFileRepository moduleFileRepository)
        {
            ModuleFileRepository = moduleFileRepository;
        }

        public IModuleFileRepository ModuleFileRepository { protected set; get; }

        /// <summary>
        /// Gets the module info.
        /// </summary>
        /// <returns></returns>
        public object GetModuleInfo(int paid)
        {
            var s = this.User;
            return new object();
            //return new EmptyResult();
        }

        /// <summary>
        /// 上传更新模块
        /// </summary>
        /// <param name="modulefile">The modulefile.</param>
        /// <returns></returns>
        public bool UploadModuleFile(ModuleFile modulefile)
        {
            //modulefile.Save();
            ModuleFileRepository.Add(modulefile);
            return true;
        }

        /// <summary>
        /// 下载 Xaml 的模块配置信息
        /// </summary>
        /// <returns></returns>
        public string DownXamlModuleInfo()
        {
            //return "ModuleFileRepository".GetInstance<IModuleFileRepository>().GetModuleXaml();

            return ModuleFileRepository.GetModuleXaml();
        }
    }
}