using System;
using System.Collections.Generic;

using RexToy.Configuration;

namespace RexToy.ORM.Configuration
{
    public class ORMConfig
    {
        private static IORMConfig _orm_cfg;
        public static IORMConfig ORMConfiguration
        {
            get
            {
                if (_orm_cfg == null)
                {
                    _orm_cfg = ModuleConfigFactory.Create<XmlORMConfig>();
                }
                return _orm_cfg;
            }
        }

        public static void Initialize(IORMConfig orm_cfg)
        {
            _orm_cfg = orm_cfg;
        }
    }
}
