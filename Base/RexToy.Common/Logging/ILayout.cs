using System;

namespace RexToy.Logging
{
    public interface ILayout
    {
        string Format(string name, LogLevel level, string msg);
    }
}
