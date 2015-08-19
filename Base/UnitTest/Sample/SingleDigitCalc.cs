using System;

namespace UnitTest.Sample
{
    class SingleDigitCalc : ICalc
    {
        private int _mode;
        public int Mode
        {
            get { return _mode; }
            set
            {
                if (value < 10)
                    throw new ArgumentOutOfRangeException();
                else
                    _mode = value;
            }
        }

        #region ICalc Members

        public int Add(int lhs, int rhs)
        {
            return (lhs + rhs) % Mode;
        }

        public int Sub(int lhs, int rhs)
        {
            return (lhs - rhs) % Mode;
        }

        #endregion
    }
}
