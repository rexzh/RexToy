using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using NUnit.Framework;

using RexToy.ORM;
using RexToy.Configuration;
using RexToy.ORM.Dialect;
using RexToy.ORM.Session;

using UnitTest.ORM.SampleEntity.MSSql;

namespace UnitTest.ORM
{
    [TestFixture]
    public class MSSQLMappingTest
    {
        private IMappingSQLEmit _emit;
        [SetUp]
        public void SetUp()
        {
            if (!AppConfig.Loaded)
                AppConfig.Load(ConfigFactory.CreateXmlConfig());
            _emit = CoreFactory.CreateDialectProvider("source").CreateMappingSQLEmit(CoreFactory.ObjectMapInfoCache);
        }

        [Test]
        public void TestFindByPK()
        {
            string sql = _emit.FindByPK(new Person() { ID = 3 });
            Assert.AreEqual("SELECT [ID], [Name], [Birth], [Gender] FROM [Person] WHERE [ID] = 3", sql);
        }

        [Test]
        public void TestFindByAll()
        {
            string sql = _emit.FindBy<Person>();
            Assert.AreEqual("SELECT [ID], [Name], [Birth], [Gender] FROM [Person]", sql);
        }

        [Test]
        public void TestFindByOrderAsc()
        {
            string sql = _emit.FindBy<Person>(p => p.Birth, 10);
            Assert.AreEqual("SELECT TOP 10 [ID], [Name], [Birth], [Gender] FROM [Person] ORDER BY [Birth] ASC", sql);
        }

        [Test]
        public void TestFindByOrderDesc()
        {
            string sql = _emit.FindBy<Person>(p => p.Birth, 5, OrderType.Desc);
            Assert.AreEqual("SELECT TOP 5 [ID], [Name], [Birth], [Gender] FROM [Person] ORDER BY [Birth] DESC", sql);
        }

        [Test]
        public void TestFindByOrderTop()
        {
            string sql = _emit.FindBy<Person>(p => p.True(), p => p.Birth, 10);
            Assert.AreEqual("SELECT TOP 10 [ID], [Name], [Birth], [Gender] FROM [Person] WHERE 1 = 1 ORDER BY [Birth] ASC", sql);
        }

        [Test]
        public void TestFindBySimple()
        {
            string sql = _emit.FindBy<Person>(p => p.Name == "R");
            Assert.AreEqual("SELECT [ID], [Name], [Birth], [Gender] FROM [Person] WHERE [Name] = 'R'", sql);
        }

        [Test]
        public void TestFindByEnum()
        {
            string sql = _emit.FindBy<ContactInfo>(c => c.PhoneType == SampleEntity.PhoneType.Mobile);
            Assert.AreEqual("SELECT [ID], [PersonID], [Phone], [PhoneType] FROM [ContactInfo] WHERE [PhoneType] = 2", sql);
        }

        [Test]
        public void TestFindBySimpleNot()
        {
            string sql = _emit.FindBy<Person>(p => p.Name != "R");
            Assert.AreEqual("SELECT [ID], [Name], [Birth], [Gender] FROM [Person] WHERE [Name] <> 'R'", sql);
        }

        [Test]
        public void TestFindByLocalVar()
        {
            DateTime t = DateTime.Parse("2000-1-1");
            string sql = _emit.FindBy<Person>(p => p.Birth == t);
            Assert.AreEqual("SELECT [ID], [Name], [Birth], [Gender] FROM [Person] WHERE [Birth] = CAST('2000-01-01 00:00:00' AS DateTime)", sql);
        }

        [Test]
        public void TestFindByComplex1()
        {
            string sql = _emit.FindBy<Person>(p => p.Birth > DateTime.Parse("2000-1-1") && p.Gender == true);
            Assert.AreEqual("SELECT [ID], [Name], [Birth], [Gender] FROM [Person] WHERE ([Birth] > CAST('2000-01-01 00:00:00' AS DateTime)) AND ([Gender] = 1)", sql);
        }

        [Test]
        public void TestFindByComplex2()
        {
            Expression<Func<Person, bool>> func = p => p.Birth > DateTime.Parse("2000-1-1");
            func = func.And(p => !(p.Gender == true));
            string sql = _emit.FindBy<Person>(func);
            Assert.AreEqual("SELECT [ID], [Name], [Birth], [Gender] FROM [Person] WHERE ([Birth] > CAST('2000-01-01 00:00:00' AS DateTime)) AND NOT ([Gender] = 1)", sql);
        }

