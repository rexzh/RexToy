using System;
using System.Runtime.Serialization;

namespace RexToy.IoC
{
    [Serializable]
    public class ObjectBuilderException : Exception
    {
        public ObjectBuilderException()
        {
        }

        public ObjectBuilderException(string msg)
            : base(msg)
        {
        }

        public ObjectBuilderException(string msg, Exception inner)
            : base(msg, inner)
        {
        }

        protected ObjectBuilderException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
