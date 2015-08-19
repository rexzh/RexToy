using System;
using System.Collections.Generic;

using RexToy.DesignPattern;

namespace RexToy.Logging
{
    class LoggerFactory : ILoggerFactory
    {
        public ILog CreateLogger(string name, ILogConfig logConfig)
        {
            if (logConfig == null)
                return new Logger(name);

            return new Logger(name, logConfig);
        }
    }
}