        [Test]
        public void TestFindByNull()
        {
            string sql = _emit.FindBy<Person>(p => p.Name == null);
            Assert.AreEqual("SELECT [ID], [Name], [Birth], [Gender] FROM [Person] WHERE [Name] IS NULL", sql);
        }

        [Test]
        public void TestFindByNotNull()
        {
            string sql = _emit.FindBy<Person>(p => p.Name != null);
            Assert.AreEqual("SELECT [ID], [Name], [Birth], [Gender] FROM [Person] WHERE [Name] IS NOT NULL", sql);
        }

        [Test]
        public void TestFindByInFunction()
        {
            string sql = _emit.FindBy<Person>(p => p.Name.In("R", "P", "Q"));
            Assert.AreEqual("SELECT [ID], [Name], [Birth], [Gender] FROM [Person] WHERE [Name] IN ('R', 'P', 'Q')", sql);
        }

        [Test]
        public void TestFindByInEnumerableFunction()
        {
            List<string> list = new List<string>() { "P", "Q" };
            string sql = _emit.FindBy<Person>(p => !p.Name.In(list));
            Assert.AreEqual("SELECT [ID], [Name], [Birth], [Gender] FROM [Person] WHERE NOT ([Name] IN ('P', 'Q'))", sql);
        }

        [Test]
        public void TestFindByLikeFunction()
        {
            string sql = _emit.FindBy<Person>(p => p.Name.Like("R%"));
            Assert.AreEqual("SELECT [ID], [Name], [Birth], [Gender] FROM [Person] WHERE [Name] LIKE 'R%'", sql);
        }

        [Test]
        public void TestFindIdentity()
        {
            string sql = _emit.FindIdentity<Person>();
            Assert.AreEqual("SELECT Ident_Current('[Person]')", sql);
        }

        [Test]
        public void TestInsert()
        {
            Person p = new Person() { Name = "R", Birth = DateTime.Parse("2000-1-1"), Gender = false };
            string sql = _emit.Create<Person>(p);
            Assert.AreEqual("INSERT [Person]([Name], [Birth], [Gender]) VALUES('R', CAST('2000-01-01 00:00:00' AS DateTime), 0)", sql);
        }

        [Test]
        public void TestDeleteByPK()
        {
            Person pk = new Person() { ID = 10 };
            string sql = _emit.Remove<Person>(pk);
            Assert.AreEqual("DELETE [Person] WHERE [ID] = 10", sql);
        }

        [Test]
        public void TestDeleteBySimple()
        {
            string sql = _emit.RemoveBy<Person>(p => p.Gender == true);
            Assert.AreEqual("DELETE [Person] WHERE [Gender] = 1", sql);
        }

        [Test]
        public void TestDeleteByComplex()
        {
            string sql = _emit.RemoveBy<Person>(p => p.ID.In(36, 38, 40) || p.Name == null);
            Assert.AreEqual("DELETE [Person] WHERE ([ID] IN (36, 38, 40)) OR ([Name] IS NULL)", sql);
        }

        [Test]
        public void TestUpdate()
        {
            Person p = new Person() { ID = 10, Name = "R", Birth = new DateTime(2000, 1, 1), Gender = false };
            string sql = _emit.Update<Person>(p);
            Assert.AreEqual("UPDATE [Person] SET [Name] = 'R', [Birth] = CAST('2000-01-01 00:00:00' AS DateTime), [Gender] = 0 WHERE [ID] = 10", sql);
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
            Assert.AreEqual("SELECT [ID], [Name], [Birth], [Gender] FROM [Person] WHERE (1 = 1) AND ([Name] = 'R')", sql);
        }

        [Test]
        public void TestUnIdentical()
        {
            Expression<Func<Person, bool>> f = p => p.False();
            f = f.Or<Person>(p => p.Name == "R");

            string sql = _emit.FindBy<Person>(f);
            Assert.AreEqual("SELECT [ID], [Name], [Birth], [Gender] FROM [Person] WHERE (1 = 0) OR ([Name] = 'R')", sql);
        }
    }
}
