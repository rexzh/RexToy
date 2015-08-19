using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace RexToy.WebService
{
    public class WebServiceException : Exception
    {
        public WebServiceException()
        {
        }

        public WebServiceException(string msg)
            : base(msg)
        {
        }

        public WebServiceException(string msg, Exception inner)
            : base(msg, inner)
        {
        }

        protected WebServiceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
