using System;
using System.Collections.Generic;

using NUnit.Framework;

using RexToy.Copy;

namespace UnitTest.Copy
{
    [TestFixture]
    public class CopyTest
    {
        [Test]
        public void CopyBySource()
        {
            var src = new Dictionary<string, string>();
            src["Age"] = "15";


            Sample.Person p = new Sample.Person();
            src.ShallowCopy(p, CopyOptions.BaseOnSource, false);

            Assert.AreEqual(15, p.Age);
        }

        [Test]
        [ExpectedException(typeof(CopyException))]
        public void CopyBySourceWithError()
        {
            var src = new Dictionary<string, string>();
            src["Age"] = "15";
            src["Salary"] = "3000";

            Sample.Person p = new Sample.Person();
            src.ShallowCopy(p, CopyOptions.BaseOnSource, true);

            Assert.AreEqual(15, p.Age);
        }

        [Test]
        public void CopyByBoth()
        {
            var src = new Dictionary<string, string>();
            src["Name"] = "r";
            src["Age"] = "15";


            Sample.Person p = new Sample.Person();
            src.ShallowCopy(p);

            Assert.AreEqual(15, p.Age);
            Assert.AreEqual("r", p.Name);
        }

        [Test]
        public void CopyByDest()
        {
            var src = new Dictionary<string, string>();
            src["Age"] = "15";


            Sample.Person p = new Sample.Person();
            src.ShallowCopy(p, CopyOptions.BaseOnDest, false);

            Assert.AreEqual(15, p.Age);
        }

        [Test]
        [ExpectedException(typeof(CopyException))]
        public void CopyByDestWithError()
        {
            var src = new Dictionary<string, string>();
            src["Age"] = "15";

            Sample.Person p = new Sample.Person();
            src.ShallowCopy(p, CopyOptions.BaseOnDest, true);

            Assert.AreEqual(15, p.Age);
        }
    }
}
