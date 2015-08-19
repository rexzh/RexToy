using System;
using System.Collections.Generic;

using NUnit.Framework;

using RexToy;
using RexToy.Configuration;
using RexToy.AOP;

namespace UnitTest.AOP
{
    [TestFixture]
    public class ConfigTest
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            AppConfig.Destroy();
            AppConfig.Load(ConfigFactory.CreateXmlConfig(@".\config.xml"));
        }

        [Test]
        public void TestGetPath()
        {
            IAOPConfig cfg = AOPConfig.AOPConfiguration;
            Assert.AreEqual(Runtime.GetPath(@".\aop.xml"), cfg.LoadAOPInfoPath());
        }

        [Test]
        public void TestSinkFactory()
        {
            IAOPConfig cfg = AOPConfig.AOPConfiguration;
            Type t = cfg.LoadSinkFactory();
            Assert.AreEqual(typeof(RexToy.AOP.Services.ServiceSinkFactory), t);
        }
    }
}
