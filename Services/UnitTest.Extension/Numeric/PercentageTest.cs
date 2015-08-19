using System;
using System.Collections.Generic;
using NUnit.Framework;

using RexToy;
using RexToy.Numeric;

namespace UnitTest.Numeric
{
    [TestFixture]
    public class PercentageTest
    {
        [Test]
        public void ExplicitCastTest1()
        {
            Percentage p = (Percentage)1;
            Assert.AreEqual(1, p.Value);
            Assert.AreEqual("100%", p.ToString());
        }

        [Test]
        public void ExplicitCastTest2()
        {
            Percentage p = (Percentage)0.1;
            Assert.AreEqual(0.1, p.Value);
            Assert.AreEqual("10%", p.ToString());
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void ErrParseTest1()
        {
            Percentage p1 = Percentage.Parse(null);
        }

        [Test, ExpectedException(typeof(FormatException))]
        public void ErrParseTest2()
        {
            Percentage p1 = Percentage.Parse(string.Empty);
        }

        [Test]
        public void ParseTest()
        {
            Percentage p1 = Percentage.Parse("10  % ");
            Assert.IsTrue(0.1 == p1.Value);
            Percentage p2 = Percentage.Parse("99%");
            Assert.AreEqual(0.99, p2.Value);
        }

        [Test]
        public void TryParseTest()
        {
            Percentage p1;
            bool b1 = Percentage.TryParse("10%% ", out p1);
            Assert.IsFalse(b1);
            Assert.AreEqual(0, p1.Value);

            Percentage p2;
            bool b2 = Percentage.TryParse("99%", out p2);
            Assert.IsTrue(b2);
            Assert.AreEqual(0.99, p2.Value);
        }

        [Test]
        public void CalcTest1()
        {
            Percentage p = Percentage.Parse("99%");
            Percentage r = p / 3;
            Assert.AreEqual(r, 0.33);
            //Assert.AreEqual(0.33, r);//Note:this will return error!! Since we can not override float type's Equals method.
            Assert.IsTrue((double)r == 0.33);
            Assert.IsTrue(0.33 == (double)r);
        }

        [Test]
        public void CalcTest2()
        {
            Percentage p1 = Percentage.Parse("30%");
            Percentage p2 = Percentage.Parse("70%");
            Assert.IsTrue((p1 + p2) == Percentage.HundredPercent);
            Assert.IsTrue((p2 - p1) == Percentage.Parse("40%"));
        }

        [Test]
        public void CalcTest3()
        {
            Percentage p1 = Percentage.Parse("30%");
            int i = 100;
            Assert.IsTrue(30 == p1 * i);
            Assert.IsTrue(30 == i * p1);
        }

        [Test]
        public void CalcTest4()
        {
            Percentage p1 = Percentage.Parse("30%");
            Percentage p2 = Percentage.Parse("40%");            
            Assert.IsTrue(0.12 == (double)(p1 * p2));
        }

        [Test]
        public void CompareTest1()
        {
            Percentage p1 = Percentage.Parse("30%");
            Percentage p2 = Percentage.Parse("70%");
            Assert.IsTrue(p1 < p2);
            Assert.IsTrue(p1 <= p2);
            Assert.IsTrue(p1 != p2);
        }

        [Test]
        public void EqualTest()
        {
            Percentage p = Percentage.Parse("40%");
            Assert.AreEqual(0.4, p.Value);
        }

        [Test]
        public void ConverterTest1()
        {
            TypeCast.ChangeType<Percentage>("90%");
        }

        [Test]
        public void ConverterTest2()
        {
            TypeCast.ChangeType<Percentage>("0.9");
        }

        [Test]
        public void ConverterTest3()
        {
            TypeCast.ChangeType<Percentage>(1);
        }

        [Test]
        [ExpectedException(typeof(OverflowException))]
        public void ConverterOverflowTest()
        {
            Percentage p = Percentage.Parse("25700%");
            TypeCast.ChangeType<Byte>(p);
        }
    }
}
