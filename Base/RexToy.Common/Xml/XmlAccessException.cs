using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RexToy.Xml
{
    [Serializable]
    public class XmlAccessException : Exception
    {
        public XmlAccessException()
        {
        }

        public XmlAccessException(string msg)
            : base(msg)
        {
        }

        public XmlAccessException(string msg, Exception inner)
            : base(msg, inner)
        {
        }

        protected XmlAccessException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
