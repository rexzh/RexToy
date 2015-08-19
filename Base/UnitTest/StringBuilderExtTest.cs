using System;
using System.Text;
using NUnit.Framework;

using RexToy;

namespace UnitTest
{
    [TestFixture]
    public class StringBuilderExtTest
    {
        [Test]
        public void UpdateTest1()
        {
            StringBuilder str = new StringBuilder("[TestString]");

            Assert.IsTrue(str.StartsWith("["));
            Assert.IsTrue(str.EndsWith("]"));

            Assert.AreEqual("TestString", str.UnBracketing(StringPair.SquareBracket).ToString());
        }

        [Test]
        public void UpdateTest2()
        {
            StringBuilder str = new StringBuilder("[TestString]");
            str.RemoveBegin(1).RemoveEnd(1);

            Assert.AreEqual("TestString", str.ToString());
        }

        [Test]
        public void UpdateTest3()
        {
            StringBuilder str = new StringBuilder("[TestString]");
            str.RemoveBegin("<").RemoveEnd(">");

            Assert.AreEqual("[TestString]", str.ToString());

            str.UnBracketing(StringPair.SingleQuote);
            Assert.AreEqual("[TestString]", str.ToString());
        }

        [Test]
        public void IndexTest()
        {
            StringBuilder str = new StringBuilder("Hello");
            Assert.AreEqual(1, str.IndexOf("el"));
            Assert.AreEqual(3, str.LastIndexOf("l"));
        }

        [Test]
        public void EmptyStringBuilderTest()
        {
            StringBuilder str = new StringBuilder(string.Empty);
            str.RemoveBegin('(').RemoveEnd(')');
            Assert.AreEqual(0, str.Length);
        }
    }
}
