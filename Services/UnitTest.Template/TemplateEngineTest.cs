using System;
using System.Collections.Generic;

using NUnit.Framework;

using RexToy.Configuration;
using RexToy.Template;

namespace UnitTest.Template
{
    [TestFixture]
    public class TemplateEngineTest
    {
        TemplateEngine _engine;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            AppConfig.Destroy();
            AppConfig.Load(ConfigFactory.CreateXmlConfig(@"..\..\_ConfigFiles\config.xml"));
        }

        [SetUp]
        public void SetUp()
        {
            _engine = TemplateEngine.CreateInstance(@"..\..\Examples");
        }

        [Test]
        public void TestPathDefault()
        {
            TemplateEngine engine = TemplateEngine.CreateInstance();
            Assert.IsTrue(engine.Path.EndsWith(@"\Services\UnitTest.Template\bin\Debug", StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        public void TestPathAssigned()
        {
            TemplateEngine engine = TemplateEngine.CreateInstance(@"..\..\Examples");
            Assert.IsTrue(engine.Path.EndsWith(@"\Services\UnitTest.Template\Examples", StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        public void TestSimpleTemplate()
        {
            string result = _engine.Render("Simple.tpl");
            Assert.AreEqual("5*6=30", result);
        }

        [Test]
        public void TestForTemplate()
        {
            string result = _engine.Render("For.tpl");
            Assert.AreEqual("12345", result);
        }

        [Test]
        public void TestIfTemplate()
        {
            string result = _engine.Render("If.tpl");
            Assert.AreEqual("\ta>0", result);
        }

        [Test]
        public void TestIfElseTemplate()
        {
            string result = _engine.Render("IfElse.tpl");
            Assert.AreEqual("\t\ta<0", result);
        }

        [Test]
        public void TestIncludeTemplate()
        {
            string result = _engine.Render("SimpleContent.tpl");
            Assert.AreEqual("Title\r\nHead\r\nContent\r\nFoot", result);
        }

        [Test]
        public void TestForWithBreakTemplate()
        {
            string result = _engine.Render("ForWithBreak.tpl");
            Assert.AreEqual("123", result);
        }

        [Test]
        public void TestForWithContinueTemplate()
        {
            string result = _engine.Render("ForWithContinue.tpl");
            Assert.AreEqual("345", result);
        }

        [Test]
        public void TestClassDefTemplate()
        {
            string result = _engine.Render("ClassDef.tpl");
            Assert.AreEqual(int.MinValue.ToString(), result);
        }
    }
}
