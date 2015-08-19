using System;
using System.Collections.Generic;

namespace UnitTest.Sample
{
    public class CalcFactory
    {
        public static ICalc Create(string id)
        {
            switch (id)
            {
                case "SingleDigit":
                    return new SingleDigitCalc();

                default:
                    return new SimpleCalc();
            }
        }

        public static ICalc Create()
        {
            return new SingleDigitCalc();
        }
    }
}
