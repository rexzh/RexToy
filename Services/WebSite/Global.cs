using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

using RexToy.Configuration;
using RexToy.Json;

namespace WebSite
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            AppConfig.Load(ConfigFactory.CreateXmlConfig());
            ExtendConverter.Instance().Register(typeof(ListConverter));
            ExtendConverter.Instance().Register(typeof(DictionaryConverter));
            ExtendConverter.Instance().Register(typeof(JavascriptTimeConverter));
            ExtendConverter.Instance().Register(typeof(EnumConverter));
        }
    }
}
