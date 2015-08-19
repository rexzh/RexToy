using System;
using System.Collections.Generic;

using NUnit.Framework;

using RexToy.Configuration;

using UnitTest.Sample;

namespace UnitTest.AOP.Service
{
    //Note:Test tx service
    [TestFixture]
    public class ServiceTest
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            AppConfig.Destroy();
            AppConfig.Load(ConfigFactory.CreateXmlConfig(@"config.xml"));
        }

        [Test]
        public void TestSimpleAuto()
        {
            ComponentA a = new ComponentA();
            var ti = a.MethodA();
            Assert.IsNotNull(ti);
        }

        [Test]
        public void TestNonTx()
        {
            ComponentA a = new ComponentA();
            var ti = a.MethodB();
            Assert.IsNull(ti);
        }

        [Test]
        public void TestCombine()
        {
            ComponentA a = new ComponentA();
            var ti = a.MethodC();
            Assert.IsNotNull(ti);
        }
    }
}
