using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using NUnit.Framework;

using RexToy.Configuration;
using RexToy.ORM;
using RexToy.ORM.MappingInfo;
using RexToy.ORM.Dialect;
using RexToy.ORM.QueryModel;
using RexToy.ORM.Session;

using UnitTest.ORM.SampleEntity;
using UnitTest.ORM.SampleEntity.MySQL;

namespace UnitTest.ORM
{
    [TestFixture]
    public class MySQLQueryTest
    {
        MSSqlSchema mssql = new MSSqlSchema();
        private IQuerySQLEmit _emit;
        [SetUp]
        public void SetUp()
        {
            if (!AppConfig.Loaded)
                AppConfig.Load(ConfigFactory.CreateXmlConfig());
            _emit = CoreFactory.CreateDialectProvider("my").CreateQuerySQLEmit(CoreFactory.ObjectMapInfoCache);
        }

        [Test]
        public void TestJoin()
        {
            var q = mssql.Person.As("p").Join(mssql.ContactInfo.As("c"), (p, c) => p.ID == c.PersonID)
                        .Where<Person>(p => p.Name == "R").And<ContactInfo>(c => c.PhoneType == PhoneType.Mobile)
                        .OrderBy<Person>(p => p.Name).ThenBy<ContactInfo>(c => c.PhoneType);

            string sql = _emit.Query(q);
            Assert.AreEqual("SELECT `p`.`ID` AS `pID`, `p`.`Name` AS `pName`, `p`.`Birth` AS `pBirth`, `p`.`Gender` AS `pGender`, `c`.`ID` AS `cID`, `c`.`PersonID` AS `cPersonID`, `c`.`Phone` AS `cPhone`, `c`.`PhoneType` AS `cPhoneType` FROM `Person` `p` JOIN `ContactInfo` `c` ON `p`.`ID` = `c`.`PersonID` WHERE (`p`.`Name` = 'R') AND (`c`.`PhoneType` = 2) ORDER BY `p`.`Name` ASC, `c`.`PhoneType` ASC", sql);
        }

        [Test]
        public void TestComplexJoin()
        {
            var q = mssql.CoursePerson.Join(mssql.Person, (cp, p) => cp.PersonID == p.ID)
                        .Join(mssql.Course, (cp, c) => cp.CourseID == c.ID)
                        .Where<Person, Course>((p, c) => p.Name == "R" && c.Name == "Math")
                        .OrderBy<Person>(p => p.Name).ThenBy<Course>(c => c.Name, OrderType.Desc);

            string sql = _emit.Query(q);
            Assert.AreEqual("SELECT `CoursePerson`.`ID` AS `CoursePersonID`, `CoursePerson`.`PersonID` AS `CoursePersonPersonID`, `CoursePerson`.`CourseID` AS `CoursePersonCourseID`, `Person`.`ID` AS `PersonID`, `Person`.`Name` AS `PersonName`, `Person`.`Birth` AS `PersonBirth`, `Person`.`Gender` AS `PersonGender`, `Course_Info`.`Course_ID` AS `Course_InfoCourse_ID`, `Course_Info`.`Course_Name` AS `Course_InfoCourse_Name`, `Course_Info`.`UID` AS `Course_InfoUID` FROM (`CoursePerson` JOIN `Person` ON `CoursePerson`.`PersonID` = `Person`.`ID`) JOIN `Course_Info` ON `CoursePerson`.`CourseID` = `Course_Info`.`Course_ID` WHERE (`Person`.`Name` = 'R') AND (`Course_Info`.`Course_Name` = 'Math') ORDER BY `Person`.`Name` ASC, `Course_Info`.`Course_Name` DESC", sql);
        }

        [Test]
        public void TestSelfJoin()
        {
            var q = mssql.Employee.As("m").Join(mssql.Employee.As("e"), (m, e) => m.ID == e.ManagerID)
                        .Where<Employee>(m => m.Name == "R");
            string sql = _emit.Query(q);
            Assert.AreEqual("SELECT `m`.`ID` AS `mID`, `m`.`Name` AS `mName`, `m`.`Birth` AS `mBirth`, `m`.`Gender` AS `mGender`, `m`.`ManagerID` AS `mManagerID`, `e`.`ID` AS `eID`, `e`.`Name` AS `eName`, `e`.`Birth` AS `eBirth`, `e`.`Gender` AS `eGender`, `e`.`ManagerID` AS `eManagerID` FROM `Employee` `m` JOIN `Employee` `e` ON `m`.`ID` = `e`.`ManagerID` WHERE `m`.`Name` = 'R'", sql);
        }

