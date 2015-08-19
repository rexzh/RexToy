using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using NUnit.Framework;

using RexToy.ORM;
using RexToy.Configuration;
using RexToy.ORM.MappingInfo;
using RexToy.ORM.Dialect;
using RexToy.ORM.QueryModel;
using RexToy.ORM.Session;

using UnitTest.ORM.SampleEntity;
using UnitTest.ORM.SampleEntity.Oracle;

namespace UnitTest.ORM
{
    [TestFixture]
    public class OracleQueryTest
    {
        OracleSchema ora = new OracleSchema();
        private IQuerySQLEmit _emit;
        [SetUp]
        public void SetUp()
        {
            if (!AppConfig.Loaded)
                AppConfig.Load(ConfigFactory.CreateXmlConfig());
            _emit = CoreFactory.CreateDialectProvider("dest").CreateQuerySQLEmit(CoreFactory.ObjectMapInfoCache);
        }

        [Test]
        public void TestJoin()
        {
            var q = ora.Person.As("p").Join(ora.ContactInfo.As("c"), (p, c) => p.ID == c.PersonID)
                        .Where<Person>(p => p.Name == "R").And<ContactInfo>(c => c.PhoneType == PhoneType.Mobile)
                        .OrderBy<Person>(p => p.Name).ThenBy<ContactInfo>(c => c.PhoneType);

            string sql = _emit.Query(q);
            Assert.AreEqual("SELECT \"P\".\"ID\" AS \"PID\", \"P\".\"NAME\" AS \"PNAME\", \"P\".\"BIRTH\" AS \"PBIRTH\", \"P\".\"GENDER\" AS \"PGENDER\", \"C\".\"ID\" AS \"CID\", \"C\".\"PERSONID\" AS \"CPERSONID\", \"C\".\"PHONE\" AS \"CPHONE\", \"C\".\"PHONETYPE\" AS \"CPHONETYPE\" FROM \"PERSON\" \"P\" JOIN \"CONTACTINFO\" \"C\" ON \"P\".\"ID\" = \"C\".\"PERSONID\" WHERE (\"P\".\"NAME\" = 'R') AND (\"C\".\"PHONETYPE\" = 2) ORDER BY \"P\".\"NAME\" ASC, \"C\".\"PHONETYPE\" ASC", sql);
        }

        [Test]
        public void TestComplexJoin()
        {
            var q = ora.CoursePerson.Join(ora.Person, (cp, p) => cp.PersonID == p.ID)
                        .Join(ora.Course, (cp, c) => cp.CourseID == c.ID)
                        .Where<Person, Course>((p, c) => p.Name == "R" && c.Name == "Math")
                        .OrderBy<Person>(p => p.Name).ThenBy<Course>(c => c.Name, OrderType.Desc);

            string sql = _emit.Query(q);
            Assert.AreEqual("SELECT \"COURSEPERSON\".\"ID\" AS \"COURSEPERSONID\", \"COURSEPERSON\".\"PERSONID\" AS \"COURSEPERSONPERSONID\", \"COURSEPERSON\".\"COURSEID\" AS \"COURSEPERSONCOURSEID\", \"PERSON\".\"ID\" AS \"PERSONID\", \"PERSON\".\"NAME\" AS \"PERSONNAME\", \"PERSON\".\"BIRTH\" AS \"PERSONBIRTH\", \"PERSON\".\"GENDER\" AS \"PERSONGENDER\", \"COURSE_INFO\".\"COURSE_ID\" AS \"COURSE_INFOCOURSE_ID\", \"COURSE_INFO\".\"COURSE_NAME\" AS \"COURSE_INFOCOURSE_NAME\", \"COURSE_INFO\".\"UID\" AS \"COURSE_INFOUID\" FROM (\"COURSEPERSON\" JOIN \"PERSON\" ON \"COURSEPERSON\".\"PERSONID\" = \"PERSON\".\"ID\") JOIN \"COURSE_INFO\" ON \"COURSEPERSON\".\"COURSEID\" = \"COURSE_INFO\".\"COURSE_ID\" WHERE (\"PERSON\".\"NAME\" = 'R') AND (\"COURSE_INFO\".\"COURSE_NAME\" = 'Math') ORDER BY \"PERSON\".\"NAME\" ASC, \"COURSE_INFO\".\"COURSE_NAME\" DESC", sql);
        }

