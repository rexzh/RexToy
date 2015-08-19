using System;
using System.Collections.Generic;
using NUnit.Framework;

using RexToy;
using RexToy.ExpressionLanguage;

namespace UnitTest.ExpressionLanguage
{
    [TestFixture]
    public class EvalSmartInvokeTest
    {
        public ISmartInvoker CreateSmartInvoker()
        {
            Type t = Reflector.LoadType("RexToy.ExpressionLanguage.EvalSmartInvoker,RexToy.Core.Extension");
            IReflector r = Reflector.Bind(t, ReflectorPolicy.TypePublic);
            return (ISmartInvoker)r.Invoke("CreateInstance", new object[] { new SampleClassForMethodTest(), true });
        }

        [Test]
        public void TestNormal()
        {
            ISmartInvoker _si = CreateSmartInvoker();
            object[] args = new object[] { 1 };
            object result = _si.Invoke("Normal", args);
            Assert.AreEqual("1", result);
        }

        [Test, ExpectedException(typeof(ReflectorException))]
        public void TestNormalErr()
        {
            ISmartInvoker _si = CreateSmartInvoker();
            object[] args = new object[] { 1, 2 };
            object result = _si.Invoke("Normal", args);
            Assert.AreEqual("1", result);
        }

        [Test]
        public void TestOptArgProvideArg()
        {
            ISmartInvoker _si = CreateSmartInvoker();
            object[] args = new object[] { 1 };
            object result = _si.Invoke("OptionalArgs", args);
            Assert.AreEqual("2.1", result);
        }

        [Test]
        public void TestOptArgOmitArg()
        {
            ISmartInvoker _si = CreateSmartInvoker();
            object[] args = new object[] { 1, 2 };
            object result = _si.Invoke("OptionalArgs", args);
            Assert.AreEqual("2.2", result);
        }

        [Test]
        public void TestVarArgsNoArg()
        {
            ISmartInvoker _si = CreateSmartInvoker();
            object[] args = new object[] { 1 };
            object result = _si.Invoke("VarArgs", args);
            Assert.AreEqual("3.1", result);
        }

        [Test]
        public void TestVarArgsOneArg()
        {
            ISmartInvoker _si = CreateSmartInvoker();
            object[] args = new object[] { 1, 2 };
            object result = _si.Invoke("VarArgs", args);
            Assert.AreEqual("3.2", result);
        }

        [Test]
        public void TestVarArgsMoreArg()
        {
            ISmartInvoker _si = CreateSmartInvoker();
            object[] args = new object[] { 1, 2, 3, 4, 5 };
            object result = _si.Invoke("VarArgs", args);
            Assert.AreEqual("3.2", result);
        }

        [Test]
        public void TestOptVarArgsOmitAll()
        {
            ISmartInvoker _si = CreateSmartInvoker();
            object[] args = new object[] { 1 };
            object result = _si.Invoke("OptAndVarArgs", args);
            Assert.AreEqual("4.1", result);
        }

        [Test]
        public void TestOptVarArgsProvideOptNoVarArg()
        {
            ISmartInvoker _si = CreateSmartInvoker();
            object[] args = new object[] { 1, 2 };
            object result = _si.Invoke("OptAndVarArgs", args);
            Assert.AreEqual("4.2", result);
        }

        [Test]
        public void TestOptVarArgsProvideOptOneVarArg()
        {
            ISmartInvoker _si = CreateSmartInvoker();
            object[] args = new object[] { 1, 2, 3 };
            object result = _si.Invoke("OptAndVarArgs", args);
            Assert.AreEqual("4.3", result);
        }

        [Test]
        public void TestOptVarArgsProvideOptMoreVarArg()
        {
            ISmartInvoker _si = CreateSmartInvoker();
            object[] args = new object[] { 1, 2, 3, 4, 5 };
            object result = _si.Invoke("OptAndVarArgs", args);
            Assert.AreEqual("4.3", result);
        }
    }
}
