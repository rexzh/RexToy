using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RexToy.Json
{
    [Serializable]
    class ExtendConverterException : Exception
    {
        public ExtendConverterException()
        {
        }

        public ExtendConverterException(string msg)
            : base(msg)
        {
        }

        public ExtendConverterException(string msg, Exception inner)
            : base(msg, inner)
        {
        }

        protected ExtendConverterException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
