using System;
using System.Runtime.Serialization;

namespace RexToy.AOP
{
    [Serializable]
    public class AspectException : Exception
    {
        public Exception OriginalException { get; set; }

        public AspectException()
        {
        }

        public AspectException(string msg)
            : base(msg)
        {
        }

        public AspectException(string msg, Exception inner)
            : base(msg, inner)
        {
        }

        protected AspectException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override string ToString()
        {
            string msg = string.Format("Original exception: {0}\r\n", OriginalException.ToString());
            return msg + base.ToString();
        }
    }
}
