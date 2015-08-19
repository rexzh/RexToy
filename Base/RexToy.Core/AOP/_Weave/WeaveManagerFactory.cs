using System;

using RexToy.Configuration;

namespace RexToy.AOP
{
    public static class WeaveManagerFactory
    {
        static WeaveManagerFactory()
        {
            _mgr = new WeaveManager();
            _mgr.ReadConfig();
        }

        private static IWeaveManager _mgr;
        public static IWeaveManager Create()
        {
            return _mgr;
        }
    }
}
