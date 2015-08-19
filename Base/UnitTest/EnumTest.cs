using System;
using System.Collections.Generic;
using NUnit.Framework;

using RexToy;

namespace UnitTest
{
    [TestFixture]
    public class EnumTest
    {
        [Test]
        public void TestParseString()
        {
            SampleEnum val = EnumEx.Parse<SampleEnum>("Val1");
            Assert.AreEqual(SampleEnum.Val1, val);
        }

        [Test, ExpectedExceptionAttribute(typeof(ArgumentException))]
        public void TestParseErrString()
        {
            SampleEnum val = EnumEx.Parse<SampleEnum>("Val0");
        }

        [Test]
        public void TestParseStringIgnoreCase()
        {
            SampleEnum val = EnumEx.Parse<SampleEnum>("val2", true);
            Assert.AreEqual(SampleEnum.Val2, val);
        }

        [Test]
        public void TestParseInt()
        {
            SampleEnum val = EnumEx.Parse<SampleEnum>(1);
            Assert.AreEqual(SampleEnum.Val1, val);
        }

        [Test, ExpectedExceptionAttribute(typeof(ArgumentException))]
        public void TestParseErrInt()
        {
            SampleEnum val = EnumEx.Parse<SampleEnum>(0);
        }

        [Test]
        public void TestParseIntNotCheck()
        {
            SampleEnum val = EnumEx.Parse<SampleEnum>(3, false);
            Assert.AreEqual(3, (int)val);
        }

        [Test]
        public void TestCheckInRange()
        {
            SampleEnum val1 = (SampleEnum)3;
            Assert.IsFalse(val1.IsValid());

            SampleEnum val2 = (SampleEnum)2;
            Assert.IsTrue(val2.IsValid());
        }

        [Test]
        public void TestGetDescription()
        {
            SampleEnum val1 = SampleEnum.Val1;
            string desc = val1.GetDescription();
            Assert.AreEqual("Value: 1", desc);
        }

        [Test]
        public void TestGetDescriptionFallback()
        {
            SampleEnum val = SampleEnum.Val2;
            string desc = val.GetDescription("Non");
            Assert.AreEqual("Non", desc);
        }

        [Test]
        public void TestGetDescriptionNoFallback()
        {
            SampleEnum val = SampleEnum.Val2;
            string desc = val.GetDescription();
            Assert.AreEqual("Val2", desc);
        }

        [Test, ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestGetDescriptionOutOfRange()
        {
            SampleEnum val = (SampleEnum)3;
            string desc = val.GetDescription();
            Assert.AreEqual("Val2", desc);
        }
    }
}
