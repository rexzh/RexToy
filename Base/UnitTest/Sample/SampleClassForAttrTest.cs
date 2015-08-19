using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace UnitTest
{
    [Serializable]
    class SampleClassForAttrTest
    {
        [STAThread]
        public void MethodSTA()
        {
        }

        [Conditional("A")]
        [Conditional("B")]
        public void MethodMultiConditional()
        {
        }

        public void Method()
        {
        }

        [NonSerialized]        
        public int _i = 0;
    }
}
