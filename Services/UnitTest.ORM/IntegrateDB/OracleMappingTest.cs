using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using NUnit.Framework;

using RexToy.Configuration;
using RexToy.ORM;

using UnitTest.ORM.SampleEntity.Oracle;

namespace UnitTest.ORM
{
    [TestFixture, Category("IntegrateDB")]
    public class OracleTest
    {
        [SetUp]
        public void SetUp()
        {
            if (!AppConfig.Loaded)
                AppConfig.Load(ConfigFactory.CreateXmlConfig());
        }

        [Test]
        public void TestFindAll()
        {
            using (ISession s = SessionFactory.OpenSession("dest"))
            {
                var persons = s.FindBy<Person>();
            }
        }

        [Test]
        public void TestQuery()
        {
            OracleSchema ora = new OracleSchema();
            var q = ora.Person.Join(ora.ContactInfo, (p, c) => p.ID == c.PersonID)
                        .Where<Person>(p => p.Name != "R");
            using (ISession s = SessionFactory.OpenSession("dest"))
            {
                RowSet rs = s.Query(q);
                var p = rs[0].GetEntity<Person>();
            }
        }

        [Test]
        public void TestDatabase()
        {
            using (IDatabase db = DatabaseFactory.OpenDatabase("dest"))
            {
                db.CreateTable<Order>();
                db.TruncateTable<Order>();
                db.DropTable<Order>();
            }
        }
    }
}
