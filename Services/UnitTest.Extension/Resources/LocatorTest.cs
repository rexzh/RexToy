using System;
using System.Collections.Generic;
using System.IO;

using NUnit.Framework;

using RexToy.Resources;

namespace UnitTest.Resources
{
    [TestFixture]
    public class LocatorTest
    {
        [Test]
        public void FSGetLogTest()
        {
            var loc = LocatorFactory.Create(@"file:///..\..\_ConfigFiles");
            using (Stream s = loc.GetStream("log.xml"))
            {
                Assert.IsNotNull(s);
            }
        }

        [Test]
        public void FSCombineTest()
        {
            var loc = LocatorFactory.Create(@"file:///..\..\");
            loc = loc.Combine("_ConfigFiles");
            using (var s = loc.GetStream("log.xml"))
            {
                Assert.IsNotNull(s);
            }
        }

        [Test]
        public void FSEnumTest()
        {
            List<string> list = new List<string>()
            {
                "log.xml","res.txt"
            };

            var loc = LocatorFactory.Create(@"file:///..\..\_ConfigFiles\");
            var items = loc.EnumItems();
            foreach (var s in items)
                Assert.IsTrue(list.Contains(s));
        }

        [Test]
        public void FSGetFileNotExist()
        {
            var loc = LocatorFactory.Create(@"file:///..\..\");
            using (Stream s = loc.GetStream("log.xml", false))
            {
                Assert.IsNull(s);
            }
        }

        [Test, ExpectedException(typeof(LocatorException))]
        public void FSGetFileNotExistThrow()
        {
            var loc = LocatorFactory.Create(@"file:///..\..\");
            using (Stream s = loc.GetStream("log.xml", true))
            {
                
            }
        }

        [Test]
        public void CLRGetResTest()
        {
            var loc = LocatorFactory.Create("clr-ns://UnitTest, Assembly = UnitTest.Extension");
            using (var s = loc.GetStream("Extension.res.txt"))
            {
                Assert.IsNotNull(s);
            }
        }

        [Test]
        public void CLRGetFileNotExist()
        {
            var loc = LocatorFactory.Create("clr-ns://UnitTest.Extension, Assembly = UnitTest.Extension");
            using (Stream s = loc.GetStream("log.xml", false))
            {
                Assert.IsNull(s);
            }
        }

        [Test, ExpectedException(typeof(LocatorException))]
        public void CLRGetFileNotExistThrow()
        {
            var loc = LocatorFactory.Create("clr-ns://UnitTest.Extension, Assembly = UnitTest.Extension");
            using (Stream s = loc.GetStream("log.xml", true))
            {
                
            }
        }

        [Test]
        public void CLRCombineTest()
        {
            var loc = LocatorFactory.Create("clr-ns://UnitTest, Assembly = UnitTest.Extension");
            loc = loc.Combine("Extension");
            using (var s = loc.GetStream("res.txt"))
            {
                Assert.IsNotNull(s);
            }
        }

        [Test]
        public void CLREnumTest()
        {
            var loc = LocatorFactory.Create("clr-ns://UnitTest,Assembly=UnitTest.Extension");
            var names = loc.EnumItems();

            var list = new List<string>()
            {
                "Extension.res.txt"
            };

            foreach (var name in names)
                Assert.IsTrue(list.Contains(name));
        }
    }
}
