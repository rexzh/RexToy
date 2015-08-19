using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using NUnit.Framework;

using RexToy.ORM;
using RexToy.Configuration;
using RexToy.ORM.MappingInfo;
using RexToy.ORM.Dialect;
using RexToy.ORM.Session;

using UnitTest.ORM.SampleEntity.Oracle;

namespace UnitTest.ORM
{
    [TestFixture]
    public class OracleMappingTest
    {
        private IMappingSQLEmit _emit;
        [SetUp]
        public void SetUp()
        {
            if (!AppConfig.Loaded)
                AppConfig.Load(ConfigFactory.CreateXmlConfig());
            _emit = CoreFactory.CreateDialectProvider("dest").CreateMappingSQLEmit(CoreFactory.ObjectMapInfoCache);
        }

        [Test]
        public void TestFindByPK()
        {
            string sql = _emit.FindByPK(new Person() { ID = 3 });
            Assert.AreEqual("SELECT \"ID\", \"NAME\", \"BIRTH\", \"GENDER\" FROM \"PERSON\" WHERE \"ID\" = 3", sql);
        }

        [Test]
        public void TestFindByAll()
        {
            string sql = _emit.FindBy<Person>();
            Assert.AreEqual("SELECT \"ID\", \"NAME\", \"BIRTH\", \"GENDER\" FROM \"PERSON\"", sql);
        }

        [Test]
        public void TestFindBySimple()
        {
            string sql = _emit.FindBy<Person>(p => p.Name == "R");
            Assert.AreEqual("SELECT \"ID\", \"NAME\", \"BIRTH\", \"GENDER\" FROM \"PERSON\" WHERE \"NAME\" = 'R'", sql);
        }

        [Test]
        public void TestFindBySimpleNot()
        {
            string sql = _emit.FindBy<Person>(p => p.Name != "R");
            Assert.AreEqual("SELECT \"ID\", \"NAME\", \"BIRTH\", \"GENDER\" FROM \"PERSON\" WHERE \"NAME\" <> 'R'", sql);
        }

        [Test]
        public void TestFindByLocalVar()
        {
            DateTime t = DateTime.Parse("2000-1-1");
            string sql = _emit.FindBy<Person>(p => p.Birth == t);
            Assert.AreEqual(string.Format("SELECT \"ID\", \"NAME\", \"BIRTH\", \"GENDER\" FROM \"PERSON\" WHERE \"BIRTH\" = TO_DATE('{0}', 'YYYY-MM-DD HH24:MI:SS')", t.ToString("yyyy-MM-dd HH:mm:ss")), sql);
        }

        [Test]
        public void TestFindByComplex1()
        {
            string sql = _emit.FindBy<Person>(p => p.Birth > DateTime.Parse("2000-1-1") && p.Gender == true);
            Assert.AreEqual(string.Format("SELECT \"ID\", \"NAME\", \"BIRTH\", \"GENDER\" FROM \"PERSON\" WHERE (\"BIRTH\" > TO_DATE('{0}', 'YYYY-MM-DD HH24:MI:SS')) AND (\"GENDER\" = 1)", DateTime.Parse("2000-1-1").ToString("yyyy-MM-dd HH:mm:ss")), sql);
        }

        [Test]
        public void TestFindByComplex2()
        {
            Expression<Func<Person, bool>> func = p => p.Birth > DateTime.Parse("2000-1-1");
            func = func.And(p => !(p.Gender == true));
            string sql = _emit.FindBy<Person>(func);
            Assert.AreEqual(string.Format("SELECT \"ID\", \"NAME\", \"BIRTH\", \"GENDER\" FROM \"PERSON\" WHERE (\"BIRTH\" > TO_DATE('{0}', 'YYYY-MM-DD HH24:MI:SS')) AND NOT (\"GENDER\" = 1)", DateTime.Parse("2000-1-1").ToString("yyyy-MM-dd HH:mm:ss")), sql);
        }

        [Test]
        public void TestFindByNull()
        {
            string sql = _emit.FindBy<Person>(p => p.Name == null);
            Assert.AreEqual("SELECT \"ID\", \"NAME\", \"BIRTH\", \"GENDER\" FROM \"PERSON\" WHERE \"NAME\" IS NULL", sql);
        }

        [Test]
        public void TestFindByNotNull()
        {
            string sql = _emit.FindBy<Person>(p => p.Name != null);
            Assert.AreEqual("SELECT \"ID\", \"NAME\", \"BIRTH\", \"GENDER\" FROM \"PERSON\" WHERE \"NAME\" IS NOT NULL", sql);
        }

