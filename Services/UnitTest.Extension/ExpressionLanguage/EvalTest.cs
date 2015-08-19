using System;
using System.Collections.Generic;
using NUnit.Framework;

using RexToy;
using RexToy.ExpressionLanguage;

namespace UnitTest.ExpressionLanguage
{
    [TestFixture]
    public class EvalTest
    {
        private ExpressionLanguageEngine engine;

        [SetUp]
        public void SetUp()
        {
            engine = ExpressionLanguageEngine.CreateEngine(new Eval());
        }

        [Test]
        public void Test1()
        {
            string expr = "(a>b)?a:b";
            object res = engine.Eval(expr);
            Assert.AreEqual(10, res);
        }

        [Test]
        public void Test2()
        {
            string expr = "-a";
            object res = engine.Eval(expr);
            Assert.AreEqual(-10, res);
        }

        [Test]
        public void Test3()
        {
            string expr = "\"result=\"+(-a)";
            object res = engine.Eval(expr);
            Assert.AreEqual("result=-10", res);
        }

        [Test]
        public void Test4()
        {
            string expr = "numbers[0]+numbers[1]";
            object res = engine.Eval(expr);
            Assert.AreEqual("OneTwo", res);
        }

        [Test]
        public void Test5()
        {
            string expr = "numbers.Count";
            object res = engine.Eval(expr);
            Assert.AreEqual(3, res);
        }

        [Test]
        public void Test6()
        {
            string expr = "a.GetType().ToString()";
            object res = engine.Eval(expr);
            Assert.AreEqual("System.Int32", res);
        }

        [Test]
        public void Test7()
        {
            string expr = "Devide(15,30)";
            object res = engine.Eval(expr);
            Assert.AreEqual(0.5, res);
        }

        [Test]
        public void Test8()
        {
            string expr = "(a+b)/2";
            object res = engine.Eval(expr);
            Assert.AreEqual(7.5, res);
        }

        [Test]
        public void Test9()
        {
            string expr = "a%7%3";
            object res = engine.Eval(expr);
            Assert.AreEqual(0, res);
        }

        [Test]
        public void Test10()
        {
            string expr = "c[0][0]";
            object res = engine.Eval(expr);
            Assert.AreEqual(1, res);
        }

        [Test]
        public void Test11()
        {
            string expr = "dialect==\"SQL\"";
            object r = engine.Eval(expr);
            Assert.AreEqual(true, r);
        }

        [Test]
        public void Test14()
        {
            string expr = "dialect.TrimEnd(\"L\")";
            object r = engine.Eval(expr);
            Assert.AreEqual("SQ", r);
        }

        [Test]
        public void TestIfNull()
        {
            string expr = "n?1:0";
            object r = engine.Eval(expr);
            Assert.AreEqual(0, r);
        }

        [Test]
        public void TestEqual()
        {
            string expr = "a0==a";
            object r = engine.Eval(expr);
            Assert.IsTrue((bool)r);
        }

        [Test]
        public void TestIfNotNull()
        {
            string expr = "dialect?1:0";
            object r = engine.Eval(expr);
            Assert.AreEqual(1, r);
        }

        [Test]
        public void TestPropertyAsIndexer()
        {
            string expr = "dialect[\"Length\"]";
            object r = engine.Eval(expr);
            Assert.AreEqual(3, r);//Note:Dialect="SQL", len is 3
        }

        [Test]
        public void TestIndexerAsProperty()
        {
            string expr = "dict.One";
            object r = engine.Eval(expr);
            Assert.AreEqual(1, r);
        }

        [Test]
        [ExpectedException(typeof(ELParseException))]
        public void ErrorHandleTest1()
        {
            string expr = "x d";
            object res = engine.Eval(expr);
        }

        [Test]
        [ExpectedException(typeof(ELParseException))]
        public void ErrorHandleTest2()
        {
            string expr = "x+y)";
            object res = engine.Eval(expr);
        }

        [Test]
        [ExpectedException(typeof(ELParseException))]
        public void ErrorHandleTest3()
        {
            string expr = "c[0]]";
            object res = engine.Eval(expr);
        }

        [Test]
        public void ArrayTest()
        {
            string expr = "array[1,2,3]";

            object res = engine.Eval(expr);
            object[] array = res as object[];
            Assert.AreEqual(3, array.Length);
            Assert.AreEqual(1, array[0]);
            Assert.AreEqual(2, array[1]);
        }

        [Test]
        public void ArrayTest1()
        {
            string expr = "array[1,2,dialect.Length][2]";

            object res = engine.Eval(expr);

            Assert.AreEqual(3, res);
        }

        [Test]
        public void HashTest()
        {
            string expr = "hash[a:1,b:dialect].a";

            object res = engine.Eval(expr);

            Assert.AreEqual(1, res);
        }

        [Test]
        public void HashTest2()
        {
            string expr = "hash[a:1,b:dialect[\"Length\"]].b";

            object res = engine.Eval(expr);

            Assert.AreEqual(3, res);
        }

