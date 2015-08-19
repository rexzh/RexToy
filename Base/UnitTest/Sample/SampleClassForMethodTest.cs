using System;
using System.Collections.Generic;

namespace UnitTest
{
    class SampleClassForMethodTest
    {
        public string Normal(int i)
        {
            return "1";
        }

        public string OptionalArgs(int i, int j = 10)
        {
            if (j == 10)
                return "2.1";
            else
                return "2.2";
        }

        public string VarArgs(int i, params object[] vargs)
        {
            if (vargs.Length == 0)
                return "3.1";
            else
                return "3.2";
        }

        public string OptAndVarArgs(int i, int j = 10, params object[] vargs)
        {
            if (vargs.Length == 0 && j == 10)
                return "4.1";
            if (j != 10 && vargs.Length == 0)
                return "4.2";
            if (j != 10 && vargs.Length > 0)
                return "4.3";
            return "4.x";
        }
    }
}
