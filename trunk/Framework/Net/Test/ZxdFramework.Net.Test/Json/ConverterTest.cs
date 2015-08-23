using System;
using System.Globalization;
using Common.Logging;
using Newtonsoft.Json;
using NUnit.Framework;
using ZxdFramework.Authorization;

namespace ZxdFramework.Json
{
    public class ConverterTest
    {
        private ILog _log = LogManager.GetCurrentClassLogger();
        [Test]
        public void TestCov()
        {
            var dt = DateTime.Now;
            
            _log.Debug(dt);
            var str = JsonConverter.Serialize(dt);
            _log.Debug(str);
            
             var d =  JsonConverter.DeserializeObject<DateTime>(str);
             _log.Debug((d - dt).Ticks);
            _log.Debug(d);
            _log.Debug((new TimeSpan(8, 0, 0)).Ticks + 621355968000000000L);

            //TimeZone.CurrentTimeZone = TimeZoneInfo.CreateCustomTimeZone("", new TimeSpan(8, 0, 0), "ss", "", "");
            //TimeZone.CurrentTimeZone.
            //CultureInfo.CurrentCulture.Calendar.AddHours(DateTime.)
            _log.Debug(TimeZoneInfo.Local);
        }

        [Test]
        public void TestU()
        {
            var u = new User();
            u.LoginTime = DateTime.Now;
            var str = JsonConverter.Serialize(u);

            _log.Debug(str);
        }
    }
}