using System;
using System.Collections.Generic;

namespace RexToy.AOP
{
    public abstract class Advisor : IAdvisor
    {
        private IMethodCallContext _ctx;
        protected IMethodCallContext Context
        {
            get { return _ctx; }
        }

        #region IAdvisor Members

        public void SetContext(IMethodCallContext ctx)
        {
            _ctx = ctx;
        }

        #endregion

        public void Execute()
        {
            this.Run(_ctx);
        }

        protected abstract void Run(IMethodCallContext ctx);
    }
}
