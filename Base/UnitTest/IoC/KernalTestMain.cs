using System;
using System.Collections.Generic;

using NUnit.Framework;

using RexToy.Configuration;
using RexToy.IoC;

using UnitTest.Sample;

namespace UnitTest.IoC
{
    [TestFixture]
    public class KernalTestMain
    {
        IKernal _k;

        [TestFixtureSetUp]
        public void SetUp()
        {
            AppConfig.Destroy();
            AppConfig.Load(ConfigFactory.CreateXmlConfig(@".\config.xml"));
            _k = new Kernal("main");
        }

        [Test]
        public void TestActivatorBuild()
        {
            ICalc c = _k.Lookup<ICalc>("c");
            Assert.AreEqual(30, c.Add(10, 20));
        }

        [Test]
        public void TestFactoryBuild()
        {
            ICalc c = _k.Lookup<ICalc>("sdCalc");
            Assert.AreEqual(16, (c as SingleDigitCalc).Mode);
            Assert.AreEqual(3, c.Add(10, 25));
        }

        [Test]
        public void TestRecursiveBuild()
        {
            CalcMock c = _k.Lookup<CalcMock>("cMock");
            Assert.AreEqual("3", c.Add("10", "25"));
        }

        [Test]
        public void TestDefaultCalc()
        {
            ICalc c = _k.Lookup<ICalc>("dCalc");
            Assert.AreEqual(10, c.Add(4, 3));
        }
    }
}
