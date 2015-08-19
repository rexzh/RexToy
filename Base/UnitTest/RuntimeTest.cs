using System;
using System.Collections.Generic;
using System.Reflection;

using NUnit.Framework;

using RexToy;

namespace UnitTest
{
    [TestFixture]
    public class RuntimeTest
    {
        [Test]
        public void TestThisAssembly()
        {
            var a = Assembly.GetExecutingAssembly();
            Runtime.SetStartupDirectory(a);

            Assert.AreEqual(System.Environment.CurrentDirectory, Runtime.StartupDirectory);
        }

        [Test]
        public void TestNullAssembly()
        {
            Runtime.SetStartupDirectory();

            Assert.AreEqual(System.Environment.CurrentDirectory, Runtime.StartupDirectory);
        }
    }
}
