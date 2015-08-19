using System;
using System.Collections.Generic;
using System.Globalization;

using RexToy.Logging;
using RexToy.Resources;
using RexToy.Xml;

namespace RexToy.L10N
{
    class Localization : ILocalization
    {
        private const string L10N = @"l10n";
        private const string DICTIONARY = @"dictionary_{0}.xml";
        private static ILog _log = LogContext.GetLogger<Localization>();

        private CultureInfo _cInfo;
        private ITargetLocator _locator;
        private Dictionary<string, string> _dict;
        internal Localization(CultureInfo cInfo, ITargetLocator locator)
        {
            _cInfo = cInfo;

            if (locator == null)
            {
                _log.Warning("Locator pass to Localization is null, create default.");
                _locator = LocatorFactory.Create(LocalFilePath.LOCAL_FILE_SCHEME + Runtime.StartupDirectory);
            }
            else
                _locator = locator;
            _locator = _locator.Combine(L10N);
        }

        public void Initialize()
        {
            _dict = new Dictionary<string, string>();
            //Extend:Load xml and make dict.
            var s = _locator.GetStream(string.Format(DICTIONARY, _cInfo.Name));
            if (s != null)
            {
                using (s)
                {
                    var doc = XDoc.LoadFromStream(s);
                    var entries = doc.NavigateToList("/translate/entry");
                    foreach (var entry in entries)
                    {
                        _dict[entry.GetStringValue("@key")] = entry.GetStringValue("@value");
                    }
                }
            }
        }

        public CultureInfo CultureInfo
        {
            get { return _cInfo; }
        }

        public ITargetLocator Locator
        {
            get { return _locator; }
        }

        public string Localize(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                _log.Warning("Can't localize null or empty string, return empty.");
                return string.Empty;
            }

            string translated;
            bool exist = _dict.TryGetValue(str, out translated);

            if (exist)
            {
                return translated;
            }
            else
            {
                _log.Warning("Can't find localized string of [{0}], return original string.", str);
                return str;
            }
        }
    }
}