        [Test]
        public void TestSimpleView()
        {
            var q = mssql.Person.Join(mssql.ContactInfo, (p, c) => p.ID == c.PersonID);
            string sql = _emit.Query(q.AsQuery());
            Assert.AreEqual("SELECT `Person`.`ID` AS `PersonID`, `Person`.`Name` AS `PersonName`, `Person`.`Birth` AS `PersonBirth`, `Person`.`Gender` AS `PersonGender`, `ContactInfo`.`ID` AS `ContactInfoID`, `ContactInfo`.`PersonID` AS `ContactInfoPersonID`, `ContactInfo`.`Phone` AS `ContactInfoPhone`, `ContactInfo`.`PhoneType` AS `ContactInfoPhoneType` FROM `Person` JOIN `ContactInfo` ON `Person`.`ID` = `ContactInfo`.`PersonID`", sql);
        }

        [Test]
        public void TestLeftJoinView()
        {
            var v = mssql.Person.LeftJoin(mssql.ContactInfo, (p, c) => p.ID == c.PersonID);
            string sql = _emit.Query(v.AsQuery());
            Assert.AreEqual("SELECT `Person`.`ID` AS `PersonID`, `Person`.`Name` AS `PersonName`, `Person`.`Birth` AS `PersonBirth`, `Person`.`Gender` AS `PersonGender`, `ContactInfo`.`ID` AS `ContactInfoID`, `ContactInfo`.`PersonID` AS `ContactInfoPersonID`, `ContactInfo`.`Phone` AS `ContactInfoPhone`, `ContactInfo`.`PhoneType` AS `ContactInfoPhoneType` FROM `Person` LEFT JOIN `ContactInfo` ON `Person`.`ID` = `ContactInfo`.`PersonID`", sql);
        }

        [Test]
        public void TestRightJoinView()
        {
            var v = mssql.Person.RightJoin(mssql.ContactInfo, (p, c) => p.ID == c.PersonID);
            string sql = _emit.Query(v.AsQuery());
            Assert.AreEqual("SELECT `Person`.`ID` AS `PersonID`, `Person`.`Name` AS `PersonName`, `Person`.`Birth` AS `PersonBirth`, `Person`.`Gender` AS `PersonGender`, `ContactInfo`.`ID` AS `ContactInfoID`, `ContactInfo`.`PersonID` AS `ContactInfoPersonID`, `ContactInfo`.`Phone` AS `ContactInfoPhone`, `ContactInfo`.`PhoneType` AS `ContactInfoPhoneType` FROM `Person` RIGHT JOIN `ContactInfo` ON `Person`.`ID` = `ContactInfo`.`PersonID`", sql);
        }

        [Test]
        public void TestOuterJoinView()
        {
            var v = mssql.Person.OuterJoin(mssql.ContactInfo, (p, c) => p.ID == c.PersonID);
            string sql = _emit.Query(v.AsQuery());
            Assert.AreEqual("SELECT `Person`.`ID` AS `PersonID`, `Person`.`Name` AS `PersonName`, `Person`.`Birth` AS `PersonBirth`, `Person`.`Gender` AS `PersonGender`, `ContactInfo`.`ID` AS `ContactInfoID`, `ContactInfo`.`PersonID` AS `ContactInfoPersonID`, `ContactInfo`.`Phone` AS `ContactInfoPhone`, `ContactInfo`.`PhoneType` AS `ContactInfoPhoneType` FROM `Person` OUTER JOIN `ContactInfo` ON `Person`.`ID` = `ContactInfo`.`PersonID`", sql);
        }

        [Test]
        public void TestSimpleViewWithFilterOrder()
        {
            var q = mssql.Person.Join(mssql.ContactInfo, (p, c) => p.ID == c.PersonID)
                        .Where<Person>(p => p.Name == "R")
                        .OrderBy<Person>(p => p.ID);
            string sql = _emit.Query(q);
            Assert.AreEqual("SELECT `Person`.`ID` AS `PersonID`, `Person`.`Name` AS `PersonName`, `Person`.`Birth` AS `PersonBirth`, `Person`.`Gender` AS `PersonGender`, `ContactInfo`.`ID` AS `ContactInfoID`, `ContactInfo`.`PersonID` AS `ContactInfoPersonID`, `ContactInfo`.`Phone` AS `ContactInfoPhone`, `ContactInfo`.`PhoneType` AS `ContactInfoPhoneType` FROM `Person` JOIN `ContactInfo` ON `Person`.`ID` = `ContactInfo`.`PersonID` WHERE `Person`.`Name` = 'R' ORDER BY `Person`.`ID` ASC", sql);
        }

