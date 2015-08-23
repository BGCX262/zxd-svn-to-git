using System;
using System.Linq;
using System.Web.Mvc;

namespace ZxdFramework.Mvc.Engines.Razor
{
    /// <summary>
    /// 更具视图的后缀名 .db, 从数据库中提取视图内容
    /// </summary>
    public class DbRazorEngine : BuildManagerViewEngine
    {

        public DbRazorEngine() : this(null)
        {
            
        }

        public DbRazorEngine(IViewPageActivator viewPageActivator)
            : base(viewPageActivator)
        {
            FileExtensions = new[] {
                "dbcs",
                "dbvb",
            };
        }


        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            return new RazorView(controllerContext, partialPath,
                                 layoutPath: null, runViewStartPages: false, viewStartFileExtensions: FileExtensions, viewPageActivator: ViewPageActivator);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            var view = new RazorView(controllerContext, viewPath,
                                     layoutPath: masterPath, runViewStartPages: true, viewStartFileExtensions: FileExtensions, viewPageActivator: ViewPageActivator);
            return view;
        }



        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }
            if (String.IsNullOrEmpty(viewName))
            {
                throw new ArgumentException("没有当前的视图");
            }

            if (!viewName.EndsWith(".db"))
            {
                return new ViewEngineResult(new string[]{});
            }

            return new ViewEngineResult(CreateView(controllerContext, viewName, masterName), this);
        }

        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            return base.FindPartialView(controllerContext, partialViewName, useCache);
        }
    }
}