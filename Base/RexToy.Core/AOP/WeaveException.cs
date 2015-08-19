using System;
using System.Runtime.Serialization;

namespace RexToy.AOP
{
    [Serializable]
    public class WeaveException : Exception
    {
        public Exception OriginalException { get; set; }

        public WeaveException()
        {
        }

        public WeaveException(string msg)
            : base(msg)
        {
        }

        public WeaveException(string msg, Exception inner)
            : base(msg, inner)
        {
        }

        protected WeaveException(SerializationInfo info, StreamingContext context)
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
