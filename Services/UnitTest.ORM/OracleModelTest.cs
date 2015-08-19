using System;
using System.Collections.Generic;

using NUnit.Framework;

using RexToy.ORM;
using RexToy.Configuration;
using RexToy.ORM.Dialect;
using RexToy.ORM.Session;

using UnitTest.ORM.SampleEntity.Oracle;

namespace UnitTest.ORM
{
    [TestFixture]
    public class OracleModelTest
    {
        private IModelSQLEmit _emit;
        [SetUp]
        public void SetUp()
        {
            if (!AppConfig.Loaded)
                AppConfig.Load(ConfigFactory.CreateXmlConfig());
            _emit = CoreFactory.CreateDialectProvider("dest").CreateModelSQLEmit(CoreFactory.ObjectMapInfoCache);
        }

        [Test]
        public void TestCreateTableAutoSinglePK()
        {
            string sql = _emit.CreateTable<Person>();
            Assert.AreEqual("CREATE TABLE \"PERSON\"(\"ID\" integer NOT NULL, \"NAME\" varchar2(64) NOT NULL, \"BIRTH\" date NOT NULL, \"GENDER\" integer NOT NULL, CONSTRAINT \"PK_PERSON\" PRIMARY KEY(\"ID\"))", sql);
        }

        [Test]
        public void TestCreateTableCompositePK()
        {
            string sql = _emit.CreateTable<Order>();
            Assert.AreEqual("CREATE TABLE \"ORDER\"(\"CUSTOMERID\" integer NOT NULL, \"NUMBER\" varchar2(64) NOT NULL, \"DATE\" date NOT NULL, CONSTRAINT \"PK_ORDER\" PRIMARY KEY(\"CUSTOMERID\", \"NUMBER\"))", sql);
        }

        [Test]
        public void TestCreateTableManualSinglePK()
        {
            string sql = _emit.CreateTable<Course>();
            Assert.AreEqual("CREATE TABLE \"COURSE_INFO\"(\"COURSE_ID\" varchar2(64) NOT NULL, \"COURSE_NAME\" varchar2(64) NOT NULL, \"UID\" varchar2(64) NOT NULL, CONSTRAINT \"PK_COURSE_INFO\" PRIMARY KEY(\"COURSE_ID\"))", sql);

        }

        [Test]
        public void TestTruncateTable()
        {
            string sql = _emit.TruncateTable<Person>();
            Assert.AreEqual("TRUNCATE TABLE \"PERSON\"", sql);
        }

        [Test]
        public void TestDropTable()
        {
            string sql = _emit.DropTable<Person>();
            Assert.AreEqual("DROP TABLE \"PERSON\"", sql);
        }
    }
}
