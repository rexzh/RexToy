using System;
using System.Collections.Generic;

using RexToy.DesignPattern;

namespace RexToy.Configuration
{
    public class AppConfig : Singleton<AppConfig>
    {
        private static object _locker = new object();

        private IConfig _config;
        protected IConfig _Config
        {
            get
            {
                lock (_locker)
                {
                    if (_config == null)
                        ExceptionHelper.ThrowConfigNotRegistered();
                    return _config;
                }
            }
        }

        private AppConfig()
        {
        }

        public static void Load(IConfig config)
        {
            config.ThrowIfNullArgument(nameof(config));
            
            if (Instance()._config != null)
                ExceptionHelper.ThrowDestroyFirst();
            lock (_locker)
            {
                Instance()._config = config;
            }
        }

        public static bool Loaded
        {
            get
            {
                lock (_locker)
                {
                    return Instance()._config != null;
                }
            }
        }

        public static IConfig Config
        {
            get { return Instance()._Config; }
        }
    }
}