        [Test]
        public void TestSelfJoin()
        {
            var q = ora.Employee.As("m").Join(ora.Employee.As("e"), (m, e) => m.ID == e.ManagerID)
                        .Where<Employee>(m => m.Name == "R");
            string sql = _emit.Query(q);
            Assert.AreEqual("SELECT \"M\".\"ID\" AS \"MID\", \"M\".\"NAME\" AS \"MNAME\", \"M\".\"BIRTH\" AS \"MBIRTH\", \"M\".\"GENDER\" AS \"MGENDER\", \"M\".\"MANAGERID\" AS \"MMANAGERID\", \"E\".\"ID\" AS \"EID\", \"E\".\"NAME\" AS \"ENAME\", \"E\".\"BIRTH\" AS \"EBIRTH\", \"E\".\"GENDER\" AS \"EGENDER\", \"E\".\"MANAGERID\" AS \"EMANAGERID\" FROM \"EMPLOYEE\" \"M\" JOIN \"EMPLOYEE\" \"E\" ON \"M\".\"ID\" = \"E\".\"MANAGERID\" WHERE \"M\".\"NAME\" = 'R'", sql);
        }

        [Test]
        public void TestSimpleView()
        {
            var q = ora.Person.Join(ora.ContactInfo, (p, c) => p.ID == c.PersonID);
            string sql = _emit.Query(q.AsQuery());
            Assert.AreEqual("SELECT \"PERSON\".\"ID\" AS \"PERSONID\", \"PERSON\".\"NAME\" AS \"PERSONNAME\", \"PERSON\".\"BIRTH\" AS \"PERSONBIRTH\", \"PERSON\".\"GENDER\" AS \"PERSONGENDER\", \"CONTACTINFO\".\"ID\" AS \"CONTACTINFOID\", \"CONTACTINFO\".\"PERSONID\" AS \"CONTACTINFOPERSONID\", \"CONTACTINFO\".\"PHONE\" AS \"CONTACTINFOPHONE\", \"CONTACTINFO\".\"PHONETYPE\" AS \"CONTACTINFOPHONETYPE\" FROM \"PERSON\" JOIN \"CONTACTINFO\" ON \"PERSON\".\"ID\" = \"CONTACTINFO\".\"PERSONID\"", sql);
        }

        [Test]
        public void TestLeftJoinView()
        {
            var v = ora.Person.LeftJoin(ora.ContactInfo, (p, c) => p.ID == c.PersonID);
            string sql = _emit.Query(v.AsQuery());
            Assert.AreEqual("SELECT \"PERSON\".\"ID\" AS \"PERSONID\", \"PERSON\".\"NAME\" AS \"PERSONNAME\", \"PERSON\".\"BIRTH\" AS \"PERSONBIRTH\", \"PERSON\".\"GENDER\" AS \"PERSONGENDER\", \"CONTACTINFO\".\"ID\" AS \"CONTACTINFOID\", \"CONTACTINFO\".\"PERSONID\" AS \"CONTACTINFOPERSONID\", \"CONTACTINFO\".\"PHONE\" AS \"CONTACTINFOPHONE\", \"CONTACTINFO\".\"PHONETYPE\" AS \"CONTACTINFOPHONETYPE\" FROM \"PERSON\" LEFT JOIN \"CONTACTINFO\" ON \"PERSON\".\"ID\" = \"CONTACTINFO\".\"PERSONID\"", sql);
        }

        [Test]
        public void TestRightJoinView()
        {
            var v = ora.Person.RightJoin(ora.ContactInfo, (p, c) => p.ID == c.PersonID);
            string sql = _emit.Query(v.AsQuery());
            Assert.AreEqual("SELECT \"PERSON\".\"ID\" AS \"PERSONID\", \"PERSON\".\"NAME\" AS \"PERSONNAME\", \"PERSON\".\"BIRTH\" AS \"PERSONBIRTH\", \"PERSON\".\"GENDER\" AS \"PERSONGENDER\", \"CONTACTINFO\".\"ID\" AS \"CONTACTINFOID\", \"CONTACTINFO\".\"PERSONID\" AS \"CONTACTINFOPERSONID\", \"CONTACTINFO\".\"PHONE\" AS \"CONTACTINFOPHONE\", \"CONTACTINFO\".\"PHONETYPE\" AS \"CONTACTINFOPHONETYPE\" FROM \"PERSON\" RIGHT JOIN \"CONTACTINFO\" ON \"PERSON\".\"ID\" = \"CONTACTINFO\".\"PERSONID\"", sql);
        }