        [Test]
        public void TestSimpleViewAliasWithFilterOrder()
        {
            var q = mssql.Person.As("p").Join(mssql.ContactInfo.As("c"), (p, c) => p.ID == c.PersonID)
                        .Where<Person>(p => p.Name == "R").OrderBy<Person>(p => p.ID);
            string sql = _emit.Query(q);
            Assert.AreEqual("SELECT `p`.`ID` AS `pID`, `p`.`Name` AS `pName`, `p`.`Birth` AS `pBirth`, `p`.`Gender` AS `pGender`, `c`.`ID` AS `cID`, `c`.`PersonID` AS `cPersonID`, `c`.`Phone` AS `cPhone`, `c`.`PhoneType` AS `cPhoneType` FROM `Person` `p` JOIN `ContactInfo` `c` ON `p`.`ID` = `c`.`PersonID` WHERE `p`.`Name` = 'R' ORDER BY `p`.`ID` ASC", sql);
        }

        [Test]
        public void TestSimpleViewCount()
        {
            var q = mssql.Person.Join(mssql.ContactInfo, (p, c) => p.ID == c.PersonID);
            string sql = _emit.QueryCount(q.AsQuery());
            Assert.AreEqual("SELECT Count(0) FROM `Person` JOIN `ContactInfo` ON `Person`.`ID` = `ContactInfo`.`PersonID`", sql);
        }

        [Test]
        public void TestComplexView()
        {
            View v = mssql.CoursePerson.Join(mssql.Course, (cp, c) => cp.CourseID == c.ID)
                                        .Join(mssql.Person, (cp, p) => cp.PersonID == p.ID);
            string sql = _emit.Query(v.AsQuery());
            Assert.AreEqual("SELECT `CoursePerson`.`ID` AS `CoursePersonID`, `CoursePerson`.`PersonID` AS `CoursePersonPersonID`, `CoursePerson`.`CourseID` AS `CoursePersonCourseID`, `Course_Info`.`Course_ID` AS `Course_InfoCourse_ID`, `Course_Info`.`Course_Name` AS `Course_InfoCourse_Name`, `Course_Info`.`UID` AS `Course_InfoUID`, `Person`.`ID` AS `PersonID`, `Person`.`Name` AS `PersonName`, `Person`.`Birth` AS `PersonBirth`, `Person`.`Gender` AS `PersonGender` FROM (`CoursePerson` JOIN `Course_Info` ON `CoursePerson`.`CourseID` = `Course_Info`.`Course_ID`) JOIN `Person` ON `CoursePerson`.`PersonID` = `Person`.`ID`", sql);
        }

        [Test]
        public void TestComplexView2()
        {
            var v = mssql.Course.Join(mssql.CoursePerson
                                            .Join(mssql.Person, (cp, p) => cp.PersonID == p.ID),
                                            (c, cp) => c.ID == cp.CourseID);
            string sql = _emit.Query(v.AsQuery());
            Assert.AreEqual("SELECT `Course_Info`.`Course_ID` AS `Course_InfoCourse_ID`, `Course_Info`.`Course_Name` AS `Course_InfoCourse_Name`, `Course_Info`.`UID` AS `Course_InfoUID`, `CoursePerson`.`ID` AS `CoursePersonID`, `CoursePerson`.`PersonID` AS `CoursePersonPersonID`, `CoursePerson`.`CourseID` AS `CoursePersonCourseID`, `Person`.`ID` AS `PersonID`, `Person`.`Name` AS `PersonName`, `Person`.`Birth` AS `PersonBirth`, `Person`.`Gender` AS `PersonGender` FROM `Course_Info` JOIN (`CoursePerson` JOIN `Person` ON `CoursePerson`.`PersonID` = `Person`.`ID`) ON `Course_Info`.`Course_ID` = `CoursePerson`.`CourseID`", sql);
        }

