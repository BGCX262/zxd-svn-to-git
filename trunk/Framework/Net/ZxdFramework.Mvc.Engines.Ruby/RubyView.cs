using IronRubyMvc;
using ZxdFramework.Mvc.Engines.Ruby.Helpers;
using System;
using System.IO;
using System.Text;
using System.Web.Mvc;
using Microsoft.Scripting.Hosting;

namespace ZxdFramework.Mvc.Engines.Ruby {
    public class RubyView : IView {
        RubyTemplate _template;

        public RubyView(ScriptRuntime scriptRuntime, string viewContents, RubyView master) {
            ScriptRuntime = scriptRuntime;
            Contents = viewContents;
            MasterView = master;
        }

        public RubyTemplate Template {
            get {
                if (_template == null)
                    _template = new RubyTemplate(Contents);
                return _template;
            }
        }

        protected ScriptRuntime ScriptRuntime {
            get;
            private set;
        }

        public string Contents {
            get;
            private set;
        }

        public RubyView MasterView {
            get;
            private set;
        }

        public void Render(ViewContext context, TextWriter writer) {
            var rubyEngine = IronRuby.Ruby.GetEngine(ScriptRuntime);

            //var rubyContext = IronRuby.Ruby.GetExecutionContext(ScriptRuntime);

            ScriptScope scope = ScriptRuntime.CreateScope();
            scope.SetVariable("view_data", context.ViewData);
            scope.SetVariable("model", context.ViewData.Model);
            scope.SetVariable("context", context);
            scope.SetVariable("response", context.HttpContext.Response);
            scope.SetVariable("url", new RubyUrlHelper(context));
            var viewDataContainer = new Container(context.ViewData);
            scope.SetVariable("html", new RubyHtmlHelper(context, viewDataContainer));
            scope.SetVariable("ajax", new AjaxHelper(context, viewDataContainer));

            Template.AddRequire("System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
            Template.AddRequire("System.Web.Abstractions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
            Template.AddRequire("System.Web.Routing, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");
            Template.AddRequire("System.Web.Mvc, Version=1.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35");

            StringBuilder script = new StringBuilder();
            Template.ToScript("render_page", script);

            if (MasterView != null)
                MasterView.Template.ToScript("render_layout", script);
            else
                script.AppendLine("def render_layout; yield; end");

            script.AppendLine(@"
def model.method_missing(methodname)
    methodname = methodname.to_s
# self.methods.join(', ')
    self.GetType().GetProperty(methodname).GetValue(self, nil)
end

def view_data.method_missing(methodname)
    get_Item(methodname.to_s)
end");
            script.AppendLine("render_layout { |content| render_page }");

            try {
                ScriptSource source = rubyEngine.CreateScriptSourceFromString(script.ToString());
                source.Execute(scope);
            }
            catch (Exception e) {
                writer.Write(script + "<br />");
                writer.Write(e.ToString());
            }
        }

        internal class Container : IViewDataContainer {
            internal Container(ViewDataDictionary viewData) {
                ViewData = viewData;
            }

            public ViewDataDictionary ViewData { get; set; }
        }
    }
}