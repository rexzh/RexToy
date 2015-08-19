using System;
using System.Globalization;
using System.Collections.Generic;
using System.Threading;

using NUnit.Framework;

using RexToy.L10N;

namespace UnitTest.Extension.L10N
{
    [TestFixture]
    public class L10NTest
    {
        private const string MSG_HELLO = "MSG_HELLO";
        private const string MSG_EXIT = "MSG_EXIT";
        private const string MSG_BYE = "BYE";

        [Test]
        public void TestBuild()
        {
            var l = L10NContext.GetLocalization("en-US");
            Assert.AreEqual("en-US", l.CultureInfo.Name);
        }

        [Test]
        public void TestBuildDefault()
        {
            var l = L10NContext.GetLocalization();
            Assert.AreEqual(Thread.CurrentThread.CurrentCulture.Name, l.CultureInfo.Name);
        }

        [Test]
        public void TestBuildFallback()
        {
            var l = L10NContext.GetLocalization("zzzz");
            Assert.AreEqual(Thread.CurrentThread.CurrentCulture.Name, l.CultureInfo.Name);
        }

        [Test]
        public void TestEN_US()
        {
            var l = L10NContext.GetLocalization("en-US");
            string tr = l.Localize(MSG_HELLO);
            Assert.AreEqual("Hello", tr);
        }

        [Test]
        public void TestEN_US_NotExist()
        {
            var l = L10NContext.GetLocalization("en-US");
            string tr = l.Localize(MSG_BYE);
            Assert.AreEqual(MSG_BYE, tr);
        }

        [Test]
        public void TestZH_CN()
        {
            var l = L10NContext.GetLocalization("zh-CN");
            string tr = l.Localize(MSG_HELLO);
            Assert.AreEqual("你好", tr);
        }

        [Test]
        public void TestZH_CN_NotExist()
        {
            var l = L10NContext.GetLocalization("zh-CN");
            string tr = l.Localize(MSG_BYE);
            Assert.AreEqual(MSG_BYE, tr);
        }
    }
}
