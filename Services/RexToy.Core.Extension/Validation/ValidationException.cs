using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RexToy.Validation
{
    [Serializable]
    public class ValidationException : Exception
    {
        public ValidationException()
        {
        }

        public ValidationException(string msg)
            : base(msg)
        {
        }

        public ValidationException(string msg, Exception inner)
            : base(msg, inner)
        {
        }

        protected ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
