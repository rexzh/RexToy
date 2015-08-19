using System;
using System.Collections.Generic;

namespace RexToy.Logging
{
    static class ExceptionHelper
    {
        public static void ThrowConfigItemNull(string configItem)
        {
            throw new LogConfigException(string.Format("[{0}] is null or invalid.", configItem));
        }

        public static void ThrowConfigFileNotExist(string path)
        {
            throw new LogConfigException(string.Format("Log config file [{0}] not exist.", path));
        }
    }
}
