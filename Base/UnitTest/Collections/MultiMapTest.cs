using System;
using System.Collections.Generic;

using NUnit.Framework;

using RexToy.Collections;

namespace UnitTest.Collections
{
    [TestFixture]
    public class MultiMapTest
    {
        MultiMap<string, int> _mmp;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _mmp = new MultiMap<string, int>();
            _mmp.Add("Odd", 1);
            _mmp.Add("Odd", 3);
            _mmp.Add("Eve", 4);
            _mmp.Add("Eve", 8);
            _mmp.Add("Eve", 6);
        }

        [Test]
        public void Test()
        {
            Assert.AreEqual(2, _mmp.KeyCount);
            Assert.AreEqual(5, _mmp.ValueCount);
        }

        [Test]
        public void Test2()
        {
            Assert.IsTrue(_mmp.ContainsKey("Odd"));
        }

        
    }
}
