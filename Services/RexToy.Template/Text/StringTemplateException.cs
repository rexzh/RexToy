using System;
using System.Runtime.Serialization;

namespace RexToy.Template.Text
{
    [Serializable]
    public class StringTemplateException : Exception
    {
        public StringTemplateException()
        {
        }

        public StringTemplateException(string msg)
            : base(msg)
        {
        }

        public StringTemplateException(string msg, Exception inner)
            : base(msg, inner)
        {
        }

        protected StringTemplateException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
