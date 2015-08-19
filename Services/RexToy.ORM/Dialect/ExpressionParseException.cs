using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace RexToy.ORM.Dialect
{
    [Serializable]
    public class ExpressionParseException : Exception
    {
        public ExpressionParseException()
        {
        }

        public ExpressionParseException(string msg)
            : base(msg)
        {
        }

        public ExpressionParseException(string msg, Exception inner)
            : base(msg, inner)
        {
        }

        protected ExpressionParseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
