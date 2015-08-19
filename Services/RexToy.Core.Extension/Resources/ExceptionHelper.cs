using System;
using System.Collections.Generic;

namespace RexToy.Resources
{
    static class ExceptionHelper
    {
        public static void ThrowUnknowSchema(string uri)
        {
            throw new LocatorException(string.Format("The uri:[{0}] is not supported.", uri));
        }

        public static void ThrowNotFound(string uri)
        {
            throw new LocatorException(string.Format("Target not found: [{0}]", uri));
        }
    }
}
