using System;
using System.Collections.Generic;

using NUnit.Framework;

using RexToy.Configuration;
using RexToy.AOP;

namespace UnitTest.AOP
{
    [TestFixture]
    public class AOPTest
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            AppConfig.Destroy();
            AppConfig.Load(ConfigFactory.CreateXmlConfig(@".\config.xml"));
        }

        [Test]
        public void TestBefore()
        {
            try
            {
                MyComponent my = new MyComponent();
                int j = 4;
                Assert.IsTrue(!MyBeforeAdvisor.executed);
                bool b = my.Run("Hello", ref j);
                Assert.IsTrue(MyBeforeAdvisor.executed);
            }
            finally
            {
                MyBeforeAdvisor.executed = false;
            }
        }

        [Test]
        public void TestAround()
        {
            try
            {
                MyComponent my = new MyComponent();

                my.AddCount();
                Assert.IsTrue(MyAroundAdvisor.around_a);
                Assert.AreEqual(1, my.Count);
            }
            finally
            {
                MyAroundAdvisor.around_a = false;
            }
        }

        [Test]
        public void TestAfter()
        {
            try
            {
                MyComponent my = new MyComponent();
                Assert.IsFalse(MyAfterAdvisor.executed);
                my.AddOne(3);
                Assert.IsTrue(MyAfterAdvisor.executed);
            }
            finally
            {
                MyAfterAdvisor.executed = false;
            }
        }

        [Test]
        public void TestAroundNotProceed()
        {
            MyComponent my = new MyComponent();
            my.AddCount();
            MyAroundAdvisor.around_b = false;

            try
            {
                my.Reset();//Method will be skip            
                Assert.IsTrue(MyAroundAdvisor.around_b);
                Assert.AreEqual(1, my.Count);
            }
            finally
            {
                MyAroundAdvisor.around_b = false;
            }
        }

        [Test]
        public void TestDeriveAroundNotProceed()
        {
            MyDerivedComponent my = new MyDerivedComponent();
            my.AddCount();
            MyAroundAdvisor.around_b = false;

            try
            {
                my.Reset();//Method will be skip            
                Assert.IsTrue(MyAroundAdvisor.around_b);
                Assert.AreEqual(1, my.Count);
            }
            finally
            {
                MyAroundAdvisor.around_b = false;
            }
        }
    }
}
