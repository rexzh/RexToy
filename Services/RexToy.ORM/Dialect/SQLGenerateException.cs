using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RexToy.ORM.Dialect
{
    [Serializable]
    public class SQLGenerateException : Exception
    {
        public SQLGenerateException()
        {
        }

        public SQLGenerateException(string msg)
            : base(msg)
        {
        }

        public SQLGenerateException(string msg, Exception inner)
            : base(msg, inner)
        {
        }

        protected SQLGenerateException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
