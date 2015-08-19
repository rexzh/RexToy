using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace RexToy.AOP
{
    class MethodCallContext : IMethodCallContext
    {
        public MethodCallContext(IMethodCallMessage mcm, IMessageSink nextSink)
        {
            mcm.ThrowIfNullArgument(nameof(mcm));
            nextSink.ThrowIfNullArgument(nameof(nextSink));
            _mcm = mcm;
            _nextSink = nextSink;
        }

        private IMessageSink _nextSink;
        private IMethodCallMessage _mcm;
        private IMethodReturnMessage _mrm;

        #region IMethodCallContext Members

        public IMethodCallMessage CallMessage
        {
            get { return _mcm; }
        }

        public IMethodReturnMessage ReturnMessage
        {
            get
            {
                return _mrm;
            }
            set
            {
                value.ThrowIfNullArgument(nameof(value));
                _mrm = value;
            }
        }

        public IMessageSink NextSink
        {
            get { return _nextSink; }
        }

        public Position Position { get; set; }

        #endregion
    }
}
