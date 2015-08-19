using System;
using System.Runtime.Serialization;

namespace RexToy.AOP.Services
{
    [Serializable]
    public class TransactionManagementException : Exception
    {
        public TransactionManagementException()
        {
        }

        public TransactionManagementException(string msg)
            : base(msg)
        {
        }

        public TransactionManagementException(string msg, Exception inner)
            : base(msg, inner)
        {
        }

        protected TransactionManagementException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
