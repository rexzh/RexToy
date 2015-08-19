using System;
using System.Runtime.Serialization;

namespace RexToy
{
    [Serializable]
    public class ReflectorException : Exception
    {
        public ReflectorException()
        {
        }

        public ReflectorException(string msg)
            : base(msg)
        {
        }

        public ReflectorException(string msg, Exception inner)
            : base(msg, inner)
        {
        }

        protected ReflectorException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
