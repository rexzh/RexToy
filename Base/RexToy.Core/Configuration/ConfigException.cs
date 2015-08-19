using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RexToy.Configuration
{
    [Serializable]
    public class ConfigException : Exception
    {
        public ConfigException()
        {
        }
        
        public ConfigException(string msg)
            : base(msg)
        {
        }

        public ConfigException(string msg, Exception inner)
            : base(msg, inner)
        {
        }

        protected ConfigException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
