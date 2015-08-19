using System;
using System.Collections.Generic;
using NUnit.Framework;

using RexToy;
using RexToy.ExpressionLanguage;

namespace UnitTest.ExpressionLanguage
{
    [TestFixture]
    public class EvalSmartInvokeAdvanceTest
    {
        public ISmartInvoker CreateSmartInvoker()
        {
            Type t = Reflector.LoadType("RexToy.ExpressionLanguage.EvalSmartInvoker,RexToy.Core.Extension");
            IReflector r = Reflector.Bind(t, ReflectorPolicy.TypePublic);
            return (ISmartInvoker)r.Invoke("CreateInstance", new object[] { new SampleFuncOverload(), true });
        }

        [Test]
        public void TestCallV1()
        {
            ISmartInvoker _si = CreateSmartInvoker();
            object[] arg = new object[] { 1, 2 };
            object result = _si.Invoke("Sum", arg);
            Assert.AreEqual("v1", result);
        }

        [Test]
        public void TestCallV2()
        {
            ISmartInvoker _si = CreateSmartInvoker();
            object[] arg = new object[] { 1, 2, 3 };
            object result = _si.Invoke("Sum", arg);
            Assert.AreEqual("v2", result);
        }

        [Test]
        public void TestCallV3()
        {
            ISmartInvoker _si = CreateSmartInvoker();
            object[] arg = new object[] { 1, 2, 3, 4 };
            object result = _si.Invoke("Sum", arg);
            Assert.AreEqual("v3", result);
        }

        [Test, ExpectedException(typeof(ReflectorException))]
        public void TestCallAmbiguous()
        {
            ISmartInvoker _si = CreateSmartInvoker();
            object[] arg = new object[] { 1 };
            object result = _si.Invoke("Sum", arg);
        }
    }
}