        [Test]
        public void TestOuterJoinView()
        {
            var v = ora.Person.OuterJoin(ora.ContactInfo, (p, c) => p.ID == c.PersonID);
            string sql = _emit.Query(v.AsQuery());
            Assert.AreEqual("SELECT \"PERSON\".\"ID\" AS \"PERSONID\", \"PERSON\".\"NAME\" AS \"PERSONNAME\", \"PERSON\".\"BIRTH\" AS \"PERSONBIRTH\", \"PERSON\".\"GENDER\" AS \"PERSONGENDER\", \"CONTACTINFO\".\"ID\" AS \"CONTACTINFOID\", \"CONTACTINFO\".\"PERSONID\" AS \"CONTACTINFOPERSONID\", \"CONTACTINFO\".\"PHONE\" AS \"CONTACTINFOPHONE\", \"CONTACTINFO\".\"PHONETYPE\" AS \"CONTACTINFOPHONETYPE\" FROM \"PERSON\" OUTER JOIN \"CONTACTINFO\" ON \"PERSON\".\"ID\" = \"CONTACTINFO\".\"PERSONID\"", sql);
        }

        [Test]
        public void TestSimpleViewWithFilterOrder()
        {
            var q = ora.Person.Join(ora.ContactInfo, (p, c) => p.ID == c.PersonID)
                        .Where<Person>(p => p.Name == "R")
                        .OrderBy<Person>(p => p.ID);
            string sql = _emit.Query(q);
            Assert.AreEqual("SELECT \"PERSON\".\"ID\" AS \"PERSONID\", \"PERSON\".\"NAME\" AS \"PERSONNAME\", \"PERSON\".\"BIRTH\" AS \"PERSONBIRTH\", \"PERSON\".\"GENDER\" AS \"PERSONGENDER\", \"CONTACTINFO\".\"ID\" AS \"CONTACTINFOID\", \"CONTACTINFO\".\"PERSONID\" AS \"CONTACTINFOPERSONID\", \"CONTACTINFO\".\"PHONE\" AS \"CONTACTINFOPHONE\", \"CONTACTINFO\".\"PHONETYPE\" AS \"CONTACTINFOPHONETYPE\" FROM \"PERSON\" JOIN \"CONTACTINFO\" ON \"PERSON\".\"ID\" = \"CONTACTINFO\".\"PERSONID\" WHERE \"PERSON\".\"NAME\" = 'R' ORDER BY \"PERSON\".\"ID\" ASC", sql);
        }

        [Test]
        public void TestSimpleViewAliasWithFilterOrder()
        {
            var q = ora.Person.As("p").Join(ora.ContactInfo.As("c"), (p, c) => p.ID == c.PersonID)
                        .Where<Person>(p => p.Name == "R").OrderBy<Person>(p => p.ID);
            string sql = _emit.Query(q);
            Assert.AreEqual("SELECT \"P\".\"ID\" AS \"PID\", \"P\".\"NAME\" AS \"PNAME\", \"P\".\"BIRTH\" AS \"PBIRTH\", \"P\".\"GENDER\" AS \"PGENDER\", \"C\".\"ID\" AS \"CID\", \"C\".\"PERSONID\" AS \"CPERSONID\", \"C\".\"PHONE\" AS \"CPHONE\", \"C\".\"PHONETYPE\" AS \"CPHONETYPE\" FROM \"PERSON\" \"P\" JOIN \"CONTACTINFO\" \"C\" ON \"P\".\"ID\" = \"C\".\"PERSONID\" WHERE \"P\".\"NAME\" = 'R' ORDER BY \"P\".\"ID\" ASC", sql);
        }

        [Test]
        public void TestSimpleViewCount()
        {
            var q = ora.Person.Join(ora.ContactInfo, (p, c) => p.ID == c.PersonID);
            string sql = _emit.QueryCount(q.AsQuery());
            Assert.AreEqual("SELECT Count(0) FROM \"PERSON\" JOIN \"CONTACTINFO\" ON \"PERSON\".\"ID\" = \"CONTACTINFO\".\"PERSONID\"", sql);
        }

