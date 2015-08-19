using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using RexToy.Json;

namespace UnitTest.Json
{
    [TestFixture]
    public class ConvertTest
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            ExtendConverter.Instance().Register(typeof(JavascriptTimeConverter));
        }

        [Test]
        public void Test()
        {
            var start = DateTime.Now;
            Employee e0 = new Employee("Zhang", 30, 10000.00, start);

            string jsonText = e0.ToJsonString();

            Employee e1 = jsonText.ParseJson<Employee>();

            Assert.AreEqual("Zhang", e1.Name);
            Assert.AreEqual(30, e1.Age);
            Assert.AreEqual(10000.00, e1.Salary);
            Assert.AreEqual(start.Ticks / 100, e1.Start.Ticks / 100);
        }
    }
}
