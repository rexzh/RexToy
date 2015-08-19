using System;

using NUnit.Framework;

using RexToy;
using RexToy.Logging;

namespace UnitTest.Logging
{
    [TestFixture]
    public class LoggerFactoryTest
    {
        private ILogConfig _cfg1;
        private ILogConfig _cfg2;
        [SetUp]
        public void SetUp()
        {
            string type = "RexToy.Logging.XmlLogConfig, RexToy.Common";
            _cfg1 = Reflector.LoadInstance<ILogConfig>(type, args: @"..\..\Logging\log1.xml");
            _cfg2 = Reflector.LoadInstance<ILogConfig>(type, args: @"..\..\Logging\log2.xml");
        }

        [Test]
        public void CreateTest()
        {
            ILoggerFactory f = Reflector.LoadInstance<ILoggerFactory>("RexToy.Logging.LoggerFactory, RexToy.Common");
            ILog log1 = f.CreateLogger("abc", _cfg1);
            ILog log2 = f.CreateLogger("abc", _cfg2);
            Assert.IsFalse(object.ReferenceEquals(log1, log2));

            Assert.AreEqual(LogLevel.Debug, log1.LogLevel);
            Assert.AreEqual(LogLevel.Error, log2.LogLevel);
        }

        [Test]
        public void CreateDiffTest()
        {
            ILoggerFactory f = Reflector.LoadInstance<ILoggerFactory>("RexToy.Logging.LoggerFactory, RexToy.Common");
            ILog log1 = f.CreateLogger("abc.log1", _cfg1);
            ILog log2 = f.CreateLogger("abc.log2", _cfg2);
            Assert.AreEqual(LogLevel.Debug, log1.LogLevel);
            Assert.AreEqual(LogLevel.Error, log2.LogLevel);
        }
    }
}
