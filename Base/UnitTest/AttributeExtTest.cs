using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;

using RexToy;

namespace UnitTest
{
    [TestFixture]
    public class AttributeExtTest
    {
        [Test]
        public void TestGetSingle()
        {
            MethodBase m = typeof(SampleClassForAttrTest).GetMethod("MethodSTA");
            STAThreadAttribute sta = m.GetSingleAttribute<STAThreadAttribute>();
            Assert.IsNotNull(sta);
        }

        [Test, ExpectedException(typeof(ReflectorException))]
        public void TestGetSingleError()
        {
            MethodBase m = typeof(SampleClassForAttrTest).GetMethod("MethodMultiConditional");
            ConditionalAttribute cdt = m.GetSingleAttribute<ConditionalAttribute>();
            Assert.IsNotNull(cdt);
        }

        [Test]
        public void TestGetSingleNull()
        {
            MethodBase m = typeof(SampleClassForAttrTest).GetMethod("Method");
            ConditionalAttribute sta = m.GetSingleAttribute<ConditionalAttribute>();
            Assert.IsNull(sta);
        }

        [Test]
        public void TestGetMulti()
        {
            MethodBase m = typeof(SampleClassForAttrTest).GetMethod("MethodMultiConditional");
            ConditionalAttribute[] cdts = m.GetAttributes<ConditionalAttribute>();
            Assert.AreEqual(2, cdts.Length);
        }
    }
}
