using System;
using System.Collections.Generic;

using NUnit.Framework;

using RexToy.Configuration;
using RexToy.ORM;

namespace UnitTest.ORM.IntegrateDB
{
    [TestFixture, Category("IntegrateDB")]
    public class MetaQueryTest
    {
        [SetUp]
        public void SetUp()
        {
            if (!AppConfig.Loaded)
                AppConfig.Load(ConfigFactory.CreateXmlConfig());
        }

        [Test]
        public void QueryAccess()
        {
            using (var db = DatabaseFactory.OpenDatabase("std"))
            {
                var m = db.QueryMeta();
                foreach (var t in m.Tables)
                {
                    Console.WriteLine(t.DbName);
                }
            }
        }

        [Test]
        public void QueryMSSQL()
        {
            using (var db = DatabaseFactory.OpenDatabase("source"))
            {
                var m = db.QueryMeta();
                foreach (var t in m.Tables)
                {
                    Console.WriteLine(t.DbName);
                }
            }
        }

        [Test]
        public void QueryOracle()
        {
            using (var db = DatabaseFactory.OpenDatabase("dest"))
            {
                var m = db.QueryMeta();
                foreach (var t in m.Tables)
                {
                    Console.WriteLine(t.DbName);
                }
            }
        }

        [Test]
        public void QueryMySQL()
        {
            using (var db = DatabaseFactory.OpenDatabase("my"))
            {
                var m = db.QueryMeta();
                foreach (var t in m.Tables)
                {
                    Console.WriteLine(t.DbName);
                }
            }
        }
    }
}