        [Test]
        public void AutoArrayTest1()
        {
            //[-1,0,1,2,3]
            string expr = "array[-1:numbers.Count].Length";
            object res = engine.Eval(expr);
            Assert.AreEqual(5, res);
        }

        [Test]
        public void AutoArrayTest2()
        {
            //[-1,0,1,2,3]
            string expr = "array[-1:numbers.Count][2]";
            object res = engine.Eval(expr);
            Assert.AreEqual(1, res);
        }

        [Test]
        public void AutoArrayTest3()
        {
            //[1,0,-1,-2]
            string expr = "array[1:-2][2]";
            object res = engine.Eval(expr);
            Assert.AreEqual(-1, res);
        }

        [Test]
        public void StaticFieldTest()
        {
            string expr = "System::Int32.MaxValue";
            object res = engine.Eval(expr);
            Assert.AreEqual(Int32.MaxValue, res);
        }

        [Test]
        public void EnumTest()
        {
            string expr = "System::ConsoleKey.A";
            object res = engine.Eval(expr);
            Assert.AreEqual(ConsoleKey.A, res);
        }

        [Test]
        public void StaticPropertyTest()
        {
            string expr = "UnitTest::ExpressionLanguage::M.P";
            object res = engine.Eval(expr);

            Assert.AreEqual(3.14, res);
        }

        [Test]
        public void StaticMethodTest1()
        {
            string expr = "System::Int32.Parse(\"10\")";
            object res = engine.Eval(expr);

            Assert.AreEqual(10, res);
        }

        [Test]
        public void StaticMethodTest2()
        {
            string expr = "System::Type.GetType(\"System.Int32\")";
            object res = engine.Eval(expr);

            Assert.AreEqual(typeof(int), res);
        }

        [Test]
        public void PowerTest1()
        {
            string expr = "3^2";
            object res = engine.Eval(expr);

            Assert.AreEqual(9, res);
        }

        [Test]
        public void PowerTest2()
        {
            string expr = "2*3^2";
            object res = engine.Eval(expr);

            Assert.AreEqual(18, res);
        }

        [Test]
        public void PowerTest3()
        {
            string expr = "2^3^2";
            object res = engine.Eval(expr);

            Assert.AreEqual(64, res);
        }

        [Test]
        public void PowerTest4()
        {
            string expr = "2^(3^2)";
            object res = engine.Eval(expr);

            Assert.AreEqual(512, res);
        }

        [Test]
        public void StringMultipleTest()
        {
            string expr = "\"Hi\"*4";
            object res = engine.Eval(expr);
            Assert.AreEqual("HiHiHiHi", res);
        }

        [Test]
        public void UnaryTest1()
        {
            string expr = "!true";
            object res = engine.Eval(expr);
            Assert.AreEqual(false, res);
        }

        [Test]
        public void UnaryTest2()
        {
            string expr = "!(2==3)";
            object res = engine.Eval(expr);
            Assert.AreEqual(true, res);
        }

        [Test]
        public void UnaryTest3()
        {
            string expr = "!(!x)";
            object res = engine.Eval(expr);
            Assert.AreEqual(false, res);
        }

        [Test]
        public void ModTest()
        {
            string expr = "8%6==2";
            object res = engine.Eval(expr);
            Assert.AreEqual(true, res);
        }

        [Test]
        public void SQuoteTest1()
        {
            string expr = "'Hell\"o'";
            object res = engine.Eval(expr);
            Assert.AreEqual("Hell\"o", res);
        }

        [Test]
        public void SQuoteTest2()
        {
            string expr = "\"Hell'o\"";
            object res = engine.Eval(expr);
            Assert.AreEqual("Hell'o", res);
        }

        [Test]
        public void DQuotEscapeTest()
        {
            string expr = "\"Hell\"\"o\"";
            object res = engine.Eval(expr);
            Assert.AreEqual("Hell\"o", res);
        }

        [Test]
        public void SQuotEscapeTest()
        {
            string expr = "'Hell''o'";
            object res = engine.Eval(expr);
            Assert.AreEqual("Hell'o", res);
        }

        [Test]
        public void RegexTest()
        {
            string expr = @"regex['[0-9]{3}'].IsMatch('123')";
            object res = engine.Eval(expr);
            Assert.AreEqual(true, res);
        }

        [Test]
        public void NewTest()
        {
            string expr = @"new[System::DateTime 2000,1,1]";
            object res = engine.Eval(expr);
            Assert.AreEqual(DateTime.Parse("2000-1-1"), res);
        }

        [Test]
        public void NewParamlessTest()
        {
            string expr = @"new[System::Text::StringBuilder]";
            object res = engine.Eval(expr);
            System.Text.StringBuilder str = res as System.Text.StringBuilder;
            Assert.IsNotNull(str);
            Assert.IsTrue(str.Length == 0);
        }
    }
}
