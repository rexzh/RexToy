using System;
using System.Runtime.Serialization;

namespace RexToy.ORM.Configuration
{
    [Serializable]
    public class ORMConfigException : Exception
    {
        public ORMConfigException()
        {
        }
        
        public ORMConfigException(string msg)
            : base(msg)
        {
        }

        public ORMConfigException(string msg, Exception inner)
            : base(msg, inner)
        {
        }

        protected ORMConfigException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
