using System;
using System.Collections.Generic;
using NUnit.Framework;

using RexToy;

namespace UnitTest
{
    [TestFixture]
    public class LoadTest
    {
        [Test]
        public void TestLoadType()
        {
            Type t = Type.GetType("UnitTest.SampleType1, UnitTest");
            Assert.IsNotNull(t);
        }

        [Test]
        public void TestLoadInstanceObject()
        {
            object o = Reflector.LoadInstance("UnitTest.SampleType1, UnitTest");
            Assert.IsNotNull(o);
        }

        [Test]
        public void TestLoadInstance()
        {
            SampleType1 st = Reflector.LoadInstance<SampleType1>("UnitTest.SampleType1, UnitTest");
            Assert.IsNotNull(st);
        }

        [Test, ExpectedException(typeof(ReflectorException))]
        public void TestThrowOnError1()
        {
            SampleType1 st = Reflector.LoadInstance<SampleType1>("UnitTest.SampleType1, UnitTest2");
        }

        [Test]
        public void TestThrowOnError2()
        {
            SampleType1 st = Reflector.LoadInstance<SampleType1>("UnitTest.SampleType1, UnitTest2", false);
        }

        [Test]
        public void TestLoadGenericType2()
        {
            Type[] argTypes = new Type[] { typeof(int), typeof(string) };
            Type t = Reflector.LoadGenericType("System.Collections.Generic.Dictionary", argTypes);
            Assert.AreEqual(typeof(Dictionary<int, string>), t);
        }

        [Test]
        public void TestLoadGenericType1()
        {
            Type[] argTypes = new Type[] { typeof(int) };
            Type t = Reflector.LoadGenericType("System.Collections.Generic.List", argTypes);
            Assert.AreEqual(typeof(List<int>), t);
        }

        [Test]
        public void TestLoadGenericInstance1()
        {
            Type[] argTypes = new Type[] { typeof(int) };
            object o = Reflector.LoadGenericInstance("System.Collections.Generic.List", argTypes);
            Assert.AreEqual(typeof(List<int>), o.GetType());
        }
    }
}
