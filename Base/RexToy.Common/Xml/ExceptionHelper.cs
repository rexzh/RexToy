using System;

namespace RexToy.Xml
{
    static class ExceptionHelper
    {
        public static void ThrowInvalidCastException(object val, Type targetType)
        {
            throw new InvalidCastException(string.Format("Can not convert [{0}] to type [{1}].", val, targetType));
        }

        public static void ThrowNodeNotFoundException(string xpath)
        {
            throw new XmlNodeNotFoundException(string.Format("No node found with XPath [{0}].", xpath));
        }
    }
}
