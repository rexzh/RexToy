using System;
using System.Collections.Generic;

namespace RexToy.ORM.MappingInfo
{
    public static class MappingInfoExceptionHelper
    {
        public static void ThrowMappingInformationNotFound(Type entityType)
        {
            throw new MappingInfoException(string.Format("Mapping information of type [{0}] not found.", entityType));
        }

        public static void ThrowLoadEntityTypeFail(string prefix, string localName)
        {
            throw new MappingInfoException(string.Format("Entity type [{0}:{1}] load fail.", prefix, localName));
        }
    }
}
