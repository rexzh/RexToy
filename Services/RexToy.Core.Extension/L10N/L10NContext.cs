using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Threading;

using RexToy.Logging;
using RexToy.Configuration;
using RexToy.Resources;

namespace RexToy.L10N
{
    public static class L10NContext
    {
        private const string L10N = "l10n";
        private const string LOCALE = "locale";

        private static ILog _log = LogContext.GetLogger("RexToy.L10N.L10NContext");

        private static ITargetLocator _locator;

        /// <summary>
        /// Uri can be file:/// or clr-ns://, see RexToy.Resources
        /// </summary>
        /// <param name="uri"></param>
        public static void SetResourceBase(string uri)
        {
            if (_locator != null)
            {
                _log.Warning("Localization resource base already set, can't switch.");
                return;
            }

            try
            {
                _locator = LocatorFactory.Create(uri);
            }
            catch
            {
                _log.Warning("Localization build locator with uri [{0}] fail.", uri);
                //Note: will pass null to Localization's constructor. it will handle null.
            }
        }

        private static ConcurrentDictionary<string, ILocalization> _localizations = new ConcurrentDictionary<string, ILocalization>();
        public static ILocalization GetLocalization(string name = null)
        {
            ILocalization l;

            if (string.IsNullOrEmpty(name))
            {
                if (AppConfig.Loaded && AppConfig.Config.ExistsKey(L10N, LOCALE))
                {
                    name = AppConfig.Config.ReadValue(L10N, LOCALE);
                }
                else
                {
                    name = Thread.CurrentThread.CurrentCulture.Name;
                }
            }

            bool exist = _localizations.TryGetValue(name, out l);
            if (exist)
                return l;
            else
            {
                CultureInfo cInfo;
                try
                {
                    cInfo = new CultureInfo(name);
                    _log.Info("Create culture info of [{0}].", name);
                }
                catch (Exception)//Note: localization should always be safe. Don't throw exception.
                {
                    _log.Warning("Can't create culture info with [{0}].", name);
                    cInfo = Thread.CurrentThread.CurrentCulture;
                }

                l = new Localization(cInfo, _locator);
                _localizations[name] = l;
                l.Initialize();
                return l;
            }
        }
    }
}
