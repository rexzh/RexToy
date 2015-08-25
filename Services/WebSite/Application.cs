using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

using RexToy.Configuration;
using RexToy.Json;
using RexToy.WebService;

[assembly: Startup(typeof(WebSite.Application))]

namespace WebSite
{
    
    class Application : IStartup
    {
        public void Startup(HttpApplication app)
        {
            AppConfig.Load(ConfigFactory.CreateXmlConfig());
            ExtendConverter.Instance().Register(typeof(ListConverter));
            ExtendConverter.Instance().Register(typeof(DictionaryConverter));
            ExtendConverter.Instance().Register(typeof(JavascriptTimeConverter));
            ExtendConverter.Instance().Register(typeof(EnumConverter));
        }
    }
}
