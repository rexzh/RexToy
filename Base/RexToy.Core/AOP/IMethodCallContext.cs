using System;
using System.Runtime.Remoting.Messaging;

namespace RexToy.AOP
{
    public interface IMethodCallContext
    {
        IMethodCallMessage CallMessage { get; }
        IMethodReturnMessage ReturnMessage { get; set; }
        IMessageSink NextSink { get; }
        Position Position { get; }
    }
}