        [Test]
        public void TestComplexView()
        {
            View v = ora.CoursePerson.Join(ora.Course, (cp, c) => cp.CourseID == c.ID)
                                        .Join(ora.Person, (cp, p) => cp.PersonID == p.ID);
            string sql = _emit.Query(v.AsQuery());
            Assert.AreEqual("SELECT \"COURSEPERSON\".\"ID\" AS \"COURSEPERSONID\", \"COURSEPERSON\".\"PERSONID\" AS \"COURSEPERSONPERSONID\", \"COURSEPERSON\".\"COURSEID\" AS \"COURSEPERSONCOURSEID\", \"COURSE_INFO\".\"COURSE_ID\" AS \"COURSE_INFOCOURSE_ID\", \"COURSE_INFO\".\"COURSE_NAME\" AS \"COURSE_INFOCOURSE_NAME\", \"COURSE_INFO\".\"UID\" AS \"COURSE_INFOUID\", \"PERSON\".\"ID\" AS \"PERSONID\", \"PERSON\".\"NAME\" AS \"PERSONNAME\", \"PERSON\".\"BIRTH\" AS \"PERSONBIRTH\", \"PERSON\".\"GENDER\" AS \"PERSONGENDER\" FROM (\"COURSEPERSON\" JOIN \"COURSE_INFO\" ON \"COURSEPERSON\".\"COURSEID\" = \"COURSE_INFO\".\"COURSE_ID\") JOIN \"PERSON\" ON \"COURSEPERSON\".\"PERSONID\" = \"PERSON\".\"ID\"", sql);
        }

        [Test]
        public void TestComplexView2()
        {
            var v = ora.Course.Join(ora.CoursePerson
                                            .Join(ora.Person, (cp, p) => cp.PersonID == p.ID),
                                            (c, cp) => c.ID == cp.CourseID);
            string sql = _emit.Query(v.AsQuery());
            Assert.AreEqual("SELECT \"COURSE_INFO\".\"COURSE_ID\" AS \"COURSE_INFOCOURSE_ID\", \"COURSE_INFO\".\"COURSE_NAME\" AS \"COURSE_INFOCOURSE_NAME\", \"COURSE_INFO\".\"UID\" AS \"COURSE_INFOUID\", \"COURSEPERSON\".\"ID\" AS \"COURSEPERSONID\", \"COURSEPERSON\".\"PERSONID\" AS \"COURSEPERSONPERSONID\", \"COURSEPERSON\".\"COURSEID\" AS \"COURSEPERSONCOURSEID\", \"PERSON\".\"ID\" AS \"PERSONID\", \"PERSON\".\"NAME\" AS \"PERSONNAME\", \"PERSON\".\"BIRTH\" AS \"PERSONBIRTH\", \"PERSON\".\"GENDER\" AS \"PERSONGENDER\" FROM \"COURSE_INFO\" JOIN (\"COURSEPERSON\" JOIN \"PERSON\" ON \"COURSEPERSON\".\"PERSONID\" = \"PERSON\".\"ID\") ON \"COURSE_INFO\".\"COURSE_ID\" = \"COURSEPERSON\".\"COURSEID\"", sql);
        }

        [Test]
        public void TestComplexViewWithFilterOrder()
        {
            var q = ora.CoursePerson.Join(ora.Course, (cp, c) => cp.CourseID == c.ID)
                                .Join(ora.Person, (cp, p) => cp.PersonID == p.ID)
                                .Where<Course>(c => c.Name == "Math")
                                .OrderBy<Person>(p => p.Name);
            string sql = _emit.Query(q);
            Assert.AreEqual("SELECT \"COURSEPERSON\".\"ID\" AS \"COURSEPERSONID\", \"COURSEPERSON\".\"PERSONID\" AS \"COURSEPERSONPERSONID\", \"COURSEPERSON\".\"COURSEID\" AS \"COURSEPERSONCOURSEID\", \"COURSE_INFO\".\"COURSE_ID\" AS \"COURSE_INFOCOURSE_ID\", \"COURSE_INFO\".\"COURSE_NAME\" AS \"COURSE_INFOCOURSE_NAME\", \"COURSE_INFO\".\"UID\" AS \"COURSE_INFOUID\", \"PERSON\".\"ID\" AS \"PERSONID\", \"PERSON\".\"NAME\" AS \"PERSONNAME\", \"PERSON\".\"BIRTH\" AS \"PERSONBIRTH\", \"PERSON\".\"GENDER\" AS \"PERSONGENDER\" FROM (\"COURSEPERSON\" JOIN \"COURSE_INFO\" ON \"COURSEPERSON\".\"COURSEID\" = \"COURSE_INFO\".\"COURSE_ID\") JOIN \"PERSON\" ON \"COURSEPERSON\".\"PERSONID\" = \"PERSON\".\"ID\" WHERE \"COURSE_INFO\".\"COURSE_NAME\" = 'Math' ORDER BY \"PERSON\".\"NAME\" ASC", sql);
        }

