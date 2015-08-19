using System;

using NUnit.Framework;

using RexToy.Template.Tokens;

namespace UnitTest.Template
{
    [TestFixture]
    public class LexicalParseTest
    {
        [Test]
        public void TestSimple()
        {
            LexicalParser p = new LexicalParser();
            p.SetParseContent("#{let a=3}");
            var l = p.Parse();
            Assert.AreEqual(TokenType.Let, l[0].TokenType);
            Assert.AreEqual(1, l.Count);
        }

        [Test]
        public void TestBeginColon()
        {
            LexicalParser p = new LexicalParser();
            p.SetParseContent("#{:include \"head.tpl\"}");
            var l = p.Parse();
            Assert.AreEqual(TokenType.Include, l[0].TokenType);
            Assert.AreEqual(1, l.Count);
        }

        [Test]
        public void TestEndColon()
        {
            LexicalParser p = new LexicalParser();
            p.SetParseContent("#{//Comment Line.:}");
            var l = p.Parse();
            Assert.AreEqual(TokenType.Remark, l[0].TokenType);
            Assert.AreEqual(1, l.Count);
        }

        [Test]
        public void TestCombine()
        {
            LexicalParser p = new LexicalParser();
            p.SetParseContent("#{if c>0}positive#{end}");
            var l = p.Parse();
            Assert.AreEqual(TokenType.If, l[0].TokenType);
            Assert.AreEqual(TokenType.Text, l[1].TokenType);
            Assert.AreEqual(TokenType.End, l[2].TokenType);
            Assert.AreEqual(3, l.Count);
        }

        [Test]
        public void TestEscape()
        {
            LexicalParser p = new LexicalParser();
            p.SetParseContent("##{content}");
            var l = p.Parse();
            Assert.AreEqual(TokenType.Text, l[0].TokenType);
            Assert.AreEqual(1, l.Count);
        }

        [Test]
        public void TestRemoveNextSpace()
        {
            LexicalParser p = new LexicalParser();
            p.SetParseContent("#{if i>5:}  \r\n\ti>5\r\n#{end}");
            var l = p.Parse();
            Assert.AreEqual(TokenType.Text, l[1].TokenType);
            Assert.AreEqual(3, l.Count);

            var text = l[1];
            Assert.AreEqual("\ti>5\r\n", text.TokenValue);
        }

        [Test]
        public void TestRemovePrevSpace()
        {
            LexicalParser p = new LexicalParser();
            p.SetParseContent("#{if i>5}  \r\n\ti>5\r\n#{:end}");
            var l = p.Parse();
            Assert.AreEqual(TokenType.Text, l[1].TokenType);
            Assert.AreEqual(3, l.Count);

            var text = l[1];
            Assert.AreEqual("  \r\n\ti>5", text.TokenValue);
        }

        [Test]
        public void TestRemoveBothSpace()
        {
            LexicalParser p = new LexicalParser();
            p.SetParseContent("#{if i>5:}  \r\n\ti>5\r\n  #{:end}");
            var l = p.Parse();
            Assert.AreEqual(TokenType.Text, l[1].TokenType);
            Assert.AreEqual(3, l.Count);

            var text = l[1];
            Assert.AreEqual("\ti>5", text.TokenValue);
        }
    }
}
