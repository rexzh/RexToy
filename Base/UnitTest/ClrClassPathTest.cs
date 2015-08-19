using System;
using System.Collections.Generic;

using NUnit.Framework;

using RexToy;

namespace UnitTest
{
    [TestFixture]
    public class ClrClassPathTest
    {
        [Test]
        public void TestCreate()
        {
            ClrClassPath p = "UnitTest.Sample, Assembly = UnitTest.dll";
            Assert.AreEqual("UnitTest.dll", p.AssemblyName);
            Assert.AreEqual("UnitTest.Sample", p.ClassPath);
        }

        [Test]
        public void TestCombine()
        {
            ClrClassPath p = "UnitTest, Assembly = UnitTest.dll";
            p = p.Navigate("Sample");
            Assert.AreEqual("UnitTest.dll", p.AssemblyName);
            Assert.AreEqual("UnitTest.Sample", p.ClassPath);
        }

        [Test]
        public void TestMakeType()
        {
            ClrClassPath p = "UnitTest.Sample, Assembly = UnitTest.dll";
            Type t = p.MakeType("Person");
            Assert.NotNull(t);
        }
    }
}
