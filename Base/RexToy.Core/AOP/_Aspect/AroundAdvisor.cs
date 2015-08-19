using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace RexToy.AOP
{
    public abstract class AroundAdvisor : Advisor
    {
        private AroundAdvisor _next;
        internal AroundAdvisor Next
        {
            get { return _next; }
            set { _next = value; }
        }

        protected void Proceed()
        {
            if (_next != null)
                _next.Execute();
            else
            {
                IMethodReturnMessage mrm = Context.NextSink.SyncProcessMessage(Context.CallMessage) as IMethodReturnMessage;
                Context.ReturnMessage = mrm;
            }
        }
    }
}
