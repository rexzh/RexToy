using System;
using System.Runtime.Serialization;

namespace RexToy.Logging
{
    [Serializable]
    public class LogConfigException : Exception
    {
        public LogConfigException()
        {
        }
        
        public LogConfigException(string msg)
            : base(msg)
        {
        }

        public LogConfigException(string msg, Exception inner)
            : base(msg, inner)
        {
        }

        protected LogConfigException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
