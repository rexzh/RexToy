using System;
using System.Text;
using NUnit.Framework;

using RexToy;

namespace UnitTest
{
    [TestFixture]
    public class StringExtTest
    {
        [Test]
        public void UpdateTest1()
        {
            string str = "[TestString]";

            Assert.AreEqual("TestString", str.UnBracketing(StringPair.SquareBracket));
        }

        [Test]
        public void UpdateTest2()
        {
            string str = "[TestString]";
            str = str.RemoveBegin(1).RemoveEnd(1);

            Assert.AreEqual("TestString", str);
        }

        [Test]
        public void UpdateTest3()
        {
            string str = "[TestString]";
            str.RemoveBegin('<').RemoveEnd('>');

            Assert.AreEqual("[TestString]", str);

            str.UnBracketing(StringPair.SingleQuote);
            Assert.AreEqual("[TestString]", str.ToString());
        }

        [Test]
        public void EmptyStringTest()
        {
            string str = string.Empty;
            str.RemoveBegin('(').RemoveEnd(')');
            Assert.AreEqual(0, str.Length);
        }

        [Test]
        public void PrefixTest()
        {
            string str = "COM1.BaudRate";
            Assert.IsTrue(str.IsPrefixWith("COM1", PrefixedName.DotDelimiter));
        }
    }
}
