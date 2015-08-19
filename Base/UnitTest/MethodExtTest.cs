using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;

using RexToy;

namespace UnitTest
{
    [TestFixture]
    public class MethodExtTest
    {
        [Test]
        public void TestIsVarArg()
        {
            MethodInfo m;
            m = typeof(SampleClassForMethodTest).GetMethod("Normal");
            Assert.IsFalse(m.IsVarArgs());
            m = typeof(SampleClassForMethodTest).GetMethod("VarArgs");
            Assert.IsTrue(m.IsVarArgs());
            m = typeof(SampleClassForMethodTest).GetMethod("OptionalArgs");
            Assert.IsFalse(m.IsVarArgs());
            m = typeof(SampleClassForMethodTest).GetMethod("OptAndVarArgs");
            Assert.IsTrue(m.IsVarArgs());
        }

        [Test]
        public void TestHasOptArg()
        {
            MethodInfo m;
            m = typeof(SampleClassForMethodTest).GetMethod("Normal");
            Assert.IsFalse(m.HasOptionalArgs());
            m = typeof(SampleClassForMethodTest).GetMethod("VarArgs");
            Assert.IsFalse(m.HasOptionalArgs());
            m = typeof(SampleClassForMethodTest).GetMethod("OptionalArgs");
            Assert.IsTrue(m.HasOptionalArgs());
            m = typeof(SampleClassForMethodTest).GetMethod("OptAndVarArgs");
            Assert.IsTrue(m.HasOptionalArgs());
        }

        [Test]
        public void TestMisc()
        {
            MethodInfo m;
            m = typeof(SampleClassForMethodTest).GetMethod("Normal");
            Assert.AreEqual(-1, m.FirstOptArgIndex());
            m = typeof(SampleClassForMethodTest).GetMethod("OptionalArgs");
            Assert.AreEqual(1, m.FirstOptArgIndex());

            Assert.IsTrue(m.IsOptArg(1));
            Assert.IsFalse(m.IsOptArg(0));

            m = typeof(SampleClassForMethodTest).GetMethod("VarArgs");
            Assert.AreEqual(typeof(object), m.GetVarArgElementType());
        }
    }
}
