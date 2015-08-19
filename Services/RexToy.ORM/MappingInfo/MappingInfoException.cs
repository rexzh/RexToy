using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RexToy.ORM.MappingInfo
{
    [Serializable]
    public class MappingInfoException : Exception
    {
        public MappingInfoException()
        {
        }

        public MappingInfoException(string msg)
            : base(msg)
        {
        }

        public MappingInfoException(string msg, Exception inner)
            : base(msg, inner)
        {
        }

        protected MappingInfoException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
