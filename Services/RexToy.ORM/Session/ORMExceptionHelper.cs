using System;
using System.Collections.Generic;

namespace RexToy.ORM.Session
{
    public static class ORMExceptionHelper
    {
        public static void ThrowFindByPKResultNotUnique(Type entityType)
        {
            throw new ORMException(string.Format("Find by PK on type [{0}] return more than 1 result.", entityType));
        }
    }
}
