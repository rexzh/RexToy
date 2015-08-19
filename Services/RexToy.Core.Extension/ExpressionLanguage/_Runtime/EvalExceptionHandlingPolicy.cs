using System;

namespace RexToy.ExpressionLanguage
{
    public class EvalExceptionHandlingPolicy
    {
        public static IEvalExceptionHandlingPolicy ThrowPolicy = new ThrowPolicy();
        public static IEvalExceptionHandlingPolicy IgnorePolicy = new IgnorePolicy();
    }
}
