using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RexToy.ExpressionLanguage
{
    [Serializable]
    public class ELParseException : Exception
    {
        public ELParseException()
        {
        }

        public ELParseException(string msg)
            : base(msg)
        {
        }

        public ELParseException(string msg, Exception inner)
            : base(msg, inner)
        {
        }

        protected ELParseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
