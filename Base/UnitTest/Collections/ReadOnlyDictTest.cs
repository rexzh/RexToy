using System;
using System.Collections.Generic;
using NUnit.Framework;

using RexToy.Collections;

namespace UnitTest.Collections
{
    [TestFixture]
    public class ReadOnlyDictTest
    {
        [Test]
        public void TestImplicitCast()
        {
            Dictionary<int, string> d = new Dictionary<int, string>();
            d.Add(1, "One");
            d.Add(2, "Two");

            IReadOnlyDictionary<int, string> rd = d;
            Assert.AreEqual(2, rd.Count);
            Assert.IsTrue(rd.ContainsKey(1) && rd.ContainsKey(2));
            Assert.AreEqual("One", rd[1]);
        }

        [Test]
        public void TestExplicitCast()
        {
            Dictionary<int, string> orgd = new Dictionary<int, string>();
            orgd.Add(1, "One");
            orgd.Add(2, "Two");

            IReadOnlyDictionary<int, string> rd = orgd;

            Dictionary<int, string> d = (Dictionary<int, string>)rd;
            //Note:After convert to List<int>, it become mutable, and change will reflect to the readonly List.
            d.Add(3, "Three");
            Assert.AreEqual(3, rd.Count);
        }
    }
}
