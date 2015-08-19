using System;

namespace RexToy.Json
{
    static partial class ExceptionHelper
    {
        public static void ThrowNoConverterDefine(Type t)
        {
            string msg = string.Format("No converter define for type [{0}].", t.Name);
            throw new JsonExtendConverterException(msg);
        }
    }
}
