using System;
using System.Collections.Generic;

namespace RexToy.Copy
{
    public static class ExceptionHelper
    {
        public static void ThrowDestPropertyNotExist(Type destType, string property)
        {
            throw new CopyException(string.Format("Dest type [{0}] does not have property [{1}].", destType, property));
        }

        public static void ThrowSourceKeyOrPropertyNotExist(string property)
        {
            throw new CopyException(string.Format("Source does not have property/key [{0}].", property));
        }
    }
}