        [Test]
        public void TestComplexViewWithFilterOrder()
        {
            var q = mssql.CoursePerson.Join(mssql.Course, (cp, c) => cp.CourseID == c.ID)
                                .Join(mssql.Person, (cp, p) => cp.PersonID == p.ID)
                                .Where<Course>(c => c.Name == "Math")
                                .OrderBy<Person>(p => p.Name);
            string sql = _emit.Query(q);
            Assert.AreEqual("SELECT `CoursePerson`.`ID` AS `CoursePersonID`, `CoursePerson`.`PersonID` AS `CoursePersonPersonID`, `CoursePerson`.`CourseID` AS `CoursePersonCourseID`, `Course_Info`.`Course_ID` AS `Course_InfoCourse_ID`, `Course_Info`.`Course_Name` AS `Course_InfoCourse_Name`, `Course_Info`.`UID` AS `Course_InfoUID`, `Person`.`ID` AS `PersonID`, `Person`.`Name` AS `PersonName`, `Person`.`Birth` AS `PersonBirth`, `Person`.`Gender` AS `PersonGender` FROM (`CoursePerson` JOIN `Course_Info` ON `CoursePerson`.`CourseID` = `Course_Info`.`Course_ID`) JOIN `Person` ON `CoursePerson`.`PersonID` = `Person`.`ID` WHERE `Course_Info`.`Course_Name` = 'Math' ORDER BY `Person`.`Name` ASC", sql);
        }

        [Test]
        public void TestComplexViewAliasWithFilterOrder()
        {
            var q = mssql.CoursePerson.As("cp")
                        .Join(mssql.Course.As("c"), (cp, c) => cp.CourseID == c.ID)
                        .Join(mssql.Person.As("p"), (cp, p) => cp.PersonID == p.ID)
                        .Where<Person>(p => p.Name.Like("R%"))
                        .OrderBy<Course>(c => c.ID);
            string sql = _emit.Query(q);
            Assert.AreEqual(@"SELECT `cp`.`ID` AS `cpID`, `cp`.`PersonID` AS `cpPersonID`, `cp`.`CourseID` AS `cpCourseID`, `c`.`Course_ID` AS `cCourse_ID`, `c`.`Course_Name` AS `cCourse_Name`, `c`.`UID` AS `cUID`, `p`.`ID` AS `pID`, `p`.`Name` AS `pName`, `p`.`Birth` AS `pBirth`, `p`.`Gender` AS `pGender` FROM (`CoursePerson` `cp` JOIN `Course_Info` `c` ON `cp`.`CourseID` = `c`.`Course_ID`) JOIN `Person` `p` ON `cp`.`PersonID` = `p`.`ID` WHERE `p`.`Name` LIKE 'R%' ORDER BY `c`.`Course_ID` ASC", sql);
        }

        [Test]
        public void TestJoinSameView()
        {
            var q = mssql.Lifecycle.As("lc")
                        .Join(mssql.Week.As("s"), (lc, s) => lc.StartWeekID == s.ID)
                        .Join(mssql.Week.As("e"), (lc, e) => lc.EndWeekID == e.ID)
                        .Where<Week>(s => s.StartTime > DateTime.Parse("2000-1-1"))
                        .OrderBy<Lifecycle>(lc => lc.Name).ThenBy<Week>(e => e.EndTime);

            string sql = _emit.Query(q);
            Assert.AreEqual("SELECT `lc`.`ID` AS `lcID`, `lc`.`Name` AS `lcName`, `lc`.`StartWeekID` AS `lcStartWeekID`, `lc`.`EndWeekID` AS `lcEndWeekID`, `s`.`ID` AS `sID`, `s`.`StartTime` AS `sStartTime`, `s`.`EndTime` AS `sEndTime`, `e`.`ID` AS `eID`, `e`.`StartTime` AS `eStartTime`, `e`.`EndTime` AS `eEndTime` FROM (`Lifecycle` `lc` JOIN `Week` `s` ON `lc`.`StartWeekID` = `s`.`ID`) JOIN `Week` `e` ON `lc`.`EndWeekID` = `e`.`ID` WHERE `s`.`StartTime` > CAST('2000-01-01 00:00:00' AS DateTime) ORDER BY `lc`.`Name` ASC, `e`.`EndTime` ASC", sql);
        }

