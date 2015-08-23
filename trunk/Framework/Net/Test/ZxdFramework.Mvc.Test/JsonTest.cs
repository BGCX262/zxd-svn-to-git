using System;
using System.Dynamic;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Common.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using ZxdFramework.DataContract.System;

namespace ZxdFramework.Mvc
{

    public class JsonTest : TestSupport
    {
        [Test]
        public void TestString()
        {

            var m = new ModuleFile()
                        {
                            FileName = "asdfasdf"
                        };

            var list = new HashSet<ModuleInfo>();
            list.Add(new ModuleInfo());

            m.ModuleList = list;

            var data = Json.JsonConverter.Serialize(m);
            Log.Debug(data);
        }
    }
}
