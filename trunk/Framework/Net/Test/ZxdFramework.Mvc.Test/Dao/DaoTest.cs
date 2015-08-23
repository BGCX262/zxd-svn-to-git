using System;
using NUnit.Framework;
using Spring.Context;
using ZxdFramework.Dao;
using ZxdFramework.Json;
using ZxdFramework.Mvc.Config;
using ZxdFramework.Mvc.Models;
using System.Collections.Generic;
using ZxdFramework.Model;

namespace ZxdFramework.Mvc.Dao
{
    public class DaoTest : TestSupport
    {

    }


    public class TestEvent : IApplicationEventListener
    {
        public void HandleApplicationEvent(object sender, ApplicationEventArgs e)
        {
            
        }
    }

    public class Test2Event : IApplicationContextAware
    {
        public IApplicationContext ApplicationContext { set; get; }
    }
}