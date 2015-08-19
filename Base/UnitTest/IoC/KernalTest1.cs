using System;
using System.Collections.Generic;
using System.Collections.Concurrent;

using NUnit.Framework;

using RexToy;
using RexToy.Configuration;
using RexToy.IoC;

using UnitTest.Sample;

namespace UnitTest.IoC
{
    [TestFixture]
    public class KernalTest1
    {
        IKernal _k;

        [TestFixtureSetUp]
        public void SetUp()
        {
            AppConfig.Destroy();
            AppConfig.Load(ConfigFactory.CreateXmlConfig(@".\config.xml"));
            _k = new Kernal("test1");
        }

        [Test]
        public void TestLookupInterfaceBuild()
        {
            ICalc c = _k.Lookup<ICalc>();
            Assert.AreEqual(10, c.Add(4, 6));
            _k.TearDown(c);
        }

        [Test]
        public void TestRecusiveBuild()
        {
            CalcMock c = _k.Lookup<CalcMock>("cMock");
            Assert.AreEqual("20", c.Add("7", "13"));
            _k.TearDown(c);
        }

        [Test]
        public void TestStateless()
        {
            ICalc c1 = _k.Lookup<ICalc>();
            ICalc c2 = _k.Lookup<ICalc>();

            Assert.AreNotEqual(c1, c2);

            _k.TearDown(c1);
            _k.TearDown(c2);
        }

        [Test]
        public void TestTeardown()
        {
            CalcMock c = _k.Lookup<CalcMock>("cMock");
            _k.TearDown(c);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            IReflector r = Reflector.Bind(_k, ReflectorPolicy.InstanceAll);
            object _c = r.GetFieldValue("_lifecycleContainer");
            ConcurrentDictionary<object, IObjectBuildContext> c = _c as ConcurrentDictionary<object, IObjectBuildContext>;
            Assert.AreEqual(0, c.Count);
        }
    }
}
