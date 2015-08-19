using System;
using System.Collections.Generic;

using NUnit.Framework;

using RexToy;
using RexToy.Logging;

namespace UnitTest.Logging
{
    [TestFixture]
    public class LogTest
    {
        ILoggerFactory _f;
        [SetUp]
        public void SetUp()
        {
            _f = Reflector.LoadInstance<ILoggerFactory>("RexToy.Logging.LoggerFactory, RexToy.Common");
        }

        [Test]
        public void LogDebugTest()
        {
            ILog log = _f.CreateLogger("debug", new SampleLogConfig());

            log.Debug("Hello");
            Assert.AreEqual("Hello", GetWriter(log).Msg);

            log.Info("Next");
            Assert.AreEqual("Next", GetWriter(log).Msg);
        }

        [Test]
        public void LogInfoTest()
        {
            ILog log = _f.CreateLogger("info", new SampleLogConfig());

            log.Debug("Hello");
            Assert.IsNull(GetWriter(log).Msg);

            log.Info("Next");
            Assert.AreEqual("Next", GetWriter(log).Msg);
        }

        [Test]
        public void LogInfoIfTest()
        {
            ILog log = _f.CreateLogger("info", new SampleLogConfig());


            log.InfoIf(3 > 5, "Next");
            Assert.IsNull(GetWriter(log).Msg);
        }

        private SampleWriter GetWriter(ILog log)
        {
            object obj = Reflector.Bind(log, ReflectorPolicy.InstanceAll).GetFieldValue("_writer");
            SampleWriter writer = obj as SampleWriter;
            Assert.IsNotNull(writer);
            return writer;
        }
    }
}
