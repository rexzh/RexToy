using System;
using System.Collections.Generic;

namespace RexToy.ORM.MappingInfo
{
    public interface IObjectMapInfoCache
    {
        void SetMapInfo(Type entityType, IObjectMapInfo info);
        IObjectMapInfo GetMapInfo(Type entityType, bool throwOnNotFound);
    }
}
