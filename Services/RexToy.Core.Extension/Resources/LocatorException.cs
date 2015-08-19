using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RexToy.Resources
{
    [Serializable]
    public class LocatorException : Exception
    {
        public LocatorException()
        {
        }

        public LocatorException(string msg)
            : base(msg)
        {
        }

        public LocatorException(string msg, Exception inner)
            : base(msg, inner)
        {
        }

        protected LocatorException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
