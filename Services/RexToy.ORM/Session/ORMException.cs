using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RexToy.ORM.Session
{
    [Serializable]
    public class ORMException : Exception
    {
        public ORMException()
        {
        }

        public ORMException(string msg)
            : base(msg)
        {
        }

        public ORMException(string msg, Exception inner)
            : base(msg, inner)
        {
        }

        protected ORMException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
