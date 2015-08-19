using System;
using System.Collections.Generic;

namespace UnitTest.Sample
{
    class SimpleCalc : ICalc
    {
        #region ICalc Members

        public int Add(int lhs, int rhs)
        {
            return lhs + rhs;
        }

        public int Sub(int lhs, int rhs)
        {
            return lhs - rhs;
        }

        #endregion
    }
}
