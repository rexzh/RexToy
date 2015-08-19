using System;
using System.Collections.Generic;

namespace RexToy.Configuration
{
    public abstract class ModuleConfig
    {
        private IConfig _cfg;
        public void Initialize(IConfig cfg)
        {
            cfg.ThrowIfNullArgument(nameof(cfg));
            _cfg = cfg;
        }

        protected IConfig GlobalConfig
        {
            get
            {
                return _cfg ?? AppConfig.Config;
            }
        }
    }
}
