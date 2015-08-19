using System;

namespace RexToy.Logging
{
    public interface ILoggerFactory
    {
        ILog CreateLogger(string name, ILogConfig logConfig);
    }
}