        [Test]
        public void TestFindByInFunction()
        {
            string sql = _emit.FindBy<Person>(p => p.Name.In("R", "P", "Q"));
            Assert.AreEqual("SELECT \"ID\", \"NAME\", \"BIRTH\", \"GENDER\" FROM \"PERSON\" WHERE \"NAME\" IN ('R', 'P', 'Q')", sql);
        }

        [Test]
        public void TestFindByInEnumerableFunction()
        {
            List<string> list = new List<string>() { "P", "Q" };
            string sql = _emit.FindBy<Person>(p => !p.Name.In(list));
            Assert.AreEqual("SELECT \"ID\", \"NAME\", \"BIRTH\", \"GENDER\" FROM \"PERSON\" WHERE NOT (\"NAME\" IN ('P', 'Q'))", sql);
        }

        [Test]
        public void TestFindByLikeFunction()
        {
            string sql = _emit.FindBy<Person>(p => p.Name.Like("R%"));
            Assert.AreEqual("SELECT \"ID\", \"NAME\", \"BIRTH\", \"GENDER\" FROM \"PERSON\" WHERE \"NAME\" LIKE 'R%'", sql);
        }

        [Test]
        public void TestFindIdentity()
        {
            string sql = _emit.FindIdentity<Person>();
            Assert.AreEqual("SELECT SEQ_PERSON.CurrVal FROM Dual", sql);
        }

        [Test]
        public void TestInsert()
        {
            Person p = new Person() { Name = "R", Birth = DateTime.Parse("2000-1-1"), Gender = false };
            string sql = _emit.Create<Person>(p);
            Assert.AreEqual(string.Format("INSERT INTO \"PERSON\"(\"ID\", \"NAME\", \"BIRTH\", \"GENDER\") VALUES(SEQ_PERSON.NextVal, 'R', TO_DATE('{0}', 'YYYY-MM-DD HH24:MI:SS'), 0)", p.Birth.ToString("yyyy-MM-dd HH:mm:ss")), sql);
        }

        [Test]
        public void TestDeleteByPK()
        {
            Person pk = new Person() { ID = 10 };
            string sql = _emit.Remove<Person>(pk);
            Assert.AreEqual("DELETE \"PERSON\" WHERE \"ID\" = 10", sql);
        }

        [Test]
        public void TestDeleteBySimple()
        {
            string sql = _emit.RemoveBy<Person>(p => p.Gender == true);
            Assert.AreEqual("DELETE \"PERSON\" WHERE \"GENDER\" = 1", sql);
        }

        [Test]
        public void TestDeleteByComplex()
        {
            string sql = _emit.RemoveBy<Person>(p => p.ID.In(36, 38, 40) || p.Name == null);
            Assert.AreEqual("DELETE \"PERSON\" WHERE (\"ID\" IN (36, 38, 40)) OR (\"NAME\" IS NULL)", sql);
        }

        [Test]
        public void TestUpdate()
        {
            Person p = new Person() { ID = 10, Name = "R", Birth = new DateTime(2000, 1, 1), Gender = false };
            string sql = _emit.Update<Person>(p);
            Assert.AreEqual(string.Format("UPDATE \"PERSON\" SET \"NAME\" = 'R', \"BIRTH\" = TO_DATE('{0}', 'YYYY-MM-DD HH24:MI:SS'), \"GENDER\" = 0 WHERE \"ID\" = 10", p.Birth.ToString("yyyy-MM-dd HH:mm:ss")), sql);
        }

        [Test, ExpectedException(typeof(SQLGenerateException))]
        public void ErrorExample()
        {
            string sql = _emit.FindBy<Person>(p => p.Name.Length == 10);
        }

        [Test]
        public void TestIdentical()
        {
            Expression<Func<Person, bool>> f = p => p.True();
            f = f.And<Person>(p => p.Name == "R");

            string sql = _emit.FindBy<Person>(f);
            Assert.AreEqual("SELECT \"ID\", \"NAME\", \"BIRTH\", \"GENDER\" FROM \"PERSON\" WHERE (1 = 1) AND (\"NAME\" = 'R')", sql);
        }

        [Test]
        public void TestUnIdentical()
        {
            Expression<Func<Person, bool>> f = p => p.False();
            f = f.Or<Person>(p => p.Name == "R");

            string sql = _emit.FindBy<Person>(f);
            Assert.AreEqual("SELECT \"ID\", \"NAME\", \"BIRTH\", \"GENDER\" FROM \"PERSON\" WHERE (1 = 0) OR (\"NAME\" = 'R')", sql);
        }
    }
}
