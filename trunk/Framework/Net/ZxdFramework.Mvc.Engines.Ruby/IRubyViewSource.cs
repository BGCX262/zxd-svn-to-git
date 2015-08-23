using System.Web.Mvc;

namespace ZxdFramework.Mvc.Engines.Ruby {

    public interface IRubyViewSource {
        string GetViewContents(ControllerContext controllerContext, string path);
        bool FileExists(ControllerContext controllerContext, string path);
    }
}
