using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using RexToy.DesignPattern;

namespace RexToy.Logging
{
    public sealed class LogContext : Singleton<LogContext>, IDisposable
    {
        internal static Dictionary<string, ILog> _logs = new Dictionary<string, ILog>();

        private static string _path = Runtime.GetPath(@".\log.xml");//Note:default path
        public static void Initialize(string path)
        {
            _path = Runtime.GetPath(path);
        }

        private ILogConfig _cfg;
        private ILoggerFactory _factory;
        private LogContext()
        {
            if (File.Exists(_path))
                _cfg = new XmlLogConfig(_path);
            _factory = new LoggerFactory();
        }

        public static ILog GetLogger(string name)
        {
            if (_logs.ContainsKey(name))
                return _logs[name];
            else
            {
                ILog log = Instance()._factory.CreateLogger(name, LogContext.Instance()._cfg);
                _logs.Add(name, log);
                return log;
            }
        }

        public static ILog GetLogger<T>()
        {
            string name = typeof(T).GetScatterName();
            return GetLogger(name);
        }

        public static void Reset()
        {
            foreach (var v in _logs.Values)
            {
                v.Config(LogContext.Instance()._cfg);
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        ~LogContext()
        {
            Dispose(false);
        }

        private bool _disposed;
        private void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                if (_cfg != null)
                    _cfg.Writer.Dispose();
            }

            _disposed = true;
        }
    }
}
