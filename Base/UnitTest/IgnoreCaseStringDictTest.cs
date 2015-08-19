using System;
using System.Collections.Generic;
using NUnit.Framework;

using RexToy.Collections;

namespace UnitTest
{
    [TestFixture]
    public class IgnoreCaseStringDictTest
    {
        StringKeyDictionary<int> _dict;
        public IgnoreCaseStringDictTest()
        {
            _dict = new StringKeyDictionary<int>(true);
            _dict.Add("One", 1);
            _dict.Add("Two", 2);
        }

        [Test]
        public void TestContainsKey()
        {
            Assert.IsTrue(_dict.ContainsKey("one"));
            Assert.IsTrue(_dict.ContainsKey("One"));
            Assert.IsTrue(_dict.ContainsKey("ONE"));

            Assert.IsFalse(_dict.ContainsKey("ten"));
        }

        [Test]
        public void TestAdd()
        {
            _dict.Add("three", 3);
        }

        [Test, ExpectedException(typeof(ArgumentException))]
        public void TestAddErr()
        {
            _dict.Add("ONE", 10);
        }

        [Test]
        public void TestGet()
        {
            Assert.AreEqual(2, _dict["TWO"]);
            int i;
            Assert.IsFalse(_dict.TryGetValue("Thousand", out i));
            Assert.IsTrue(_dict.TryGetValue("two", out i));
        }

        [Test, ExpectedException(typeof(KeyNotFoundException))]
        public void TestGetError()
        {
            int i = _dict["Million"];
        }

        [Test]
        public void TestSet()
        {
            _dict["four"] = 4;
            Assert.AreEqual(4,_dict["FOUR"]);
        }
    }
}
