using System;
using System.Collections.Generic;

using RexToy.AOP;

namespace UnitTest.AOP
{
    class MyAroundAdvisor : AroundAdvisor
    {
        public static bool around_b = false;
        public static bool around_a = false;
        protected override void Run(IMethodCallContext ctx)
        {
            around_b = true;
            if (ctx.CallMessage.MethodName != "Reset")
                Proceed();
            around_a = true;
        }
    }
}
