using System;
using System.Runtime.Serialization;

namespace RexToy.Json
{
    [Serializable]
    public class JsonExtendConverterException : Exception
    {
        public JsonExtendConverterException()
        {
        }

        public JsonExtendConverterException(string msg)
            : base(msg)
        {
        }

        public JsonExtendConverterException(string msg, Exception inner)
            : base(msg, inner)
        {
        }

        protected JsonExtendConverterException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