        [Test]
        public void TestComplexViewAliasWithFilterOrder()
        {
            var q = ora.CoursePerson.As("cp")
                        .Join(ora.Course.As("c"), (cp, c) => cp.CourseID == c.ID)
                        .Join(ora.Person.As("p"), (cp, p) => cp.PersonID == p.ID)
                        .Where<Person>(p => p.Name.Like("R%"))
                        .OrderBy<Course>(c => c.ID);
            string sql = _emit.Query(q);
            Assert.AreEqual("SELECT \"CP\".\"ID\" AS \"CPID\", \"CP\".\"PERSONID\" AS \"CPPERSONID\", \"CP\".\"COURSEID\" AS \"CPCOURSEID\", \"C\".\"COURSE_ID\" AS \"CCOURSE_ID\", \"C\".\"COURSE_NAME\" AS \"CCOURSE_NAME\", \"C\".\"UID\" AS \"CUID\", \"P\".\"ID\" AS \"PID\", \"P\".\"NAME\" AS \"PNAME\", \"P\".\"BIRTH\" AS \"PBIRTH\", \"P\".\"GENDER\" AS \"PGENDER\" FROM (\"COURSEPERSON\" \"CP\" JOIN \"COURSE_INFO\" \"C\" ON \"CP\".\"COURSEID\" = \"C\".\"COURSE_ID\") JOIN \"PERSON\" \"P\" ON \"CP\".\"PERSONID\" = \"P\".\"ID\" WHERE \"P\".\"NAME\" LIKE 'R%' ORDER BY \"C\".\"COURSE_ID\" ASC", sql);
        }

        [Test]
        public void TestJoinSameView()
        {
            var q = ora.Lifecycle.As("lc")
                        .Join(ora.Week.As("s"), (lc, s) => lc.StartWeekID == s.ID)
                        .Join(ora.Week.As("e"), (lc, e) => lc.EndWeekID == e.ID)
                        .Where<Week>(s => s.StartTime > DateTime.Parse("2000-1-1"))
                        .OrderBy<Lifecycle>(lc => lc.Name).ThenBy<Week>(e => e.EndTime);

            string sql = _emit.Query(q);
            Assert.AreEqual(string.Format("SELECT \"LC\".\"ID\" AS \"LCID\", \"LC\".\"NAME\" AS \"LCNAME\", \"LC\".\"STARTWEEKID\" AS \"LCSTARTWEEKID\", \"LC\".\"ENDWEEKID\" AS \"LCENDWEEKID\", \"S\".\"ID\" AS \"SID\", \"S\".\"STARTTIME\" AS \"SSTARTTIME\", \"S\".\"ENDTIME\" AS \"SENDTIME\", \"E\".\"ID\" AS \"EID\", \"E\".\"STARTTIME\" AS \"ESTARTTIME\", \"E\".\"ENDTIME\" AS \"EENDTIME\" FROM (\"LIFECYCLE\" \"LC\" JOIN \"WEEK\" \"S\" ON \"LC\".\"STARTWEEKID\" = \"S\".\"ID\") JOIN \"WEEK\" \"E\" ON \"LC\".\"ENDWEEKID\" = \"E\".\"ID\" WHERE \"S\".\"STARTTIME\" > TO_DATE('{0}', 'YYYY-MM-DD HH24:MI:SS') ORDER BY \"LC\".\"NAME\" ASC, \"E\".\"ENDTIME\" ASC", DateTime.Parse("2000-1-1").ToString("yyyy-MM-dd HH:mm:ss")), sql);
        }

        [Test]
        public void TestPagedQuery1stPage()
        {
            var q = ora.Person.Join(ora.ContactInfo, (p, c) => p.ID == c.PersonID)
                            .Where<Person>(p => p.Name.Like("R%"))
                            .OrderBy<Person>(p => p.ID);
            string sql = _emit.PagedQuery(q, 10, 1);
            Assert.AreEqual("SELECT * FROM (SELECT row_.*, rownum rownum_ FROM (SELECT \"PERSON\".\"ID\" AS \"PERSONID\", \"PERSON\".\"NAME\" AS \"PERSONNAME\", \"PERSON\".\"BIRTH\" AS \"PERSONBIRTH\", \"PERSON\".\"GENDER\" AS \"PERSONGENDER\", \"CONTACTINFO\".\"ID\" AS \"CONTACTINFOID\", \"CONTACTINFO\".\"PERSONID\" AS \"CONTACTINFOPERSONID\", \"CONTACTINFO\".\"PHONE\" AS \"CONTACTINFOPHONE\", \"CONTACTINFO\".\"PHONETYPE\" AS \"CONTACTINFOPHONETYPE\" FROM \"PERSON\" JOIN \"CONTACTINFO\" ON \"PERSON\".\"ID\" = \"CONTACTINFO\".\"PERSONID\" WHERE \"PERSON\".\"NAME\" LIKE 'R%' ORDER BY \"PERSON\".\"ID\" ASC) row_ WHERE rownum <= 10) WHERE rownum_ > 0", sql);
        }

