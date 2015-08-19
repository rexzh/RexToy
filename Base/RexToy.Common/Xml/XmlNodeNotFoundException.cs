using System;
using System.Runtime.Serialization;

namespace RexToy.Xml
{
    [Serializable]
    public class XmlNodeNotFoundException : Exception
    {
        public XmlNodeNotFoundException()
        {
        }

        public XmlNodeNotFoundException(string msg)
            : base(msg)
        {
        }

        public XmlNodeNotFoundException(string msg, Exception inner)
            : base(msg, inner)
        {
        }

        protected XmlNodeNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
