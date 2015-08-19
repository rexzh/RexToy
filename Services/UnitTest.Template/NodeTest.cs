using System;
using System.Collections.Generic;

using NUnit.Framework;

using RexToy.Template;
using RexToy.Template.AST;

namespace UnitTest.Template
{
    [TestFixture]
    public class NodeTest
    {
        [Test]
        public void TestForNode()
        {
            string t = "#{ for i  in array[3  :8]}#{i }#{end}";
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(t);
            var tokens = lp.Parse();
            ForNode f = new ForNode(tokens[0]);
            Assert.AreEqual("i", f.Var);
            Assert.AreEqual("array[3  :8]", f.Enumerable);
        }

        [Test, ExpectedException(typeof(TemplateParseException), ExpectedMessage = "Keyword [in] expected.")]
        public void TestForNodeLackInKeyword()
        {
            string t = "#{for i array[1, 2, 3]}";
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(t);
            var tokens = lp.Parse();
            ForNode f = new ForNode(tokens[0]);
        }

        [Test, ExpectedException(typeof(TemplateParseException), ExpectedMessage = "[$] is not a valid var name.")]
        public void TestForNodeInvalidVarName()
        {
            string t = "#{for $ in array[1, 2, 3]}";
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(t);
            var tokens = lp.Parse();
            ForNode f = new ForNode(tokens[0]);
        }

        [Test]
        public void TestForNodeWithColon()
        {
            string t = "#{:for i in array[1 : 10]:}";
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(t);
            var tokens = lp.Parse();
            ForNode f = new ForNode(tokens[0]);
        }

        [Test]
        public void TestIfNode()
        {
            string t = "#{if a}#{end}";
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(t);
            var tokens = lp.Parse();
            IfNode i = new IfNode(tokens[0]);
            Assert.AreEqual("a", i.Expr);
        }

        [Test, ExpectedException(typeof(TemplateParseException), ExpectedMessage = "Syntax error in token [if ].")]
        public void TestIfNodeNoExpr()
        {
            string t = "#{if }#{end}";
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(t);
            var tokens = lp.Parse();
            IfNode i = new IfNode(tokens[0]);
        }

        [Test]
        public void TestLetNode()
        {
            string t = "#{let c = tuple[x:3, y:5]}";
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(t);
            var tokens = lp.Parse();
            LetNode l = new LetNode(tokens[0]);
            Assert.AreEqual("c", l.VarName);
            Assert.AreEqual("tuple[x:3, y:5]", l.Expression);
        }

        [Test, ExpectedException(typeof(TemplateParseException), ExpectedMessage = "Keyword [=] expected.")]
        public void TestLetNodeNoEqualOperator()
        {
            string t = "#{let c   tuple[x:3, y:5]}";
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(t);
            var tokens = lp.Parse();
            LetNode l = new LetNode(tokens[0]);
        }

        [Test, ExpectedException(typeof(TemplateParseException), ExpectedMessage = "[] is not a valid var name.")]
        public void TestLetNodeNoVarName()
        {
            string t = "#{let =   tuple[x:3, y:5]}";
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(t);
            var tokens = lp.Parse();
            LetNode l = new LetNode(tokens[0]);
        }

        [Test, ExpectedException(typeof(TemplateParseException), ExpectedMessage = "Syntax error in token [let c=   ].")]
        public void TestLetNodeNoExpr()
        {
            string t = "#{let c=   }";
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(t);
            var tokens = lp.Parse();
            LetNode l = new LetNode(tokens[0]);
        }
    }
}