        [Test]
        public void TestPagedQueryAlias2ndPage()
        {
            var q = ora.Person.As("p").Join(ora.ContactInfo.As("c"), (p, c) => p.ID == c.PersonID)
                        .Where<Person>(p => p.Name.Like("R%"))
                        .OrderBy<Person>(p => p.ID);

            string sql = _emit.PagedQuery(q, 10, 2);
            Assert.AreEqual("SELECT * FROM (SELECT row_.*, rownum rownum_ FROM (SELECT \"P\".\"ID\" AS \"PID\", \"P\".\"NAME\" AS \"PNAME\", \"P\".\"BIRTH\" AS \"PBIRTH\", \"P\".\"GENDER\" AS \"PGENDER\", \"C\".\"ID\" AS \"CID\", \"C\".\"PERSONID\" AS \"CPERSONID\", \"C\".\"PHONE\" AS \"CPHONE\", \"C\".\"PHONETYPE\" AS \"CPHONETYPE\" FROM \"PERSON\" \"P\" JOIN \"CONTACTINFO\" \"C\" ON \"P\".\"ID\" = \"C\".\"PERSONID\" WHERE \"P\".\"NAME\" LIKE 'R%' ORDER BY \"P\".\"ID\" ASC) row_ WHERE rownum <= 20) WHERE rownum_ > 10", sql);
        }

        [Test]
        public void TestIdentical()
        {
            var q = ora.Person.Join(ora.ContactInfo, (p, c) => p.ID == c.PersonID).Where<Person>(p=>p.True());
            q.And<ContactInfo>(c => c.PhoneType == PhoneType.Work);
            string sql = _emit.Query(q);
            Assert.AreEqual("SELECT \"PERSON\".\"ID\" AS \"PERSONID\", \"PERSON\".\"NAME\" AS \"PERSONNAME\", \"PERSON\".\"BIRTH\" AS \"PERSONBIRTH\", \"PERSON\".\"GENDER\" AS \"PERSONGENDER\", \"CONTACTINFO\".\"ID\" AS \"CONTACTINFOID\", \"CONTACTINFO\".\"PERSONID\" AS \"CONTACTINFOPERSONID\", \"CONTACTINFO\".\"PHONE\" AS \"CONTACTINFOPHONE\", \"CONTACTINFO\".\"PHONETYPE\" AS \"CONTACTINFOPHONETYPE\" FROM \"PERSON\" JOIN \"CONTACTINFO\" ON \"PERSON\".\"ID\" = \"CONTACTINFO\".\"PERSONID\" WHERE (1 = 1) AND (\"CONTACTINFO\".\"PHONETYPE\" = 1)", sql);
        }

        [Test]
        public void TestUnIdentical()
        {
            var q = ora.Person.Join(ora.ContactInfo, (p, c) => p.ID == c.PersonID).Where<Person>(p => p.False());
            q.Or<ContactInfo>(c => c.PhoneType == PhoneType.Work);
            string sql = _emit.Query(q);
            Assert.AreEqual("SELECT \"PERSON\".\"ID\" AS \"PERSONID\", \"PERSON\".\"NAME\" AS \"PERSONNAME\", \"PERSON\".\"BIRTH\" AS \"PERSONBIRTH\", \"PERSON\".\"GENDER\" AS \"PERSONGENDER\", \"CONTACTINFO\".\"ID\" AS \"CONTACTINFOID\", \"CONTACTINFO\".\"PERSONID\" AS \"CONTACTINFOPERSONID\", \"CONTACTINFO\".\"PHONE\" AS \"CONTACTINFOPHONE\", \"CONTACTINFO\".\"PHONETYPE\" AS \"CONTACTINFOPHONETYPE\" FROM \"PERSON\" JOIN \"CONTACTINFO\" ON \"PERSON\".\"ID\" = \"CONTACTINFO\".\"PERSONID\" WHERE (1 = 0) OR (\"CONTACTINFO\".\"PHONETYPE\" = 1)", sql);
        }
    }
}
