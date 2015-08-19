using System;

using NUnit.Framework;

using RexToy;
using RexToy.Numeric;

namespace UnitTest.Numeric
{
    [TestFixture]
    public class ValueTest
    {
        [Test]
        public void AssignTest()
        {
            MeasuredNumber v1 = MeasuredNumber.Parse("123s");
            MeasuredNumber v2 = MeasuredNumber.Parse("12.3m");
            MeasuredNumber v3 = MeasuredNumber.Parse("-9.87m");
        }

        [Test, ExpectedException(typeof(ArgumentNullException))]
        public void NullAssignTest()
        {
            MeasuredNumber v = MeasuredNumber.Parse(null);
        }

        [Test, ExpectedException(typeof(FormatException))]
        public void EmptyAssignTest()
        {
            MeasuredNumber v = MeasuredNumber.Parse(string.Empty);
        }

        [Test]
        public void TryParseTest()
        {
            MeasuredNumber v;
            bool b;
            b = MeasuredNumber.TryParse(null, out v);
            Assert.IsFalse(b);
            Assert.AreEqual(0, v.Number);
            Assert.AreEqual(MeasureUnit.None, v.Unit);
            
            b = MeasuredNumber.TryParse(string.Empty, out v);
            Assert.IsFalse(b);
            Assert.AreEqual(0, v.Number);
            Assert.AreEqual(MeasureUnit.None, v.Unit);

            b = MeasuredNumber.TryParse("1.5.3m", out v);
            Assert.IsFalse(b);
            Assert.AreEqual(0, v.Number);
            Assert.AreEqual(MeasureUnit.None, v.Unit);
        }

        [Test, ExpectedException(typeof(FormatException))]
        public void InvalidAssignTest()
        {
            MeasuredNumber v = MeasuredNumber.Parse("1.5.3m");
        }

        [Test]
        public void CalcTest1()
        {
            MeasuredNumber m = MeasuredNumber.Parse("1kg"), g = MeasuredNumber.Parse("9.8m/(s*s)");
            var G = m * g;
            Assert.AreEqual(MeasureUnit.Parse("kg*m/(s*s)"), G.Unit);
            Assert.AreEqual(9.8, G.Number);
        }

        [Test]
        public void CalcTest2()
        {
            MeasuredNumber t = MeasuredNumber.Parse("10s"), a = MeasuredNumber.Parse("1m/(s*s)");
            var v = a * t;
            Assert.AreEqual(MeasureUnit.Parse("m/s"), v.Unit);
            Assert.AreEqual(10, v.Number);
        }

        [Test]
        public void CalcTest3()
        {
            MeasuredNumber t = MeasuredNumber.Parse("10s"), a = MeasuredNumber.Parse("1m/(s*s)");
            var s = a * t * t / 2;
            Assert.AreEqual(MeasureUnit.Parse("m"), s.Unit);
            Assert.AreEqual(50, s.Number);
        }

        [Test]
        public void CalcTest4()
        {
            MeasuredNumber c = MeasuredNumber.Parse("100CNY");
            MeasuredNumber ex = MeasuredNumber.Parse("6.5CNY/USD");
            MeasuredNumber u = MeasuredNumber.Parse("60USD");
            var v = c / ex;
            Assert.AreEqual(MeasureUnit.Parse("USD"), v.Unit);
            Assert.AreEqual(u.Unit, v.Unit);
        }

        [Test]
        public void EqualTest1()
        {
            MeasuredNumber c = MeasuredNumber.Parse("100");
            c = c * 2;
            Assert.AreEqual(200, c.Number);
            Assert.AreEqual(MeasureUnit.None, c.Unit);
        }

        [Test]
        public void EqualTest3()
        {
            MeasuredNumber c = MeasuredNumber.Parse("100kg*m/s");
            var d = c * 2;
            var e = 2 * c;
            Assert.AreEqual(d, e);
        }

        [Test]
        public void ConverterTest1()
        {
            var mn = TypeCast.ChangeType<MeasuredNumber>("100m");
            Assert.AreEqual(100, mn.Number);
            Assert.AreEqual(MeasureUnit.Parse("m"), mn.Unit);
        }

        [Test]
        public void ConverterTest2()
        {
            var mn = TypeCast.ChangeType<MeasuredNumber>("0.9m/(s*s)");
            Assert.AreEqual(0.9, mn.Number);
            Assert.AreEqual(MeasureUnit.Parse("m/(s*s)"), mn.Unit);
        }

        [Test, ExpectedException(typeof(InvalidCastException))]
        public void ConverterTest3()
        {
            var mn = TypeCast.ChangeType<MeasuredNumber>(100);            
        }
    }
}
