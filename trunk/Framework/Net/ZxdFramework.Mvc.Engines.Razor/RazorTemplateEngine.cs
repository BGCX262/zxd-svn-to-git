using System.Collections.Generic;
using System.Web.Mvc;

namespace ZxdFramework.Mvc.Engines.Razor
{
    public class RazorTemplateEngine
    {

        public string Name
        {
            get { return "Razor"; }
        }

        public string GetFileExtensionForLayout()
        {
            return ".cshtml";
        }

        public string GetFileExtensionForView()
        {
            return ".cshtml";
        }

        public IEnumerable<string> FileExtensions
        {
            get { return new string[] { ".cshtml" }; }
        }

        public IViewEngine GetViewEngine()
        {
            return new RazorViewEngine();
        }

        public IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            return new RazorView(controllerContext, viewPath, masterPath, false, null);
        }


        public int Order
        {
            get { return 1; }
        }
    }
}