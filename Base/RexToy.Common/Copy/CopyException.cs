using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RexToy.Copy
{
    [Serializable]
    public class CopyException : Exception
    {
        public CopyException()
        {
        }

        public CopyException(string msg)
            : base(msg)
        {
        }

        public CopyException(string msg, Exception inner)
            : base(msg, inner)
        {
        }

        protected CopyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
