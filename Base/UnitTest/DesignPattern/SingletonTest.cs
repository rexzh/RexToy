using System;
using System.Text;
using NUnit.Framework;

using RexToy;
using RexToy.DesignPattern;

namespace UnitTest
{
    [TestFixture]
    public class SingletonTest
    {
        [Test]
        public void Test()
        {
            SampleType2 s1 = Singleton<SampleType2>.Instance();
            SampleType2 s2 = Singleton<SampleType2>.Instance();

            Assert.IsTrue(object.ReferenceEquals(s1, s2));
        }
    }
}
