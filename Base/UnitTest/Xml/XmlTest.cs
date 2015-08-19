using System;
using System.Collections.Generic;
using NUnit.Framework;

using RexToy.Xml;

namespace UnitTest.Xml
{
    [TestFixture]
    public class XmlTest
    {
        private static XDoc x;
        [TestFixtureSetUp]
        public static void SetUp()
        {
            x = XDoc.LoadFromFile(@"..\..\..\UnitTest\Xml\Sample.xml");
            x.AddNamespace("bk", "urn:Books");//Note:'bk' is a fake name must provide when navigate xml
            x.AddNamespace("pub", "urn:Publisher");
        }

        [Test]
        public void TestNavigateNode()
        {
            var xBook = x.NavigateToSingle("bk:Book");//Note:select single will return first item
            Assert.AreEqual("", xBook.Prefix);//Note:the prefix is the value in the xml
            Assert.AreEqual("Book", xBook.LocalName);
            var xPub = xBook.NavigateToSingle("pub:Publisher");
            Assert.AreEqual("pub", xPub.Prefix);
            Assert.AreEqual("Publisher", xPub.LocalName);
        }

        [Test]
        public void TestNavigateNotExist()
        {
            var xBook = x.NavigateToSingleOrNull("bk:book");
            Assert.IsNull(xBook);
        }

        [Test, ExpectedException(typeof(XmlNodeNotFoundException))]
        public void TestNavigateNotExistThrow()
        {
            var xBook = x.NavigateToSingle("bk:book");
        }

        [Test]
        public void TestNavigateNodeList()
        {
            var xBooks = x.NavigateToList("//bk:Book");
            Assert.AreEqual(4, xBooks.Count);
        }

        [Test]
        public void ComplexXPathExample1()
        {
            var xBooks = x.NavigateToList("bk:Book[@category='Senior']");
            Assert.AreEqual(2, xBooks.Count);
        }

        [Test]
        public void ComplexXPathExample2()
        {
            var xBooks = x.NavigateToList("//*[@category]");
            Assert.AreEqual(4, xBooks.Count);
            foreach (XAccessor b in xBooks)
            {
                Assert.AreEqual(string.Empty, b.Prefix);
                Assert.AreEqual("Book", b.LocalName);
            }
        }

        [Test]
        public void ComplexXPathExample3()
        {
            var categories = x.NavigateToList("bk:Book/@category");
            Assert.AreEqual(4, categories.Count);
        }

        [Test]
        public void GetValueTest()
        {
            Assert.AreEqual("Wrox", x.NavigateToSingle("bk:Book/pub:Publisher").GetStringValue());
            Assert.AreEqual(SampleEnumCategory.Junior, x.NavigateToSingle("bk:Book/@category").GetEnumValue<SampleEnumCategory>());
            Assert.AreEqual(100, x.NavigateToSingle("bk:Book/bk:Pages").GetValue<int>());
            Assert.AreEqual(true, x.NavigateToSingle("bk:Book/@available").GetValue<bool>());
        }

        [Test]
        public void GetValueWithNavigateTest()
        {
            Assert.AreEqual("Wrox", x.GetStringValue("bk:Book/pub:Publisher"));
            Assert.AreEqual(SampleEnumCategory.Junior, x.GetEnumValue<SampleEnumCategory>("bk:Book/@category"));
            Assert.AreEqual(100, x.GetValue<int>("bk:Book/bk:Pages"));
            Assert.AreEqual(true, x.GetValue<bool>("bk:Book/@available"));
        }

        [Test]
        public void TestGetValueNotExist()
        {
            Assert.AreEqual(null, x.GetValue<int>("bk:Book/Pages"));
        }
    }
}
