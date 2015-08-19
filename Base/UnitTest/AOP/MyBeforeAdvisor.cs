using System;
using System.Collections.Generic;

using RexToy.AOP;

namespace UnitTest.AOP
{
    class MyBeforeAdvisor : BeforeAdvisor
    {
        public static bool executed = false;
        protected override void Run(IMethodCallContext ctx)
        {
            executed = true;
        }
    }
}
