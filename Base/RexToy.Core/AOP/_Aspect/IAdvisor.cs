using System;
using System.Collections.Generic;

namespace RexToy.AOP
{
    public interface IAdvisor
    {
        void SetContext(IMethodCallContext ctx);
        void Execute();
    }
}
