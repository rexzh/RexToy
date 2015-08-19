﻿using System;
using System.Collections.Generic;

using NUnit.Framework;

using RexToy.ORM;
using RexToy.Configuration;
using RexToy.ORM.Dialect;
using RexToy.ORM.Session;

using UnitTest.ORM.SampleEntity.OleDb;

namespace UnitTest.ORM
{
    [TestFixture]
    public class OleDbModelTest
    {
        private IModelSQLEmit _emit;
        [SetUp]
        public void SetUp()
        {
            if (!AppConfig.Loaded)
                AppConfig.Load(ConfigFactory.CreateXmlConfig());
            _emit = CoreFactory.CreateDialectProvider("std").CreateModelSQLEmit(CoreFactory.ObjectMapInfoCache);
        }

        [Test]
        public void TestCreateTableAutoSinglePK()
        {
            string sql = _emit.CreateTable<Person>();
            Assert.AreEqual("CREATE TABLE [Person]([ID] AutoIncrement NOT NULL, [Name] text(64) NOT NULL, [Birth] datetime NOT NULL, [Gender] bit NOT NULL, CONSTRAINT [PK_Person] PRIMARY KEY([ID]))", sql);
        }

        [Test]
        public void TestCreateTableCompositePK()
        {
            string sql = _emit.CreateTable<Order>();
            Assert.AreEqual("CREATE TABLE [Order]([CustomerID] int NOT NULL, [Number] text(64) NOT NULL, [Date] datetime NOT NULL, CONSTRAINT [PK_Order] PRIMARY KEY([CustomerID], [Number]))", sql);
        }
        
        [Test]
        public void TestCreateTableManualSinglePK()
        {
            string sql = _emit.CreateTable<Course>();
            Assert.AreEqual("CREATE TABLE [Course_Info]([Course_ID] text(64) NOT NULL, [Course_Name] text(64) NOT NULL, [UID] guid NOT NULL, CONSTRAINT [PK_Course_Info] PRIMARY KEY([Course_ID]))", sql);
        }

        [Test]
        public void TestTruncateTable()
        {
            string sql = _emit.TruncateTable<Person>();
            Assert.AreEqual("TRUNCATE TABLE [Person]", sql);
        }

        [Test]
        public void TestDropTable()
        {
            string sql = _emit.DropTable<Person>();
            Assert.AreEqual("DROP TABLE [Person]", sql);
        }
    }
}
