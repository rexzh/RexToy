using System;
using System.Collections.Generic;

namespace RexToy.ORM.Session
{
    public static class CoreFactoryExceptionHelper
    {
        public static void ThrowNoDefaultDbProvider(string dialectId)
        {
            throw new CoreFactoryException(string.Format("No build-in DbProvider for dialect [{0}].", dialectId));
        }

        public static void ThrowNoDefaultDialectProvider(string dialectId)
        {
            throw new CoreFactoryException(string.Format("No build-in DialectProvider for dialect [{0}].", dialectId));
        }

        public static void ThrowMappingInfoAlreadyExist()
        {
            throw new CoreFactoryException("Mapping info cache is already exist.");
        }
    }
}
