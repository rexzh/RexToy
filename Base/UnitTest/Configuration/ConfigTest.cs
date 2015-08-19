using System;
using System.Collections.Generic;

using NUnit.Framework;

using RexToy.Configuration;
using RexToy.Xml;

namespace UnitTest.Configuration
{
    [TestFixture]
    public class ConfigTest
    {
        IConfig _xcfg;
        IConfig _tcfg;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _xcfg = ConfigFactory.CreateXmlConfig(@"..\..\Configuration\samplecfg1.xml");
            _tcfg = ConfigFactory.CreateTextConfig(@"..\..\Configuration\sample_config.ini");
        }

        [Test]
        public void XmlExistSectionTest()
        {
            Assert.IsTrue(_xcfg.ExistsSection("sect1"));
            Assert.IsTrue(_xcfg.ExistsSection("sect2"));
            Assert.IsFalse(_xcfg.ExistsSection("sect99"));
        }

        [Test]
        public void XmlExistKeyTest()
        {
            Assert.IsTrue(_xcfg.ExistsKey("sect1", "k1"));
            Assert.IsTrue(_xcfg.ExistsKey("sect2", "k2"));
            Assert.IsFalse(_xcfg.ExistsKey("sect1", "k2"));
            Assert.IsFalse(_xcfg.ExistsKey("sect99", "k"));
        }

        [Test]
        public void XmlGetEnumTest()
        {
            StringComparison? cmp = _xcfg.ReadEnumValue<StringComparison>("sect2", "k2");
            Assert.AreEqual(StringComparison.OrdinalIgnoreCase, cmp);
            Assert.AreEqual(355, _xcfg.ReadValue<int>("sect1", "k1"));
        }

        [Test, ExpectedException(typeof(ConfigException))]
        public void XmlGetConfigValueNotExistTest()
        {
            _xcfg.ReadValue<int>("sect", "key");
        }

        [Test, ExpectedException(typeof(ConfigException))]
        public void XmlGetConfigValueErrorConvertTest()
        {
            _xcfg.ReadValue<System.SByte>("sect1", "k1");
        }

        [Test]
        public void XmlGetTypeTest()
        {
            Type t = _xcfg.ReadType("sect2", "k");
            Assert.AreEqual(typeof(ConfigTest), t);
        }


        [Test]
        public void TextExistSectionTest()
        {
            Assert.IsTrue(_tcfg.ExistsSection("sect1"));
            Assert.IsTrue(_tcfg.ExistsSection("sect2"));
            Assert.IsFalse(_tcfg.ExistsSection("sect99"));
        }

        [Test]
        public void TextExistKeyTest()
        {
            Assert.IsTrue(_tcfg.ExistsKey("sect1", "k1"));
            Assert.IsTrue(_tcfg.ExistsKey("sect2", "k2"));
            Assert.IsFalse(_tcfg.ExistsKey("sect1", "k2"));
            Assert.IsFalse(_tcfg.ExistsKey("sect99", "k"));
        }

        [Test]
        public void TextGetEnumTest()
        {
            StringComparison? cmp = _tcfg.ReadEnumValue<StringComparison>("sect2", "k2");
            Assert.AreEqual(StringComparison.OrdinalIgnoreCase, cmp);
            Assert.AreEqual(355, _tcfg.ReadValue<int>("sect1", "k1"));
        }

        [Test, ExpectedException(typeof(ConfigException))]
        public void TextGetConfigValueNotExistTest()
        {
            _tcfg.ReadValue<int>("sect", "key");
        }

        [Test, ExpectedException(typeof(ConfigException))]
        public void TextGetConfigValueErrorConvertTest()
        {
            _tcfg.ReadValue<System.SByte>("sect1", "k1");
        }

        [Test]
        public void TextGetTypeTest()
        {
            Type t = _tcfg.ReadType("sect2", "k");
            Assert.AreEqual(typeof(ConfigTest), t);
        }
    }
}
