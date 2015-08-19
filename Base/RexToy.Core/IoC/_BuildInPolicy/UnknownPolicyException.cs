using System;
using System.Runtime.Serialization;

namespace RexToy.IoC
{
    [Serializable]
    public class UnknownPolicyException : Exception
    {
        public UnknownPolicyException()
        {
        }

        public UnknownPolicyException(string msg)
            : base(msg)
        {
        }

        public UnknownPolicyException(string msg, Exception inner)
            : base(msg, inner)
        {
        }

        protected UnknownPolicyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
