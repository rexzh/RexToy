using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Contexts;

namespace RexToy.AOP
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public sealed class DynamicAOPAttribute : ContextAttribute, IContributeObjectSink
    {
        public DynamicAOPAttribute()
            : base(string.Empty)
        {
        }

        public IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink nextSink)
        {
            ISinkFactory factory = SinkContext.GetFactory();
            return factory.Create(obj, nextSink);
        }
    }
}
