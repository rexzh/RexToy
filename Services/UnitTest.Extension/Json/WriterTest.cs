using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

using RexToy.Json;

namespace UnitTest.Json
{
    [TestFixture]
    public class WriterTest
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            ExtendConverter.Instance().Register(typeof(JavascriptTimeConverter));
        }

        [Test]
        public void IntTest()
        {
            int i = 10;
            string json = i.ToJsonString();
            Assert.AreEqual("10", json);
        }

        [Test]
        public void StringTest()
        {
            string s = "Hello";
            string json = s.ToJsonString();
            Assert.AreEqual("\"Hello\"", json);
        }

        [Test]
        public void EmptyArrayTest()
        {
            int[] i = new int[] { };
            string json = i.ToJsonString();
            Assert.AreEqual("[]", json);
        }

        [Test]
        public void EmptyObjectTest()
        {
            Empty e = new Empty();

            string json = e.ToJsonString();
            Assert.AreEqual("{}", json);
        }

        [Test]
        public void ArrayTest()
        {
            int[] i = new int[4] { 1, 2, 3, 4 };
            string json = i.ToJsonString();
            Assert.AreEqual("[1,2,3,4]", json);
        }

        [Test]
        public void ObjectTest()
        {
            Position p = new Position();
            p.X = 10;
            p.Y = 20;

            string json = p.ToJsonString();
            Assert.AreEqual("{\"x\":10,\"y\":20}", json);
        }

        [Test]
        public void ObjectInObjectTest()
        {
            Triangle t = new Triangle();
            t.P1.X = 10;
            t.P1.Y = 10;
            t.P2.X = 10;
            t.P2.Y = 20;
            t.P3.X = 20;
            t.P3.Y = 10;

            string json = t.ToJsonString();

            //Assert.AreEqual("{\"p1\":{\"x\":10,\"y\":10},\"p2\":{\"x\":10,\"y\":20},\"p3\":{\"x\":20,\"y\":10}}", json);
            Triangle t2 = json.ParseJson<Triangle>();
            Assert.AreEqual(t.P1.X, t2.P1.X);
            Assert.AreEqual(t.P1.Y, t2.P1.Y);
            Assert.AreEqual(t.P2.X, t2.P2.X);
            Assert.AreEqual(t.P2.Y, t2.P2.Y);
            Assert.AreEqual(t.P3.X, t2.P3.X);
            Assert.AreEqual(t.P3.Y, t2.P3.Y);
        }

        [Test]
        public void ArrayInArrayTest()
        {
            int[][] T = new int[][] { new int[] { 1, 0 }, new int[] { 0, 1 } };
            string json = T.ToJsonString();

            Assert.AreEqual("[[1,0],[0,1]]", json);
        }

        [Test]
        public void ObjectInArrayTest()
        {
            Point[] points = new Point[2];
            points[0] = new Point();
            points[1] = new Point();

            points[0].X = 1;
            points[0].Y = 1;
            points[1].X = 2;
            points[1].Y = 2;

            string json = points.ToJsonString();

            //Assert.AreEqual("[{\"x\":1,\"y\":1},{\"x\":2,\"y\":2}]", json);
            Point[] points2 = json.ParseJson<Point[]>();
            Assert.AreEqual(1, points2[0].X);
            Assert.AreEqual(1, points2[0].Y);
            Assert.AreEqual(2, points2[1].X);
            Assert.AreEqual(2, points2[1].Y);

        }

        [Test]
        public void ArrayInObjectTest()
        {
            Fibo f = new Fibo();
            string json = f.ToJsonString();

            Assert.AreEqual("{\"arr\":[1,1,2,3,5]}", json);
        }

        [Test]
        public void DateTimeExtendTest()
        {
            DateTime t = new DateTime(2000, 1, 1, 12, 30, 0);
            string json = t.ToJsonString();

            Assert.AreEqual("94670100000000", json);
        }

        [Test]
        public void StringEscapeTest()
        {
            string t = "He\"llo";
            string json = t.ToJsonString();

            Assert.AreEqual("\"He\"llo\"", json);
        }
    }
}