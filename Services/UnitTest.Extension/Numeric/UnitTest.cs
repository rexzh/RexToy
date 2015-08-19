using System;
using System.Collections.Generic;

using NUnit.Framework;

using RexToy.Numeric;

namespace UnitTest.Numeric
{
    [TestFixture]
    public class UnitTest
    {
        [Test]
        public void EqualTest1()
        {
            MeasuredNumber v1 = MeasuredNumber.Parse("12m/s");
            MeasuredNumber v2 = MeasuredNumber.Parse("3m/s");
            Assert.AreEqual(v1.Unit, v2.Unit);
        }

        [Test]
        public void EqualTest2()
        {
            MeasuredNumber v1 = MeasuredNumber.Parse("12m*s");
            MeasuredNumber v2 = MeasuredNumber.Parse("3s*m");
            Assert.AreEqual(v1.Unit, v2.Unit);
        }

        [Test]
        public void ParseTest()
        {
            MeasuredNumber v1 = MeasuredNumber.Parse("9.8kg*m/(s*s)");
            MeasuredNumber v2 = MeasuredNumber.Parse("100kg");
        }

        [Test]
        public void EmptyTest()
        {
            MeasureUnit u1 = new MeasureUnit(), u2 = new MeasureUnit();
            bool b = u1 == u2;
            Assert.IsTrue(b);
        }
    }
}
