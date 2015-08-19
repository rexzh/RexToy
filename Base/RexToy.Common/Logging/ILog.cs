using System;

namespace RexToy.Logging
{
    public interface ILog
    {
        void Config(ILogConfig cfg);
        LogLevel LogLevel { get; }
        string Name { get; }

        void Verbose(string msg);
        void Verbose(string msg, params object[] args);
        void VerboseIf(bool condition, string msg);
        void VerboseIf(bool condition, string msg, params object[] args);
        void Debug(string msg);
        void Debug(string msg, params object[] args);
        void DebugIf(bool condition, string msg);
        void DebugIf(bool condition, string msg, params object[] args);
        void Error(string msg);
        void Error(string msg, params object[] args);
        void ErrorIf(bool condition, string msg);
        void ErrorIf(bool condition, string msg, params object[] args);
        void Fatal(string msg);
        void Fatal(string msg, params object[] args);
        void FatalIf(bool condition, string msg);
        void FatalIf(bool condition, string msg, params object[] args);
        void Info(string msg);
        void Info(string msg, params object[] args);
        void InfoIf(bool condition, string msg);
        void InfoIf(bool condition, string msg, params object[] args);
        void Warning(string msg);
        void Warning(string msg, params object[] args);
        void WarningIf(bool condition, string msg);
        void WarningIf(bool condition, string msg, params object[] args);

        void Log(LogLevel level, string msg);
        void Log(LogLevel level, string msg, params object[] args);
        void LogIf(bool condition, LogLevel level, string msg);
        void LogIf(bool condition, LogLevel level, string msg, params object[] args);
    }
}
