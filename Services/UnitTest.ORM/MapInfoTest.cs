using System;
using System.Collections.Generic;

using NUnit.Framework;

using RexToy;
using RexToy.Configuration;
using RexToy.ORM.Configuration;
using RexToy.ORM.MappingInfo;

namespace UnitTest.ORM
{
    [TestFixture]
    public class MapInfoTest
    {
        IObjectMapInfoCache _cache;

        [TestFixtureSetUp]
        public void SetUp()
        {
            if (!AppConfig.Loaded)
                AppConfig.Load(ConfigFactory.CreateXmlConfig());

            _cache = new ObjectMapInfoCache();
            MappingInfoLoader.Load(_cache);
        }

        [Test]
        public void TestLoadFromXml()
        {
            var info = _cache.GetMapInfo(typeof(SampleEntity.MSSql.Person), true);
            Assert.IsNotNull(info);
        }

        [Test]
        public void TestLoadFromAttribute()
        {
            var info = _cache.GetMapInfo(typeof(SampleEntity.OleDb.Person), true);
            Assert.IsNotNull(info);
        }
    }
}
