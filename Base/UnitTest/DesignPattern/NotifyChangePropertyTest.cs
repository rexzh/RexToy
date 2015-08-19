using System;
using System.Collections.Generic;

using NUnit.Framework;

using UnitTest.Sample;

namespace UnitTest.DesignPattern
{
    [TestFixture]
    public class NotifyChangePropertyTest
    {
        [Test]
        public void TestChangeName()
        {
            Person p = new Person();
            PersonObserver ob = new PersonObserver();
            p.PropertyChanged += ob.PropertyChanged;

            p.Name = "H";
            Assert.AreEqual("Name", ob.ChangedName);
        }

        [Test]
        public void TestChangeAge()
        {
            Person p = new Person();
            PersonObserver ob = new PersonObserver();
            p.PropertyChanged += ob.PropertyChanged;

            p.Age = 0;
            Assert.AreEqual(null, ob.ChangedName);
            p.Age = 3;
            Assert.AreEqual("Age", ob.ChangedName);
        }
    }
}
