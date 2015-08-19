using System;
using System.Runtime.Serialization;

namespace RexToy.IoC
{
    [Serializable]
    public class ManifestException : Exception
    {
        public ManifestException()
        {
        }

        public ManifestException(string msg)
            : base(msg)
        {
        }

        public ManifestException(string msg, Exception inner)
            : base(msg, inner)
        {
        }

        protected ManifestException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
