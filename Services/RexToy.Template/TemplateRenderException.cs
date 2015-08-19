using System;
using System.Runtime.Serialization;

namespace RexToy.Template
{
    [Serializable]
    public class TemplateRenderException : Exception
    {
        public TemplateRenderException()
        {
        }

        public TemplateRenderException(string msg)
            : base(msg)
        {
        }

        public TemplateRenderException(string msg, Exception inner)
            : base(msg, inner)
        {
        }

        protected TemplateRenderException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
