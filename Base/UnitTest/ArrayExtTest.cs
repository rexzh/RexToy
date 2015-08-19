using System;

using NUnit.Framework;

using RexToy;

namespace UnitTest
{
    [TestFixture]
    public class ArrayExtTest
    {
        [Test]
        public void JoinTest1()
        {
            string[] arr = new string[] { "Pack", ".", "Type" };
            Assert.AreEqual("Pack.Type", arr.Join());
        }

        [Test]
        public void JoinTest2()
        {
            string[] arr = new string[] { "Pack", "Type" };
            Assert.AreEqual("Pack.Type", arr.Join('.'));
        }

        [Test]
        public void JoinTest3()
        {
            DateTime[] arr = new DateTime[] { DateTime.Parse("2001-1-1"), DateTime.Parse("2002-5-5"), DateTime.Parse("2003-7-7") };
            string res = arr.Join<DateTime>(d => d.Year.ToString(), ";");
            Assert.AreEqual("2001;2002;2003", res);
        }

        [Test]
        public void SliceTest1()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5, 6 };
            int[] frag = arr.Slice<int>(2, 4);
            Assert.AreEqual(2, frag.Length);
            Assert.AreEqual(3, frag[0]);
            Assert.AreEqual(4, frag[1]);
        }

        [Test]
        public void ReduceTest1()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            var r = arr.Reduce(3);
            Assert.AreEqual(4, r.Length);
            Assert.AreEqual(1, r[0]);
            Assert.AreEqual(2, r[1]);
            Assert.AreEqual(3, r[2]);
            Assert.AreEqual(5, r[3]);
        }

        [Test]
        public void ReduceTest2()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 5 };
            var r = arr.Reduce(1, 3);
            Assert.AreEqual(2, r.Length);
            Assert.AreEqual(1, r[0]);
            Assert.AreEqual(5, r[1]);
        }

        [Test]
        public void CombineTest1()
        {
            int[] arr1 = new int[] { 1, 2 };
            int[] arr2 = new int[] { 3, 4 };
            var r = arr1.Combine(arr2);
            Assert.AreEqual(4, r.Length);
            Assert.AreEqual(1, r[0]);
            Assert.AreEqual(2, r[1]);
            Assert.AreEqual(3, r[2]);
            Assert.AreEqual(4, r[3]);
        }

        [Test]
        public void CombineTest2()
        {
            int[] arr1 = new int[] { 1, 2 };
            int i = 3;
            var r = arr1.Combine(i);
            Assert.AreEqual(3, r.Length);
            Assert.AreEqual(1, r[0]);
            Assert.AreEqual(2, r[1]);
            Assert.AreEqual(3, r[2]);
        }

        [Test]
        public void CombineTest3()
        {
            int i = 1;
            int[] arr2 = new int[] { 2, 3 };
            var r = i.Combine(arr2);
            Assert.AreEqual(3, r.Length);
            Assert.AreEqual(1, r[0]);
            Assert.AreEqual(2, r[1]);
            Assert.AreEqual(3, r[2]);
        }
    }
}
