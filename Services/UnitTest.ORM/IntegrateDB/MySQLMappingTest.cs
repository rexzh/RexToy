using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using RexToy.Configuration;
using RexToy.ORM;

using UnitTest.ORM.SampleEntity.MySQL;

namespace UnitTest.ORM.IntegrateDB
{
    [TestFixture, Category("IntegrateDB")]
    public class MySQLTest
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
            using (ISession s = SessionFactory.OpenSession("my"))
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
            using (ISession s = SessionFactory.OpenSession("my"))
            {
                RowSet rs = s.Query(q);
                var p = rs[0].GetEntity<Person>();
            }
        }

        [Test]
        public void TestDatabase()
        {
            using (IDatabase db = DatabaseFactory.OpenDatabase("my"))
            {
                db.CreateTable<Order>();
                db.TruncateTable<Order>();
                db.DropTable<Order>();
            }
        }
    }
}