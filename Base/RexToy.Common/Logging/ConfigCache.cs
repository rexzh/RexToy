using System;
using System.Collections.Generic;

using RexToy.Collections;

namespace RexToy.Logging
{
    class ConfigCache : LazyLoadDictionary<string, LogLevel>
    {
        protected override LogLevel Load(string key)
        {
            while (!InternalDictionary.ContainsKey(key))
            {
                int idx = key.LastIndexOf('.');
                if (idx < 0)
                    return InternalDictionary[string.Empty];
                key = key.Substring(0, idx);
            }
            return InternalDictionary[key];
        }

        public void Add(string key, LogLevel level)
        {
            InternalDictionary.Add(key, level);
        }

        public void SetDefaultIfNotExist()
        {
            if (!InternalDictionary.ContainsKey(string.Empty))
                InternalDictionary.Add(string.Empty, LogLevel.None);
        }
    }
}
