using System;
using System.Collections.Generic;

namespace RexToy.Logging
{
    public enum LogLevel
    {
        All = 0,//Note:reserved level..only for compare
        Verbose = 5,
        Debug = 10,
        Info = 20,
        Warning = 30,
        Error = 40,
        Fatal = 50,
        None = 9999,//Note:reserved level..only for compare
    }
}
