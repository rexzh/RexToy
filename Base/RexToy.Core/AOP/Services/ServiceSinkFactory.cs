using System;
using System.Runtime.Remoting.Messaging;

namespace RexToy.AOP.Services
{
    public class ServiceSinkFactory : ISinkFactory
    {
        #region ISinkFactory Members

        public IMessageSink Create(MarshalByRefObject obj, IMessageSink nextSink)
        {
            return new ServiceSink(obj, nextSink);
        }

        #endregion
    }
}
