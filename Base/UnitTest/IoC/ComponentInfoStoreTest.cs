using System;
using System.Collections.Generic;

using NUnit.Framework;

using RexToy;
using RexToy.Configuration;
using RexToy.IoC;

using UnitTest.Sample;

namespace UnitTest.IoC
{
    [TestFixture]
    public class ComponentInfoStoreTest
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            AppConfig.Destroy();
            AppConfig.Load(ConfigFactory.CreateXmlConfig(@".\config.xml"));
        }

        [Test]
        public void TestReadConfig()
        {
            IKernalConfig cfg = KernalConfig.KernalConfiguration;
            string path_m = cfg.LoadComponentInfoPath("main");
            Assert.AreEqual(Runtime.GetPath(@".\k_main.xml"), path_m);
        }

        [Test]
        public void TestLoadKernalManifest()
        {
            IKernal k = new Kernal("main");
            Assert.AreEqual(true, k.ReadyToBuild("c"));
            Assert.AreEqual(true, k.ReadyToBuild("cMock"));
            Assert.AreEqual(true, k.ReadyToBuild("sdCalc"));
        }
    }
}
