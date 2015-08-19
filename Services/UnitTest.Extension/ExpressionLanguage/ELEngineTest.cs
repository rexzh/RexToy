using System;
using System.Collections.Generic;
using NUnit.Framework;

using RexToy.ExpressionLanguage;

namespace UnitTest.ExpressionLanguage
{
    [TestFixture]
    public class ELEngineTest
    {
        [Test]
        public void Test()
        {
            ExpressionLanguageEngine engine = ExpressionLanguageEngine.CreateEngine();
            engine.Assign("m", 10);
            engine.Assign("v", 5);
            float E = engine.Eval<float>("m*v*v/2");
            Assert.AreEqual(125, E);
        }
    }
}
