using System;
using System.Runtime.Remoting.Messaging;

namespace RexToy.AOP
{
    public interface ISinkFactory
    {
        IMessageSink Create(MarshalByRefObject obj, IMessageSink nextSink);
    }
}
