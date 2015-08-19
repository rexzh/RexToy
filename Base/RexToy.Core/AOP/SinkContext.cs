using System;
using System.Collections.Generic;

using RexToy.Configuration;

namespace RexToy.AOP
{
    static class SinkContext
    {
        private static ISinkFactory _instance;
        static SinkContext()
        {
            IAOPConfig cfg = AOPConfig.AOPConfiguration;
            Type factory = cfg.LoadSinkFactory();
            if (factory == null)
                _instance = new DefaultSinkFactory();
            else
                _instance = (ISinkFactory)Activator.CreateInstance(factory);
        }

        public static ISinkFactory GetFactory()
        {
            return _instance;
        }
    }
}
