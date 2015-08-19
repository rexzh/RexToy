using System;
using System.Collections.Generic;

using RexToy.Logging;

namespace UnitTest.Logging
{
    class SampleLogConfig : ILogConfig
    {
        #region ILogConfig Members

        public LogLevel GetLogLevel(string name)
        {
            switch (name)
            {
                case "debug":
                    return LogLevel.Debug;

                case "info":
                    return LogLevel.Info;

                default:
                    return LogLevel.None;
            }
        }

        private ILayout _l = new SampleLayout();
        public ILayout Layout
        {
            get { return _l; }
        }

        private IWriter _w = new SampleWriter();
        public IWriter Writer
        {
            get { return _w; }
        }

        #endregion
    }
}
