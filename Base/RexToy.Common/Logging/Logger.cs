using System;
using System.Collections.Generic;

namespace RexToy.Logging
{
    sealed partial class Logger : ILog
    {
        private string _name;
        private ILayout _layout;
        private IWriter _writer;
        private LogLevel _level;

        public string Name
        {
            get { return _name; }
        }

        public LogLevel LogLevel
        {
            get { return _level; }
        }

        public Logger(string name)
            : this(name, null)
        {
        }

        public Logger(string name, ILogConfig cfg)
        {
            this._name = name;
            Config(cfg);
        }

        public void Config(ILogConfig cfg)
        {
            if (cfg != null)
            {
                this._layout = cfg.Layout;
                this._writer = cfg.Writer;
                this._level = cfg.GetLogLevel(this._name);
            }
            else
            {
                this._layout = NullLayout.Instance;
                this._writer = NullWriter.Instance;
                this._level = LogLevel.None;
            }
        }
    }
}
