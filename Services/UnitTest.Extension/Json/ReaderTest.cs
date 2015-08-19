using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using RexToy.Json;

namespace UnitTest.Json
{
    [TestFixture]
    public class ReaderTest
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            ExtendConverter.Instance().Register(typeof(JavascriptTimeConverter));
        }

        [Test]
        public void SimpleObjectTest()
        {
            string s = "{\"x\":1,\"y\":2.0,\"avail\":true}";

            JsonObject json = (JsonObject)s.ParseToJsonObject();

            Assert.AreEqual("1", json["x"]);
            Assert.AreEqual("2.0", json["y"]);
            Assert.AreEqual("true", json["avail"]);
            Assert.AreEqual(null, json["notexist"]);
        }

        [Test]
        public void SimpleArrayTest()
        {
            string s = "[10,\"Hello,This is a value\",null]";
            JsonArray json = (JsonArray)s.ParseToJsonObject();

            Assert.AreEqual("10", json[0]);
            Assert.AreEqual("\"Hello,This is a value\"", json[1]);
            Assert.AreEqual(null, json[2]);//Note:null keyword eval to null
            Assert.AreEqual(null, json[3]);//Note:out of index eval to null
        }

        [Test]
        public void ArrayInObjectTest()
        {
            string s = "{\"x\":[1,\"abc\r\ndef\",3],\"y\":5}";

            JsonObject json = (JsonObject)s.ParseToJsonObject();

            JsonArray arr = (JsonArray)json["x"];
            Assert.AreEqual("1", arr[0]);
            Assert.AreEqual("\"abc\r\ndef\"", arr[1]);
            Assert.AreEqual("3", arr[2]);

            Assert.AreEqual("5", json["y"]);
        }

        [Test]
        public void ObjectInArrayTest()
        {
            string s = "[{\"x\":0,\"y\":0},{\"x\":1,\"y\":1}]";

            JsonArray json = (JsonArray)s.ParseToJsonObject();

            JsonObject o1 = (JsonObject)json[0];
            JsonObject o2 = (JsonObject)json[1];

            Assert.AreEqual("0", o1["x"]);
            Assert.AreEqual("0", o1["y"]);
            Assert.AreEqual("1", o2["x"]);
            Assert.AreEqual("1", o2["y"]);
        }

        [Test]
        public void ObjectInObjectTest()
        {
            string s = "{\"circle\":{\"x\":0,\"y\":0}}";

            JsonObject json = (JsonObject)s.ParseToJsonObject();
            JsonObject circle = (JsonObject)json["circle"];
            Assert.AreEqual("0", circle["x"]);
            Assert.AreEqual("0", circle["y"]);
        }

        [Test]
        public void ArrayInArrayTest()
        {
            string s = "[[1,0],[0,1]]";

            JsonArray array = (JsonArray)s.ParseToJsonObject();

            JsonArray a0 = (JsonArray)array[0];
            JsonArray a1 = (JsonArray)array[1];

            Assert.AreEqual("1", a0[0]);
            Assert.AreEqual("0", a0[1]);
            Assert.AreEqual("0", a1[0]);
            Assert.AreEqual("1", a1[1]);
        }

        [Test]
        public void Test()
        {
            string s = "\"a,b\"";
            string json = (string)s.ParseToJsonObject();

            Assert.AreEqual(s, json);
        }

        [Test]
        public void EmptyArrayTest()
        {
            string s = "[]";
            JsonArray json = (JsonArray)s.ParseToJsonObject();

            Assert.AreEqual(0, json.Length);
        }

        [Test]
        public void EmptyObjectTest()
        {
            string s = "{}";
            JsonObject json = (JsonObject)s.ParseToJsonObject();
        }

        [Test]
        public void EscapeTest1()
        {
            string s = "\"He\\\"llo\"";
            var val = s.ParseJson<string>();
            
            Assert.AreEqual("He\"llo", val);
            
        }

        [Test]
        public void EscapeTest2()
        {
            string s = "{\"cir\\\"cle\":{\"x\":0,\"y\":0}}";
            var c = s.ParseToJsonObject() as JsonObject;
            var obj = c["cir\"cle"];
            Assert.IsNotNull(obj);
        }

        [Test]
        public void SimpleTypeTest()
        {
            string sint = "1";
            int i = sint.ParseJson<int>();
            Assert.AreEqual(1, i);

            string snull = "null";
            object o = snull.ParseJson<object>();
            Assert.IsNull(o);

            string sbool = "true";
            bool b = sbool.ParseJson<bool>();
            Assert.AreEqual(true, b);
        }
    }
}