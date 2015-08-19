using System;

namespace RexToy.ExpressionLanguage
{
    using RexToy.ExpressionLanguage.Tokens;

    static partial class ExceptionHelper
    {
        public static void ThrowEvalNull()
        {
            throw new EvalException("Null object, eval stop.");
        }

        public static void ThrowEvalToTypeError(string typeName)
        {
            throw new EvalException(string.Format("Load type for name [{0}] failed.", typeName));
        }

        public static void ThrowIndexOrPropertyNotExist(Type t, string memberName)
        {
            throw new EvalException(string.Format("Type [{0}] don't have property of index key [{1}].", t.Name, memberName));
        }

        public static void ThrowUnaryOperatorInvalid(string op, object oprand)
        {
            string o = (oprand != null) ? oprand.GetType().Name : "null";
            throw new EvalException(string.Format("Operator [{0}] can not apply to [{1}].", op, o));
        }

        public static void ThrowBinaryOperatorInvalid(string op, object lhs, object rhs)
        {
            string l = (lhs != null) ? lhs.GetType().Name : "null";
            string r = (rhs != null) ? rhs.GetType().Name : "null";
            throw new EvalException(string.Format("Operator [{0}] can not apply to [{1}] and [{2}].", op, l, r));
        }

        public static void ThrowInvalidToken(TokenType tokenType)
        {
            throw new EvalException(string.Format("[{0}] is not valid in current context.", tokenType));
        }
    }
}
