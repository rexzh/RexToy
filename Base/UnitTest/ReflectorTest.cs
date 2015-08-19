using System;
using System.Collections.Generic;
using NUnit.Framework;

using RexToy;

namespace UnitTest
{
    [TestFixture]
    public class ReflectorTest
    {
        [Test]
        public void TestAccessField_InstancePrivate()
        {
            ReflectSampleClass1 o = new ReflectSampleClass1();
            IReflector r = Reflector.Bind(o, ReflectorPolicy.InstanceAll);
            Type t = r.GetFieldType("_name");
            Assert.IsTrue(t == typeof(string));

            object val = r.GetFieldValue("_name");
            Assert.AreEqual("sample1", val);

            Assert.IsTrue(r.ExistField("_name"));
            Assert.IsFalse(r.ExistField("_Name"));

            Assert.IsFalse(r.ExistField("V"));
        }

        [Test]
        public void TestAccessField_InstancePublic()
        {
            ReflectSampleClass1 o = new ReflectSampleClass1();
            IReflector r = Reflector.Bind(o, ReflectorPolicy.InstancePublic);
            Type t = r.GetFieldType("X");
            Assert.IsTrue(t == typeof(int));

            object val = r.GetFieldValue("X");
            Assert.AreEqual(5, val);

            Assert.IsTrue(r.ExistField("X"));
            Assert.IsFalse(r.ExistField("x"));

            r.SetFieldValue("X", 10);
            Assert.AreEqual(10, o.X);
        }

        [Test]
        public void TestAccessField_Type()
        {
            IReflector r = Reflector.Bind(typeof(ReflectSampleClass1), ReflectorPolicy.TypeAll);
            Assert.IsTrue(r.ExistField("V"));
            Assert.IsFalse(r.ExistField("v"));
        }

        [Test]
        public void TestAccessProperty_InstancePrivate()
        {
            ReflectSampleClass1 o = new ReflectSampleClass1();
            IReflector r = Reflector.Bind(o, ReflectorPolicy.InstanceAllIgnoreCase);
            Type t = r.GetPropertyType("Name");
            Assert.IsTrue(t == typeof(string));

            object val = r.GetPropertyValue("Name");
            Assert.AreEqual("sample1", val);

            Assert.IsTrue(r.ExistProperty("name"));
            Assert.IsTrue(r.ExistProperty("Name"));

            Assert.IsFalse(r.ExistProperty("P"));
        }

        [Test]
        public void TestAccessProperty_InstancePublic()
        {
            ReflectSampleClass1 o = new ReflectSampleClass1();
            IReflector r = Reflector.Bind(o, ReflectorPolicy.InstancePublicIgnoreCase);
            Type t = r.GetPropertyType("count");
            Assert.IsTrue(t == typeof(int));

            object val = r.GetPropertyValue("Count");
            Assert.AreEqual(3, val);

            Assert.IsTrue(r.ExistProperty("count"));
            Assert.IsTrue(r.ExistProperty("Count"));

            r.SetPropertyValue("count", 10);
            Assert.AreEqual(10, o.Count);
        }

        [Test]
        public void TestAccessProperty_Type()
        {
            IReflector r = Reflector.Bind(typeof(ReflectSampleClass1), ReflectorPolicy.TypeAllIgnoreCase);
            Assert.IsTrue(r.ExistProperty("P"));
            Assert.IsTrue(r.ExistProperty("p"));
        }

        [Test]
        public void TestAccessIndexer()
        {
            IReflector r = Reflector.Bind(new ReflectSampleClass1(), ReflectorPolicy.InstancePublic);
            Assert.AreEqual(5, r.GetIndexerValue(new object[] { 5 }));

            r.SetIndexerValue(new object[] { 3 }, 4);

            var cs = r.FindAllConstructors();
        }

        [Test]
        public void TestInvoke()
        {
            IReflector r = Reflector.Bind(new ReflectSampleClass2());
            object result = r.Invoke("Sum", new object[] { 1, 3 });
            Assert.AreEqual(4, result);
        }

        [Test]
        public void TestInvokeRefParam()
        {
            IReflector r = Reflector.Bind(new ReflectSampleClass2());
            r.Invoke("Swap", new object[] { 3, 4 });
        }

        [Test]
        public void TestInvokeProvideOptParam()
        {
            IReflector r = Reflector.Bind(new ReflectSampleClass2());
            object result = r.Invoke("Mod", new object[] { 5, 3 });
            Assert.AreEqual(2, result);
        }

        [Test]
        public void TestInvokeOmitOptParam()
        {
            IReflector r = Reflector.Bind(new ReflectSampleClass2());
            object result = r.Invoke("Mod", new object[] { 5 });
            Assert.AreEqual(1, result);
        }

        [Test]
        public void TestInvokeVarargProvideNon()
        {
            IReflector r = Reflector.Bind(new ReflectSampleClass2());
            object result = r.Invoke("Acc", new object[] { 1, 2 });
            Assert.AreEqual(3, result);
        }

        [Test]
        public void TestInvokeVarargProvideOne()
        {
            IReflector r = Reflector.Bind(new ReflectSampleClass2());
            object result = r.Invoke("Acc", new object[] { 1, 2, 3 });
            Assert.AreEqual(6, result);
        }

        [Test]
        public void TestInvokeVarargProvideMany()
        {
            IReflector r = Reflector.Bind(new ReflectSampleClass2());
            object result = r.Invoke("Acc", new object[] { 1, 2, 3, 4, 5 });
            Assert.AreEqual(15, result);
        }
    }
}
