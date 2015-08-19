using System;
using System.Collections.Generic;

using RexToy.AOP;

namespace UnitTest.AOP
{
    class MyAfterAdvisor : AfterAdvisor
    {
        public static bool executed = false;
        protected override void Run(IMethodCallContext ctx)
        {
            executed = true;
        }
    }
}
