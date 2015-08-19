using System;
using System.Collections.Generic;

using RexToy.Configuration;

namespace RexToy.IoC
{
    public static class KernalConfig
    {
        private static IKernalConfig _kernal_cfg;
        public static IKernalConfig KernalConfiguration
        {
            get
            {
                if (_kernal_cfg == null)
                {
                    _kernal_cfg = ModuleConfigFactory.Create<XmlKernalConfig>();
                }
                return _kernal_cfg;
            }
        }

        public static void Initialize(IKernalConfig kernal_cfg)
        {
            _kernal_cfg = kernal_cfg;
        }
    }
}
