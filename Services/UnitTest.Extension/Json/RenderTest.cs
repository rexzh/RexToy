using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using RexToy.Json;

namespace UnitTest.Json
{
    [TestFixture]
    public class RenderTest
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            ExtendConverter.Instance().Register(typeof(JavascriptTimeConverter));
        }

        [Test]
        public void SimpleObjectTest()
        {
            string s = "{\"width\":20,\"height\":10}";

            Rectangle rect = s.ParseJson<Rectangle>();

            Assert.AreEqual(20, rect.Width);
            Assert.AreEqual(10, rect.Height);
        }

        [Test]
        public void SimpleObjectIgnoreTypeSafeTest()
        {
            string s = "{\"width\":\"20\",\"height\":10}";

            Rectangle rect = (Rectangle)s.ParseJson(typeof(Rectangle), true);

            Assert.AreEqual(20, rect.Width);
            Assert.AreEqual(10, rect.Height);
        }

        [Test]
        public void SimpleArrayTest()
        {
            string s = "[1,1,2,3,5,8]";

            int[] i = s.ParseJson<int[]>();

            Assert.AreEqual(1, i[0]);
            Assert.AreEqual(8, i[5]);
        }

        [Test]
        public void SimpleArrayIgnoreTypeSafeTest()
        {
            string s = "[1,1,2,3,5,\"8\"]";

            int[] i = s.ParseJson<int[]>(true);

            Assert.AreEqual(1, i[0]);
            Assert.AreEqual(8, i[5]);
        }

        [Test]
        public void ArrayInArrayTest()
        {
            string s = "[[1,0],[0,1]]";

            int[][] T = s.ParseJson<int[][]>();

            Assert.AreEqual(1, T[0][0]);
            Assert.AreEqual(1, T[1][1]);
            Assert.AreEqual(0, T[1][0]);
            Assert.AreEqual(0, T[0][1]);
        }

        [Test]
        public void ArrayInArrayIgnoreTypeSafeTest()
        {
            string s = "[[1,\"0\"],[\"0\",1]]";

            int[][] T = s.ParseJson<int[][]>(true);

            Assert.AreEqual(1, T[0][0]);
            Assert.AreEqual(1, T[1][1]);
            Assert.AreEqual(0, T[1][0]);
            Assert.AreEqual(0, T[0][1]);
        }

        [Test]
        public void ObjectInObjectTest()
        {
            string s = "{\"p1\":{\"x\":1,\"y\":1},\"p2\":{\"x\":1,\"y\":2},\"p3\":{\"x\":2,\"y\":1}}";

            Triangle t = s.ParseJson<Triangle>();

            Assert.AreEqual(1, t.P1.X);
            Assert.AreEqual(1, t.P1.Y);

            Assert.AreEqual(1, t.P2.X);
            Assert.AreEqual(2, t.P2.Y);

            Assert.AreEqual(2, t.P3.X);
            Assert.AreEqual(1, t.P3.Y);
        }

        [Test]
        public void ObjectInObjectNullTest()
        {
            string s = "{\"p1\":{\"x\":1,\"y\":1},\"p2\":null,\"p3\":{\"x\":2,\"y\":1}}";

            Triangle t = s.ParseJson<Triangle>();

            Assert.AreEqual(1, t.P1.X);
            Assert.AreEqual(1, t.P1.Y);

            Assert.AreEqual(null, t.P2);

            Assert.AreEqual(2, t.P3.X);
            Assert.AreEqual(1, t.P3.Y);
        }

        [Test]
        public void ObjectInObjectIgnoreTypeSafeTest()
        {
            string s = "{\"p1\":{\"x\":\"1\",\"y\":\"1\"},\"p2\":{\"x\":1,\"y\":2},\"p3\":{\"x\":2,\"y\":1}}";

            Triangle t = s.ParseJson<Triangle>(true);

            Assert.AreEqual(1, t.P1.X);
            Assert.AreEqual(1, t.P1.Y);

            Assert.AreEqual(1, t.P2.X);
            Assert.AreEqual(2, t.P2.Y);

            Assert.AreEqual(2, t.P3.X);
            Assert.AreEqual(1, t.P3.Y);
        }

        [Test]
        public void ObjectInArrayTest()
        {
            string s = "[{\"x\":1,\"y\":1},{\"x\":2,\"y\":2}]";

            Point[] points = s.ParseJson<Point[]>();

            Assert.AreEqual(1, points[0].X);
            Assert.AreEqual(1, points[0].Y);
            Assert.AreEqual(2, points[1].X);
            Assert.AreEqual(2, points[1].Y);
        }

        [Test]
        public void ObjectInArrayNullTest()
        {
            string s = "[{\"x\":1,\"y\":1}, null]";

            Point[] points = s.ParseJson<Point[]>();

            Assert.AreEqual(1, points[0].X);
            Assert.AreEqual(1, points[0].Y);
            Assert.AreEqual(null, points[1]);
        }

        [Test]
        public void ObjectInArrayIgnoreTypeSafeTest()
        {
            string s = "[{\"x\":\"1\",\"y\":\"1\"},{\"x\":2,\"y\":2}]";

            Point[] points = s.ParseJson<Point[]>(true);

            Assert.AreEqual(1, points[0].X);
            Assert.AreEqual(1, points[0].Y);
            Assert.AreEqual(2, points[1].X);
            Assert.AreEqual(2, points[1].Y);
        }

        [Test]
        public void ArrayInObjectTest()
        {
            string s = "{\"arr\":[1,1,2,3,5]}";

            Fibo f = s.ParseJson<Fibo>();

            Assert.AreEqual(1, f.Arr[0]);
            Assert.AreEqual(1, f.Arr[1]);
            Assert.AreEqual(2, f.Arr[2]);
            Assert.AreEqual(3, f.Arr[3]);
            Assert.AreEqual(5, f.Arr[4]);
        }

        [Test]
        public void ArrayInObjectIgnoreTypeSafeTest()
        {
            string s = "{\"arr\":[\"1\",1,2,3,\"5\"]}";

            Fibo f = s.ParseJson<Fibo>(true);

            Assert.AreEqual(1, f.Arr[0]);
            Assert.AreEqual(1, f.Arr[1]);
            Assert.AreEqual(2, f.Arr[2]);
            Assert.AreEqual(3, f.Arr[3]);
            Assert.AreEqual(5, f.Arr[4]);
        }

        [Test]
        public void DeepArrRecursiveTest()
        {
            string str = "[[[[0,1],[1,2]],[[2,3],[3,4]]],[[[0,1],[1,2]],[[2,3],[3,4]]]]";

            int[][][][] S = str.ParseJson<int[][][][]>();
            Assert.IsTrue(S[0][0][0][0] == 0);
            Assert.IsTrue(S[0][0][1][0] == 1);
        }

        [Test]
        public void RenderTextTest()
        {
            string s = "{\"msg\":\"Hello World!I'm JSON,Yeah\"}";

            Chat c = s.ParseJson<Chat>();

            Assert.AreEqual("Hello World!I'm JSON,Yeah", c.Msg);
        }

        [Test]
        public void RenderTextIgnoreTypeSafeTest()
        {
            string s = "{\"msg\":\"Hello World!I'm JSON,Yeah\"}";

            Chat c = s.ParseJson<Chat>(true);

            Assert.AreEqual("Hello World!I'm JSON,Yeah", c.Msg);
        }

        [Test]
        public void IntTest()
        {
            string s = "100";

            int i = s.ParseJson<int>();
            Assert.AreEqual(100, i);
        }

        [Test]
        public void IntIgnoreTypeSafeTest()
        {
            string s = "100";

            int i = s.ParseJson<int>(true);
            Assert.AreEqual(100, i);
        }

        [Test]
        public void StringTest()
        {
            string str = "\"str\"";
            string s = str.ParseJson<string>();
            Assert.AreEqual("str", s);
        }
    }
}
