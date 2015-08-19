using System;
using System.Collections.Generic;

using NUnit.Framework;

using RexToy.Configuration;
using RexToy.IoC;

using UnitTest.Sample;

namespace UnitTest.IoC
{
    [TestFixture]
    public class KernalTest2
    {
        IKernal _k;

        [TestFixtureSetUp]
        public void SetUp()
        {
            AppConfig.Destroy();
            AppConfig.Load(ConfigFactory.CreateXmlConfig(@".\config.xml"));
            _k = new Kernal("test2");
        }

        [Test]
        public void TestOverloadMethodBuild()
        {
            ICalc c = _k.Lookup<ICalc>("sdCalc");
            Assert.AreEqual(3, c.Add(7, 6));
        }

        [Test]
        public void TestSingleton()
        {
            ICalc c1 = _k.Lookup<ICalc>("c");
            ICalc c2 = _k.Lookup<ICalc>("c");

            Assert.AreEqual(c1, c2);
        }

        [Test]
        public void TestDefaultCalc()
        {
            ICalc c = _k.Lookup<ICalc>("dCalc");
            Assert.AreEqual(8, c.Add(4, 3));
        }
    }
}
