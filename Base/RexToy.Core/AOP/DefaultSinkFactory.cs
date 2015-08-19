using System;
using System.Runtime.Remoting.Messaging;

namespace RexToy.AOP
{
    class DefaultSinkFactory : ISinkFactory
    {
        #region ISinkFactory Members

        public IMessageSink Create(MarshalByRefObject obj, IMessageSink nextSink)
        {
            return new InterceptSink(obj, nextSink);
        }

        #endregion
    }
}
