using System;

namespace RexToy.ExpressionLanguage
{
    using RexToy.ExpressionLanguage.Tokens;

    static partial class ExceptionHelper
    {
        public static void ThrowExpectToken(TokenType tokenType, long position)
        {
            throw new ELParseException(string.Format("Token type [{0}] expected at position [{1}].", tokenType, position));
        }

        public static void ThrowUnexpectedToken(TokenType tokenType, long position)
        {
            throw new ELParseException(string.Format("Unexpected token type [{0}] at position [{1}].", tokenType, position));
        }

        public static void ThrowParseError(long position)
        {
            throw new ELParseException(string.Format("Error occur at position [{0}].", position));
        }

        public static void ThrowUnresolveException(string param)
        {
            throw new EvalException(string.Format("[{0}] is not assigned.", param));
        }
    }
}
