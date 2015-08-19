using System;
using System.Collections.Generic;
using NUnit.Framework;

using RexToy;

namespace UnitTest
{
    [TestFixture]
    public class TypeCastTest
    {
        [Test]
        public void TestConvertToInt()
        {
            object o = 3;
            int r = TypeCast.ChangeToTypeOrNullableType<int>(o);
            Assert.AreEqual(3, r);
        }

        [Test]
        public void TestConvertToNullableInt()
        {
            object o = 3;
            int? r = TypeCast.ChangeToTypeOrNullableType<int?>(o);
            Assert.AreEqual(3, r);
        }

        [Test]
        public void TestConvertNullToNullableInt()
        {
            object o = null;
            int? r = TypeCast.ChangeToTypeOrNullableType<int?>(o);
            Assert.IsTrue(!r.HasValue);
        }

        [Test]
        public void TestConvertNullableIntToInt()
        {
            int? i = 10;
            int r = TypeCast.ChangeToTypeOrNullableType<int>(i);
            Assert.IsTrue(r == 10);
        }

        [Test]
        public void TestConvertFrom()
        {
            string str = "Class,Struct";
            AttributeTargets t = TypeCast.ChangeType<AttributeTargets>(str);

            Assert.IsTrue(t == (AttributeTargets.Class | AttributeTargets.Struct));
        }
    }
}
