using System;
using System.Collections.Generic;

namespace UnitTest
{
    class SampleFuncOverload
    {
        public string Sum(int i, int j = 10)
        {
            return "v1";
        }

        public string Sum(int i, int j = 10, int k = 20)
        {
            return "v2";
        }

        public string Sum(int i, params int[] arr)
        {
            return "v3";
        }
    }
}
