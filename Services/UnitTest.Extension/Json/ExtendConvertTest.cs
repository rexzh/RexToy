using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using NUnit.Framework;

using RexToy.Json;

namespace UnitTest.Json
{
    class HaveEnum
    {
        public int Id { get; set; }
        public TypeCode Code { get; set; }
    }

    [TestFixture]
    public class ExtendConvertTest
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            ExtendConverter.Instance().Register(typeof(ListConverter));
            ExtendConverter.Instance().Register(typeof(DictionaryConverter));
            ExtendConverter.Instance().Register(typeof(EnumConverter));
        }

        [Test]
        public void TestListToJson()
        {
            List<int> list = new List<int>() { 2, 3, 4, 5 };
            var str = list.ToJsonString();
            Assert.AreEqual("[2,3,4,5]", str);
        }

        [Test]
        public void TestMixToJson()
        {
            List<Point> list = new List<Point>()
            {
                new Point(){X=1, Y=2},
                new Point(){X=2, Y=1}
            };

            var str = list.ToJsonString();
            Assert.AreEqual("[{\"x\":1,\"y\":2},{\"x\":2,\"y\":1}]", str);
        }

        [Test]
        public void TestListFromJson()
        {
            var str = "[2,3,4,5]";

            var l = JsonExtension.ParseJson<List<int>>(str);
            Assert.AreEqual(2, l[0]);
            Assert.AreEqual(5, l[3]);
        }

        [Test]
        public void TestMixFromJson()
        {
            var str = "[{\"x\":1,\"y\":2},{\"x\":2,\"y\":1}]";

            var l = JsonExtension.ParseJson<List<Point>>(str);
            Assert.AreEqual(1, l[0].X);
            Assert.AreEqual(1, l[1].Y);
        }

        [Test]
        public void TestMixFromJson2()
        {
            var str = "[[1, 2],[2, 1]]";

            var l = JsonExtension.ParseJson<List<List<int>>>(str);
            Assert.AreEqual(1, l[0][0]);
            Assert.AreEqual(1, l[1][1]);
        }

        [Test]
        public void TestDictToJson()
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            dict["one"] = 1;
            dict["two"] = 2;
            var str = dict.ToJsonString();
            Assert.AreEqual("{\"one\":1,\"two\":2}", str);
        }

        [Test]
        public void TestDictFromJson()
        {
            var str = "{\"one\":1,\"two\":2}";

            var dict = JsonExtension.ParseJson<Dictionary<string, int>>(str);
            Assert.AreEqual(1, dict["one"]);
            Assert.AreEqual(2, dict["two"]);
        }

        [Test]
        public void TestEnumFromJson()
        {
            string str = "{\"Id\": 2, \"Code\": \"Byte\"}";
            var h = JsonExtension.ParseJson<HaveEnum>(str);
            Assert.AreEqual(TypeCode.Byte, h.Code);
        }

        [Test]
        public void TestEnumToJson()
        {
            var h = new HaveEnum() { Id = 3, Code = TypeCode.Char };
            var str = h.ToJsonString();
            Assert.AreEqual("{\"id\":3,\"code\":\"Char\"}", str);
        }
    }
}
