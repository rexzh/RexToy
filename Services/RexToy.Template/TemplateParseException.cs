using System;
using System.Runtime.Serialization;

namespace RexToy.Template
{
    [Serializable]
    public class TemplateParseException : Exception
    {
        public TemplateParseException()
        {
        }

        public TemplateParseException(string msg)
            : base(msg)
        {
        }

        public TemplateParseException(string msg, Exception inner)
            : base(msg, inner)
        {
        }

        protected TemplateParseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
