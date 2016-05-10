using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using NUnit.Framework;

using RexToy.Configuration;
using RexToy.ORM;

using UnitTest.ORM.SampleEntity.OleDb;

namespace UnitTest.ORM.IntegrateDB
{
    [TestFixture, Category("IntegrateDB")]
    public class OleDBTest
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
            using (ISession s = SessionFactory.OpenSession("std"))
            {
                var persons = s.FindBy<Person>();
            }
        }

        [Test]
        public void TestQuery()
        {
            AccessSchema a = new AccessSchema();
            var q = a.Person.Join(a.ContactInfo, (p, c) => p.ID == c.PersonID);
            using (ISession s = SessionFactory.OpenSession("std"))
            {
                s.Query(q.AsQuery());
            }
        }

        [Test]
        public void TestDatabase()
        {
            using (IDatabase db = DatabaseFactory.OpenDatabase("std"))
            {
                db.CreateTable<Order>();
                //db.TruncateTable<Order>();//Note:Access does not support this!
                db.DropTable<Order>();
            }
        }
    }
}