        [Test]
        public void TestPagedQuery1stPage()
        {
            var q = mssql.Person.Join(mssql.ContactInfo, (p, c) => p.ID == c.PersonID)
                            .Where<Person>(p => p.Name.Like("R%"))
                            .OrderBy<Person>(p => p.ID);
            string sql = _emit.PagedQuery(q, 10, 1);
            Assert.AreEqual("SELECT TOP 10 `Person`.`ID` AS `PersonID`, `Person`.`Name` AS `PersonName`, `Person`.`Birth` AS `PersonBirth`, `Person`.`Gender` AS `PersonGender`, `ContactInfo`.`ID` AS `ContactInfoID`, `ContactInfo`.`PersonID` AS `ContactInfoPersonID`, `ContactInfo`.`Phone` AS `ContactInfoPhone`, `ContactInfo`.`PhoneType` AS `ContactInfoPhoneType` FROM `Person` JOIN `ContactInfo` ON `Person`.`ID` = `ContactInfo`.`PersonID` WHERE `Person`.`ID` NOT IN (SELECT TOP 0 `Person`.`ID` FROM `Person` JOIN `ContactInfo` ON `Person`.`ID` = `ContactInfo`.`PersonID` WHERE `Person`.`Name` LIKE 'R%' ORDER BY `Person`.`ID` ASC) AND `Person`.`Name` LIKE 'R%' ORDER BY `Person`.`ID` ASC", sql);
        }

        [Test]
        public void TestPagedQueryAlias2ndPage()
        {
            var q = mssql.Person.As("p").Join(mssql.ContactInfo.As("c"), (p, c) => p.ID == c.PersonID)
                            .Where<Person>(p => p.Name.Like("R%"))
                            .OrderBy<Person>(p => p.ID);

            string sql = _emit.PagedQuery(q, 10, 2);
            Assert.AreEqual("SELECT TOP 10 `p`.`ID` AS `pID`, `p`.`Name` AS `pName`, `p`.`Birth` AS `pBirth`, `p`.`Gender` AS `pGender`, `c`.`ID` AS `cID`, `c`.`PersonID` AS `cPersonID`, `c`.`Phone` AS `cPhone`, `c`.`PhoneType` AS `cPhoneType` FROM `Person` `p` JOIN `ContactInfo` `c` ON `p`.`ID` = `c`.`PersonID` WHERE `p`.`ID` NOT IN (SELECT TOP 10 `p`.`ID` FROM `Person` `p` JOIN `ContactInfo` `c` ON `p`.`ID` = `c`.`PersonID` WHERE `p`.`Name` LIKE 'R%' ORDER BY `p`.`ID` ASC) AND `p`.`Name` LIKE 'R%' ORDER BY `p`.`ID` ASC", sql);
        }

        [Test]
        public void TestIdentical()
        {
            var v = mssql.Person.Join(mssql.ContactInfo, (p, c) => p.ID == c.PersonID);
            var q = v.Where<Person>(p => p.True());

            q.And<ContactInfo>(c => c.PhoneType == PhoneType.Work);
            string sql = _emit.Query(q);
            Assert.AreEqual("SELECT `Person`.`ID` AS `PersonID`, `Person`.`Name` AS `PersonName`, `Person`.`Birth` AS `PersonBirth`, `Person`.`Gender` AS `PersonGender`, `ContactInfo`.`ID` AS `ContactInfoID`, `ContactInfo`.`PersonID` AS `ContactInfoPersonID`, `ContactInfo`.`Phone` AS `ContactInfoPhone`, `ContactInfo`.`PhoneType` AS `ContactInfoPhoneType` FROM `Person` JOIN `ContactInfo` ON `Person`.`ID` = `ContactInfo`.`PersonID` WHERE (1 = 1) AND (`ContactInfo`.`PhoneType` = 1)", sql);
        }

        [Test]
        public void TestUnIdentical()
        {
            var v = mssql.Person.Join(mssql.ContactInfo, (p, c) => p.ID == c.PersonID);
            var q = v.Where<Person>(p => p.False());

            q.Or<ContactInfo>(c => c.PhoneType == PhoneType.Work);
            string sql = _emit.Query(q);
            Assert.AreEqual("SELECT `Person`.`ID` AS `PersonID`, `Person`.`Name` AS `PersonName`, `Person`.`Birth` AS `PersonBirth`, `Person`.`Gender` AS `PersonGender`, `ContactInfo`.`ID` AS `ContactInfoID`, `ContactInfo`.`PersonID` AS `ContactInfoPersonID`, `ContactInfo`.`Phone` AS `ContactInfoPhone`, `ContactInfo`.`PhoneType` AS `ContactInfoPhoneType` FROM `Person` JOIN `ContactInfo` ON `Person`.`ID` = `ContactInfo`.`PersonID` WHERE (1 = 0) OR (`ContactInfo`.`PhoneType` = 1)", sql);
        }
    }
}
