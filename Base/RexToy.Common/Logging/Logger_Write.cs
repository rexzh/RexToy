using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RexToy.Logging
{
    partial class Logger
    {
        #region Log
        [SuppressMessage("Microsoft.Design", "CA1031")]
        public void Log(LogLevel level, string msg)
        {
            if (level < this._level)
                return;

            try
            {
                string content = _layout.Format(this.Name, level, msg);
                _writer.Write(content);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1031")]
        public void Log(LogLevel level, string msg, params object[] args)
        {
            string _msg = string.Empty;

            try
            {
                _msg = string.Format(msg, args);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

            this.Log(level, _msg);
        }

        public void Verbose(string msg)
        {
            this.Log(LogLevel.Verbose, msg);
        }

        public void Verbose(string msg, params object[] args)
        {
            this.Log(LogLevel.Verbose, msg, args);
        }

        public void Debug(string msg)
        {
            this.Log(LogLevel.Debug, msg);
        }

        public void Debug(string msg, params object[] args)
        {
            this.Log(LogLevel.Debug, msg, args);
        }

        public void Info(string msg)
        {
            this.Log(LogLevel.Info, msg);
        }

        public void Info(string msg, params object[] args)
        {
            this.Log(LogLevel.Info, msg, args);
        }

        public void Warning(string msg)
        {
            this.Log(LogLevel.Warning, msg);
        }

        public void Warning(string msg, params object[] args)
        {
            this.Log(LogLevel.Warning, msg, args);
        }

        public void Error(string msg)
        {
            this.Log(LogLevel.Error, msg);
        }

        public void Error(string msg, params object[] args)
        {
            this.Log(LogLevel.Error, msg, args);
        }

        public void Fatal(string msg)
        {
            this.Log(LogLevel.Fatal, msg);
        }

        public void Fatal(string msg, params object[] args)
        {
            this.Log(LogLevel.Fatal, msg, args);
        }

        [SuppressMessage("Microsoft.Design", "CA1031")]
        public void LogIf(bool condition, LogLevel level, string msg)
        {
            if (level < this._level || !condition)
                return;

            try
            {
                string content = _layout.Format(this.Name, level, msg);
                _writer.Write(content);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1031")]
        public void LogIf(bool condition, LogLevel level, string msg, params object[] args)
        {
            string _msg = string.Empty;

            try
            {
                _msg = string.Format(msg, args);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

            this.LogIf(condition, level, _msg);
        }

        public void VerboseIf(bool condition, string msg)
        {
            this.LogIf(condition, LogLevel.Verbose, msg);
        }

        public void VerboseIf(bool condition, string msg, params object[] args)
        {
            this.LogIf(condition, LogLevel.Verbose, msg, args);
        }

        public void DebugIf(bool condition, string msg)
        {
            this.LogIf(condition, LogLevel.Debug, msg);
        }

        public void DebugIf(bool condition, string msg, params object[] args)
        {
            this.LogIf(condition, LogLevel.Debug, msg, args);
        }

        public void InfoIf(bool condition, string msg)
        {
            this.LogIf(condition, LogLevel.Info, msg);
        }

        public void InfoIf(bool condition, string msg, params object[] args)
        {
            this.LogIf(condition, LogLevel.Info, msg, args);
        }

        public void WarningIf(bool condition, string msg)
        {
            this.LogIf(condition, LogLevel.Warning, msg);
        }

        public void WarningIf(bool condition, string msg, params object[] args)
        {
            this.LogIf(condition, LogLevel.Warning, msg, args);
        }

        public void ErrorIf(bool condition, string msg)
        {
            this.LogIf(condition, LogLevel.Error, msg);
        }

        public void ErrorIf(bool condition, string msg, params object[] args)
        {
            this.LogIf(condition, LogLevel.Error, msg, args);
        }

        public void FatalIf(bool condition, string msg)
        {
            this.LogIf(condition, LogLevel.Fatal, msg);
        }

        public void FatalIf(bool condition, string msg, params object[] args)
        {
            this.LogIf(condition, LogLevel.Fatal, msg, args);
        }
        #endregion
    }
}
