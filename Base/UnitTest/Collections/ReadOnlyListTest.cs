using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

using RexToy.Collections;

namespace UnitTest.Collections
{
    [TestFixture]
    public class ReadOnlyListTest
    {
        [Test]
        public void TestImplicitCast()
        {
            List<int> l = new List<int> { 0, 1, 2, 3 };
            IReadOnlyList<int> rl = l;
            Assert.AreEqual(4, rl.Count);
            Assert.IsTrue(rl.Contains(0) && rl.Contains(1) && rl.Contains(2) && rl.Contains(3));
            Assert.AreEqual(0, rl[0]);
            Assert.AreEqual(2, rl[2]);
        }

        [Test]
        public void TestExplicitCast()
        {
            List<int> orgl = new List<int> { 0, 1, 2, 3 };
            IReadOnlyList<int> rl = orgl;

            List<int> l = (List<int>)rl;
            //Note:After convert to List<int>, it become mutable, and change will reflect to the readonly List.
            l.Add(4);
            Assert.AreEqual(5, rl.Count);
        }
    }
}
