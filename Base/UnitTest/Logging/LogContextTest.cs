using System;
using System.Collections.Generic;

using NUnit.Framework;

using RexToy.Logging;

namespace UnitTest.Logging
{
    [TestFixture]
    public class LogContextTest
    {
        [Test]
        public void GetLoggerTestNonGeneric()
        {
            ILog log = LogContext.GetLogger<String>();
            Assert.AreEqual("System.String", log.Name);
        }

        [Test]
        public void GetLoggerTestGeneric()
        {
            ILog log = LogContext.GetLogger<List<string>>();
            Assert.AreEqual("System.Collections.Generic.List<System.String>", log.Name);
            Assert.AreEqual(LogLevel.None, log.LogLevel);
        }

        [Test]
        public void GetLoggerTestGeneric2()
        {
            ILog log = LogContext.GetLogger<Dictionary<int, string>>();
            Assert.AreEqual("System.Collections.Generic.Dictionary<System.Int32, System.String>", log.Name);
            Assert.AreEqual(LogLevel.None, log.LogLevel);
        }

        [Test]
        public void GetLoggerTestGenericRecursive()
        {
            ILog log = LogContext.GetLogger<List<List<string>>>();
            Assert.AreEqual("System.Collections.Generic.List<System.Collections.Generic.List<System.String>>", log.Name);
            Assert.AreEqual(LogLevel.None, log.LogLevel);
        }
    }
}
