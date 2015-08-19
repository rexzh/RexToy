using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RexToy.Numeric
{
    [Serializable]
    public class UnitMismatchException : Exception
    {
        public UnitMismatchException()
        {
        }

        public UnitMismatchException(string msg)
            : base(msg)
        {
        }

        public UnitMismatchException(string msg, Exception inner)
            : base(msg, inner)
        {
        }

        protected UnitMismatchException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
