using System;
using System.Runtime.Serialization;

namespace RexToy
{
    [Serializable]
    public class AssertException : Exception
    {
        public AssertException()
        {
        }

        public AssertException(string msg)
            : base(msg)
        {
        }

        public AssertException(string msg, Exception inner)
            : base(msg, inner)
        {
        }

        protected AssertException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
