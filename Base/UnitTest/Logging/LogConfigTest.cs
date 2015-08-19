using System;
using System.Collections.Generic;

using NUnit.Framework;

using RexToy;
using RexToy.Logging;

namespace UnitTest.Logging
{
    [TestFixture]
    public class LogConfigTest
    {
        private ILogConfig _cfg;
        [SetUp]
        public void SetUp()
        {
            _cfg = Reflector.LoadInstance<ILogConfig>("RexToy.Logging.XmlLogConfig, RexToy.Common", args: @"..\..\Logging\log.xml");
        }

        [Test]
        public void TestDeclare()
        {
            Assert.AreEqual(LogLevel.Debug, _cfg.GetLogLevel("abc"));
            Assert.AreEqual(LogLevel.Warning, _cfg.GetLogLevel("abc.def"));
            Assert.AreEqual(LogLevel.Info, _cfg.GetLogLevel("def"));
        }

        [Test]
        public void TestCalcLevel()
        {
            Assert.AreEqual(LogLevel.Debug, _cfg.GetLogLevel("abc.xyz"));
            Assert.AreEqual(LogLevel.Warning, _cfg.GetLogLevel("abc.def.hij"));
            Assert.AreEqual(LogLevel.Info, _cfg.GetLogLevel("def.zzz"));
            Assert.AreEqual(LogLevel.Info, _cfg.GetLogLevel("def.zzz.yyy"));
            Assert.AreEqual(LogLevel.None, _cfg.GetLogLevel("qq"));
        }
    }
}
