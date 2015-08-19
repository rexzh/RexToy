using System;
using System.Collections.Generic;

namespace UnitTest.Sample
{
    class CalcMock
    {
        ICalc _c;
        public CalcMock(ICalc c)
        {
            _c = c;
        }

        public CalcMock()
        {
            _c = new SimpleCalc();
        }

        public string Add(string lhs, string rhs)
        {
            int l = int.Parse(lhs);
            int r = int.Parse(rhs);
            return _c.Add(l, r).ToString();
        }

        public string Sub(string lhs, string rhs)
        {
            int l = int.Parse(lhs);
            int r = int.Parse(rhs);
            return _c.Sub(l, r).ToString();
        }
    }
}
