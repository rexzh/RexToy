using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using NUnit.Framework;

using RexToy.Configuration;
using RexToy.ORM;

using UnitTest.ORM.SampleEntity.MSSql;

namespace UnitTest.ORM
{
    [TestFixture, Category("IntegrateDB")]
    public class MSSQLTest
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
            using (ISession s = SessionFactory.OpenSession("source"))
            {
                var persons = s.FindBy<Person>();
            }
        }

        [Test]
        public void TestQuery()
        {
            MSSqlSchema ms = new MSSqlSchema();
            var q = ms.Person.Join(ms.ContactInfo, (p, c) => p.ID == c.PersonID)
                        .Where<Person>(p => p.Name != "R");
            using (ISession s = SessionFactory.OpenSession("source"))
            {
                RowSet rs = s.Query(q);
                var p = rs[0].GetEntity<Person>();
            }
        }

        [Test]
        public void TestDatabase()
        {
            using (IDatabase db = DatabaseFactory.OpenDatabase("source"))
            {
                db.CreateTable<Order>();
                db.TruncateTable<Order>();
                db.DropTable<Order>();
            }
        }
    }
}
