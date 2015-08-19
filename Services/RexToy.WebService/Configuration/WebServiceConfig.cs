using System;
using System.Collections.Generic;

using RexToy.Configuration;

namespace RexToy.WebService.Configuration
{
    class WebServiceConfig
    {
        private static IWebServiceConfig _cfg;
        public static IWebServiceConfig WebServiceConfiguration
        {
            get
            {
                if (_cfg == null)
                {
                    _cfg = ModuleConfigFactory.Create<XmlWebServiceConfig>();
                }
                return _cfg;
            }
        }
    }
}
