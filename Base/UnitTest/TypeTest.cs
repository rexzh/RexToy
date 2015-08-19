using System;
using System.Collections.Generic;
using NUnit.Framework;

using RexToy;

namespace UnitTest
{
    [TestFixture]
    public class TypeTest
    {
        [Test]
        public void TestCtorInfo1()
        {
            Type t = typeof(SampleType1);
            Assert.AreEqual(true, t.HasDefaultConstructor());
            Assert.AreEqual(true, t.HasPublicConstructor());
        }

        [Test]
        public void TestCtorInfo2()
        {
            Type t = typeof(SampleType2);
            Assert.AreEqual(true, t.HasDefaultConstructor());
            Assert.AreEqual(false, t.HasPublicConstructor());
        }

        [Test]
        public void TestCtorInfo3()
        {
            Type t = typeof(SampleType3);
            Assert.AreEqual(false, t.HasDefaultConstructor());
            Assert.AreEqual(true, t.HasPublicConstructor());
        }

        [Test]
        public void TestClassRelation()
        {
            Assert.IsTrue(typeof(int).IsOrIsSubclassOf(typeof(object)));
            Assert.IsTrue(typeof(int).IsOrIsSubclassOf(typeof(int)));

            Assert.IsTrue(typeof(int).Implemented(typeof(IConvertible)));
        }

        [Test]
        public void TestEnumDefault()
        {
            object val = typeof(System.ConsoleColor).DefaultValue();
            Assert.AreEqual(System.ConsoleColor.Black, val);
        }

        [Test]
        public void TestTypeDefault()
        {
            object val = typeof(Sample.Person).DefaultValue();
            Assert.IsNull(val);
        }

        [Test]
        public void TestPrimitiveDefault()
        {
            object valInt = typeof(int).DefaultValue();
            Assert.AreEqual(0, valInt);

            object valStr = typeof(string).DefaultValue();
            Assert.IsNull(valStr);
        }

        [Test]
        public void TestStructDefault()
        {
            object val = typeof(Sample.SampleStruct).DefaultValue();
            Assert.IsNotNull(val);
            var s = (Sample.SampleStruct)val;
            Assert.AreEqual(0, s.X);
            Assert.AreEqual(0, s.Y);
        }
    }
}
