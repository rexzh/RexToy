using System;
using System.Runtime.Serialization;

namespace RexToy.ORM
{
    [Serializable]
    public class SessionException : Exception
    {
        public SessionException()
        {
        }

        public SessionException(string msg)
            : base(msg)
        {
        }

        public SessionException(string msg, Exception inner)
            : base(msg, inner)
        {
        }

        protected SessionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
