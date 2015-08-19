using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RexToy.ExpressionLanguage
{
    [Serializable]
    public class EvalException : Exception
    {
        public EvalException()
        {
        }

        public EvalException(string msg)
            : base(msg)
        {
        }

        public EvalException(string msg, Exception inner)
            : base(msg, inner)
        {
        }

        protected EvalException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
