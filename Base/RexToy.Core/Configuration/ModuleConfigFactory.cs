using System;
using System.Collections.Generic;

namespace RexToy.Configuration
{
    public static class ModuleConfigFactory
    {
        public static T Create<T>(IConfig cfg) where T : ModuleConfig, new()
        {
            T mc = new T();
            mc.Initialize(cfg);
            return mc;
        }

        public static T Create<T>() where T : ModuleConfig, new()
        {
            return new T();
        }
    }
}
