using System;
using System.Collections.Generic;

namespace UnitTest.AOP
{
    class MyDerivedComponent : MyComponent
    {
        public override void Reset()
        {
            _count = -1;
        }
    }
}
