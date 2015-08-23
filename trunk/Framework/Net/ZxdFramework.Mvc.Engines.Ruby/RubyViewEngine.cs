using System;
using System.IO;
using System.Web.Mvc;
using Microsoft.Scripting.Hosting;

namespace ZxdFramework.Mvc.Engines.Ruby {

    /// <summary>
    /// Ruby 视图引擎
    /// </summary>
    public class RubyViewEngine : VirtualPathProviderViewEngine {
        /// <summary>
        /// Initializes a new instance of the <see cref="RubyViewEngine"/> class.
        /// </summary>
        public RubyViewEngine() {
            PartialViewLocationFormats = new string[] {
                "~/Views/{1}/_{0}.rhtml", 
                "~/Views/Shared/_{0}.rhtml"
            };

            ViewLocationFormats = new string[] { 
                "~/Views/{1}/{0}.rhtml", 
                "~/Views/Shared/{0}.rhtml", 
            };

            MasterLocationFormats = new string[] { 
                "~/Views/{1}/{0}.rhtml", 
                "~/Views/Shared/{0}.rhtml"
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RubyViewEngine"/> class.
        /// </summary>
        /// <param name="scriptRuntime">初始化脚本环境</param>
        /// <param name="rubyViewSource">脚本视图资源获取对象</param>
        public RubyViewEngine(ScriptRuntime scriptRuntime, IRubyViewSource rubyViewSource) : this() {
            RubyViewSource = rubyViewSource;
            ScriptRuntime = scriptRuntime;
        }

        /// <summary>
        /// 获取视图脚本资源对象
        /// </summary>
        protected IRubyViewSource RubyViewSource {
            get;
            private set;
        }

        /// <summary>
        /// 获取脚本环境
        /// </summary>
        protected ScriptRuntime ScriptRuntime {
            get;
            private set;
        }

        /// <summary>
        /// 获取视图内容
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        string GetContents(ControllerContext controllerContext, string path) {
            if (RubyViewSource != null) {
                string contents = RubyViewSource.GetViewContents(controllerContext, path);
                if (!String.IsNullOrEmpty(contents)) {
                    return contents;
                }
            }

            using (var stream = VirtualPathProvider.GetFile(path).Open())
            using (var reader = new StreamReader(stream)) {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// Returns a value that indicates whether the file is in the specified path by using the specified controller context.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="virtualPath">The virtual path.</param>
        /// <returns>
        /// true if the file is in the specified path; otherwise, false.
        /// </returns>
        protected override bool FileExists(ControllerContext controllerContext, string virtualPath) {
            if (RubyViewSource != null && RubyViewSource.FileExists(controllerContext, virtualPath)) {
                return true;
            }
            return base.FileExists(controllerContext, virtualPath);
        }

        RubyView GetRubyMasterView(ControllerContext controllerContext, string virtualPath) {
            if (String.IsNullOrEmpty(virtualPath))
                return null;

            string viewContents = GetContents(controllerContext, virtualPath);
            return new RubyView(ScriptRuntime, viewContents, null);
        }

        /// <summary>
        /// Gets the view.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="virtualPath">The virtual path.</param>
        /// <param name="masterView">The master view.</param>
        /// <returns></returns>
        IView GetView(ControllerContext controllerContext, string virtualPath, RubyView masterView) {
            if (String.IsNullOrEmpty(virtualPath))
                return null;

            string viewContents = GetContents(controllerContext, virtualPath);
            if (String.Equals(controllerContext.HttpContext.Request.QueryString["editView"], "true", StringComparison.OrdinalIgnoreCase)) {
                return new WebFormView(controllerContext, "~/Views/Shared/ViewCreate.aspx");
            }
            return new RubyView(ScriptRuntime, viewContents, masterView);
        }

        /// <summary>
        /// Creates the specified partial view by using the specified controller context.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="partialPath">The partial path for the new partial view.</param>
        /// <returns>
        /// A reference to the partial view.
        /// </returns>
        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath) {
            return GetView(controllerContext, partialPath, null);
        }

        /// <summary>
        /// Creates the specified view by using the controller context, path of the view, and path of the master view.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="viewPath">The path of the view.</param>
        /// <param name="masterPath">The path of the master view.</param>
        /// <returns>
        /// A reference to the view.
        /// </returns>
        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath) {
            RubyView masterView = GetRubyMasterView(controllerContext, masterPath);
            return GetView(controllerContext, viewPath, masterView);
        }
    }
}
