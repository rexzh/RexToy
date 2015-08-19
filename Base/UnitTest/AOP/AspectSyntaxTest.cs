using System;
using System.Collections.Generic;
using System.Reflection;

using NUnit.Framework;

using RexToy;
using RexToy.Compiler.Lexical;
using RexToy.Configuration;
using RexToy.AOP;

namespace UnitTest.AOP
{
    [TestFixture]
    public class AspectSyntaxTest
    {
        [TestFixtureSetUp]
        public void SetUp()
        {
            AppConfig.Destroy();
            AppConfig.Load(ConfigFactory.CreateXmlConfig(@"config.xml"));
        }

        class Test
        {
            void Run()
            {
            }

            public int Foo(int c, int d)
            {
                return c + d;
            }
        }

        private static MethodInfo mRun = typeof(Test).GetMethod("Run", BindingFlags.Instance | BindingFlags.NonPublic);
        private static MethodInfo mFoo = typeof(Test).GetMethod("Foo");

        [Test]
        public void TestAccessModifierMatch()
        {
            JoinPointDefination d = new JoinPointDefination(Position.Before, string.Empty, "* void ..*.Run(..)");
            Assert.IsTrue(d.Match(mRun));
        }

        [Test]
        public void TestAccessModifierNotMatch()
        {
            JoinPointDefination d = new JoinPointDefination(Position.Before, string.Empty, "public void ..*.Run(..)");
            Assert.IsFalse(d.Match(mRun));
        }

        [Test]
        public void TestReturnTypeMatch()
        {
            JoinPointDefination d = new JoinPointDefination(Position.Before, string.Empty, "* void ..*.Run()");
            Assert.IsTrue(d.Match(mRun));
        }

        [Test]
        public void TestReturnTypeNotMatch()
        {
            JoinPointDefination d = new JoinPointDefination(Position.Before, string.Empty, "public void ..Test.Foo(Int32,Int32)");
            Assert.IsFalse(d.Match(mFoo));
        }

        [Test]
        public void TestNamespaceMatch()
        {
            JoinPointDefination d = new JoinPointDefination(Position.Before, string.Empty, "* * UnitTest..Test.Foo(..)");
            Assert.IsTrue(d.Match(mFoo));
        }

        [Test]
        public void TestNamespaceNotMatch()
        {
            JoinPointDefination d = new JoinPointDefination(Position.Before, string.Empty, "* * RexToy..Test.Foo(..)");
            Assert.IsFalse(d.Match(mFoo));
        }

        [Test]
        public void TestParamMatchAny()
        {
            JoinPointDefination d = new JoinPointDefination(Position.Before, string.Empty, "* * ..Test.Run(..)");
            Assert.IsTrue(d.Match(mRun));
        }

        [Test]
        public void TestParamMatchExact()
        {
            JoinPointDefination d = new JoinPointDefination(Position.Before, string.Empty, "* * ..Test.Foo(Int32, Int32)");
            Assert.IsTrue(d.Match(mFoo));
        }

        [Test]
        public void TestParamMatch()
        {
            JoinPointDefination d = new JoinPointDefination(Position.Before, string.Empty, "* * ..Test.Foo(Int32, *)");
            Assert.IsTrue(d.Match(mFoo));
        }

        [Test]
        public void TestParamNotMatch()
        {
            JoinPointDefination d = new JoinPointDefination(Position.Before, string.Empty, "* * ..Test.Foo()");
            Assert.IsFalse(d.Match(mFoo));
        }
    }
}
