using System;

namespace RexToy.Configuration
{
    static class ExceptionHelper
    {
        public static void ThrowConfigNotRegistered()
        {
            throw new ConfigException("Config is not registered.");
        }

        public static void ThrowDestroyFirst()
        {
            throw new ConfigException("Config file is already load, destroy it first.");
        }

        public static ConfigException CreateWrapException(Exception inner, string section, string key = null)
        {
            string msg = string.Format("Error when access [section, key] = ['{0}', '{1}']", section, key);
            return inner.CreateWrapException<ConfigException>(msg);
        }
    }
}