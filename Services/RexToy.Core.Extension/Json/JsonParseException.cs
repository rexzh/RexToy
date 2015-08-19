using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RexToy.Json
{
    [Serializable]
    public class JsonParseException : Exception
    {
        public JsonParseException()
        {
        }

        public JsonParseException(string msg)
            : base(msg)
        {
        }

        public JsonParseException(string msg, Exception inner)
            : base(msg, inner)
        {
        }

        protected JsonParseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
