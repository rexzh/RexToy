using System;
using System.Collections.Generic;
using System.IO;

using RexToy.Xml;

namespace RexToy.Logging
{
    class XmlLogConfig : ILogConfig
    {
        private const string LAYOUT = "layout";
        private const string WRITER = "writer";
        private const string NAME_ATTR = "@name";
        private const string LEVEL_ATTR = "@level";
        private const string LOG = "log";

        private XAccessor _x;
        private ConfigCache _cache;
        public XmlLogConfig(string path)
        {
            if (!File.Exists(path))
                ExceptionHelper.ThrowConfigFileNotExist(path);
            _x = XDoc.LoadFromFile(path);
            _cache = new ConfigCache();
            Initialize();
        }

        private void Initialize()
        {
            _layout = Reflector.LoadInstance<ILayout>(_x.GetStringValue(LAYOUT));
            _writer = Reflector.LoadInstance<IWriter>(_x.GetStringValue(WRITER));

            var logs = _x.NavigateToList(LOG);
            foreach (XAccessor log in logs)
            {
                string name = log.GetStringValue(NAME_ATTR);
                if (string.IsNullOrWhiteSpace(name))
                {
                    ExceptionHelper.ThrowConfigItemNull("log name");
                }
                LogLevel? level = log.GetEnumValue<LogLevel>(LEVEL_ATTR);
                if (!level.HasValue)
                {
                    ExceptionHelper.ThrowConfigItemNull("log level");
                }
                _cache.Add(name, level.Value);
            }

            _cache.SetDefaultIfNotExist();
        }

        private IWriter _writer;
        public IWriter Writer
        {
            get { return _writer; }
        }

        private ILayout _layout;
        public ILayout Layout
        {
            get { return _layout; }
        }

        public LogLevel GetLogLevel(string name)
        {
            return _cache[name];
        }
    }
}