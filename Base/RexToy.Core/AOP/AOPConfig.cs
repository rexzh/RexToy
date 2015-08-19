using System;
using System.Collections.Generic;

using RexToy.Configuration;

namespace RexToy.AOP
{
    public static class AOPConfig
    {
        private static IAOPConfig _aop_cfg;
        public static IAOPConfig AOPConfiguration
        {
            get
            {
                if (_aop_cfg == null)
                {
                    _aop_cfg = ModuleConfigFactory.Create<XmlAOPConfig>();
                }
                return _aop_cfg;
            }
        }

        public static void Initialize(IAOPConfig aop_cfg)
        {
            _aop_cfg = aop_cfg;
        }
    }
}
