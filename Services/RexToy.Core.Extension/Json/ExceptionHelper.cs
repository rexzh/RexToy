using System;

namespace RexToy.Json
{
    static partial class ExceptionHelper
    {
        public static void ThrowTokenExpected(TokenType token, long position)
        {
            throw new JsonParseException(string.Format("Token ['{0}'] expected at position [{1}].", token, position));
        }

        public static void ThrowParseException(long position)
        {
            throw new JsonParseException(string.Format("Error occur at position: [{0}]", position));
        }

        public static void ThrowSyntaxNoQuotError()
        {
            throw new JsonParseException("String should be surround by quot.");
        }

        public static void ThrowNoDefaultConstructor(Type t)
        {
            throw new ArgumentException(string.Format("Type [{0}] don't have default constructor, json parse fail.", t.Name));
        }

        public static void ThrowExtendConverterUnknownFormat(Type converterType, Type dataType)
        {
            throw new JsonExtendConverterException(string.Format("Extend Converter [{0}] can't handle data type [{1}]", converterType.Name, dataType.Name));
        }
    }
}
