using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTest.Sample
{
    public class DefaultCalc : ICalc
    {
        private int _base;
        public DefaultCalc(int b = 3)
        {
            _base = b;
        }

        public int Add(int lhs, int rhs)
        {
            return lhs + rhs + _base;
        }

        public int Sub(int lhs, int rhs)
        {
            return lhs - rhs + _base;
        }
    }
}
