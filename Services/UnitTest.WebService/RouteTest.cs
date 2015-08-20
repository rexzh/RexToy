using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

using RexToy.WebService;

namespace UnitTest.WebService
{
    [TestFixture]
    public class RouteTest
    {
        [Test]
        public void Test1()
        {
            RouteAttribute r = new RouteAttribute("/post/:postId/comment/:commentId");
            var result = r.MatchPath("/post/10/comment/2");
            Assert.IsTrue(result.Match);
            Assert.AreEqual("10", result.Captured["postId"]);
            Assert.AreEqual("2", result.Captured["commentId"]);
        }

        [Test]
        public void Test2()
        {
            RouteAttribute r = new RouteAttribute("/post/:postId/comment/:commentId");
            var result = r.MatchPath("/post/comment/10/2");
            Assert.IsFalse(result.Match);
        }
    }
}
