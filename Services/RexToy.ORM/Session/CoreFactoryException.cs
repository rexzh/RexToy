using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RexToy.ORM.Session
{
    [Serializable]
    public class CoreFactoryException : Exception
    {
        public CoreFactoryException()
        {
        }

        public CoreFactoryException(string msg)
            : base(msg)
        {
        }

        public CoreFactoryException(string msg, Exception inner)
            : base(msg, inner)
        {
        }

        protected CoreFactoryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
