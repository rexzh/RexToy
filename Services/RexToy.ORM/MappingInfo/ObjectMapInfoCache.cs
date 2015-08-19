using System;
using System.Collections.Generic;
using System.IO;

using RexToy.Logging;
using RexToy.DesignPattern;
using RexToy.Xml;

namespace RexToy.ORM.MappingInfo
{
    public class ObjectMapInfoCache : IObjectMapInfoCache
    {
        private static ILog _log = LogContext.GetLogger<ObjectMapInfoCache>();

        private Dictionary<Type, IObjectMapInfo> _cache;
        public ObjectMapInfoCache()
        {
            _cache = new Dictionary<Type, IObjectMapInfo>();
        }

        public void SetMapInfo(Type entityType, IObjectMapInfo info)
        {
            _log.WarningIf(_cache.ContainsKey(entityType), "Entity type [{0}] mapping information is overrided!.", entityType);

            _cache[entityType] = info;
        }

        public IObjectMapInfo GetMapInfo(Type entityType, bool throwOnNotFound)
        {
            if (_cache.ContainsKey(entityType))
                return _cache[entityType];
            else
            {
                if (throwOnNotFound)
                    MappingInfoExceptionHelper.ThrowMappingInformationNotFound(entityType);

                return null;
            }
        }
    }
}
