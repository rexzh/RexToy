using System;

namespace RexToy.Logging
{
    public interface ILogConfig
    {
        LogLevel GetLogLevel(string name);
        ILayout Layout { get; }
        IWriter Writer { get; }
    }
}
