using System;
using System.Collections.Generic;
using NUnit.Framework;

using RexToy.Compiler.Lexical;
using RexToy.ExpressionLanguage;
using RexToy.ExpressionLanguage.Tokens;

namespace UnitTest.ExpressionLanguage
{
    [TestFixture]
    public class LexicalTest
    {
        [Test]
        public void SimpleTest1()
        {
            string expr = ExpressionsForTest.Expr1;
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(expr);
            List<Token<TokenType>> tokens = lp.Parse();

            Assert.AreEqual("DateTime", tokens[0].TokenValue);
            Assert.AreEqual(TokenType.ID, tokens[0].TokenType);
            Assert.AreEqual(".", tokens[1].TokenValue);
            Assert.AreEqual(TokenType.Dot, tokens[1].TokenType);
            Assert.AreEqual("Parse", tokens[2].TokenValue);
            Assert.AreEqual(TokenType.ID, tokens[2].TokenType);
            Assert.AreEqual("(", tokens[3].TokenValue);
            Assert.AreEqual(TokenType.Left_Parenthesis, tokens[3].TokenType);
            Assert.AreEqual("2000-1-1", tokens[4].TokenValue);
            Assert.AreEqual(TokenType.String, tokens[4].TokenType);
            Assert.AreEqual(")", tokens[5].TokenValue);
            Assert.AreEqual(TokenType.Right_Parenthesis, tokens[5].TokenType);

            Assert.AreEqual(0, tokens[0].Position);
            Assert.AreEqual(8, tokens[1].Position);
            Assert.AreEqual(15, tokens[4].Position);
        }

        [Test]
        public void SimpleTest2()
        {
            string expr = ExpressionsForTest.Expr2;
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(expr);
            List<Token<TokenType>> tokens = lp.Parse();

            Assert.AreEqual("3.2", tokens[0].TokenValue);
            Assert.AreEqual(TokenType.Decimal, tokens[0].TokenType);
            Assert.AreEqual("+", tokens[1].TokenValue);
            Assert.AreEqual(TokenType.ArithmeticOperator, tokens[1].TokenType);
            Assert.AreEqual("1.5", tokens[2].TokenValue);
            Assert.AreEqual(TokenType.Decimal, tokens[2].TokenType);
        }

        [Test]
        public void SimpleTest3()
        {
            string expr = ExpressionsForTest.Expr3;
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(expr);
            List<Token<TokenType>> tokens = lp.Parse();

            Assert.AreEqual("_table", tokens[0].TokenValue);
            Assert.AreEqual(TokenType.ID, tokens[0].TokenType);
            Assert.AreEqual("[", tokens[1].TokenValue);
            Assert.AreEqual(TokenType.Left_Square_Bracket, tokens[1].TokenType);
            Assert.AreEqual("key", tokens[2].TokenValue);
            Assert.AreEqual(TokenType.String, tokens[2].TokenType);
            Assert.AreEqual("]", tokens[3].TokenValue);
            Assert.AreEqual(TokenType.Right_Square_Bracket, tokens[3].TokenType);
        }

        [Test]
        public void SimpleTest4()
        {
            string expr = ExpressionsForTest.Expr4;
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(expr);
            List<Token<TokenType>> tokens = lp.Parse();

            Assert.AreEqual("Key", tokens[0].TokenValue);
            Assert.AreEqual(TokenType.String, tokens[0].TokenType);
            Assert.AreEqual("+", tokens[1].TokenValue);
            Assert.AreEqual(TokenType.ArithmeticOperator, tokens[1].TokenType);
            Assert.AreEqual("Value", tokens[2].TokenValue);
            Assert.AreEqual(TokenType.String, tokens[2].TokenType);
        }

        [Test]
        public void SimpleTest5()
        {
            string expr = ExpressionsForTest.Expr5;
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(expr);
            List<Token<TokenType>> tokens = lp.Parse();

            Assert.AreEqual("1.2", tokens[0].TokenValue);
            Assert.AreEqual(TokenType.Decimal, tokens[0].TokenType);
            Assert.AreEqual("+", tokens[1].TokenValue);
            Assert.AreEqual(TokenType.ArithmeticOperator, tokens[1].TokenType);
            Assert.AreEqual("$", tokens[2].TokenValue);
            Assert.AreEqual(TokenType.String, tokens[2].TokenType);
        }

        [Test]
        public void SimpleTest6()
        {
            string expr = ExpressionsForTest.Expr6;
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(expr);
            List<Token<TokenType>> tokens = lp.Parse();

            Assert.AreEqual("Update", tokens[0].TokenValue);
            Assert.AreEqual(TokenType.ID, tokens[0].TokenType);
            Assert.AreEqual("(", tokens[1].TokenValue);
            Assert.AreEqual(TokenType.Left_Parenthesis, tokens[1].TokenType);
            Assert.AreEqual("1", tokens[2].TokenValue);
            Assert.AreEqual(TokenType.Long, tokens[2].TokenType);
            Assert.AreEqual(",", tokens[3].TokenValue);
            Assert.AreEqual(TokenType.Comma, tokens[3].TokenType);
            Assert.AreEqual("true", tokens[4].TokenValue);
            Assert.AreEqual(TokenType.Boolean, tokens[4].TokenType);
            Assert.AreEqual(")", tokens[5].TokenValue);
            Assert.AreEqual(TokenType.Right_Parenthesis, tokens[5].TokenType);
        }

        [Test]
        public void SimpleTest7()
        {
            string expr = ExpressionsForTest.Expr7;
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(expr);
            List<Token<TokenType>> tokens = lp.Parse();

            Assert.AreEqual("(", tokens[0].TokenValue);
            Assert.AreEqual(TokenType.Left_Parenthesis, tokens[0].TokenType);
            Assert.AreEqual("a", tokens[1].TokenValue);
            Assert.AreEqual(TokenType.ID, tokens[1].TokenType);
            Assert.AreEqual(">", tokens[2].TokenValue);
            Assert.AreEqual(TokenType.CompareOperator, tokens[2].TokenType);
            Assert.AreEqual("b", tokens[3].TokenValue);
            Assert.AreEqual(TokenType.ID, tokens[3].TokenType);
            Assert.AreEqual(")", tokens[4].TokenValue);
            Assert.AreEqual(TokenType.Right_Parenthesis, tokens[4].TokenType);
            Assert.AreEqual("?", tokens[5].TokenValue);
            Assert.AreEqual(TokenType.QuestionMark, tokens[5].TokenType);
            Assert.AreEqual("a", tokens[6].TokenValue);
            Assert.AreEqual(TokenType.ID, tokens[6].TokenType);
            Assert.AreEqual(":", tokens[7].TokenValue);
            Assert.AreEqual(TokenType.Colon, tokens[7].TokenType);
            Assert.AreEqual("b", tokens[8].TokenValue);
            Assert.AreEqual(TokenType.ID, tokens[8].TokenType);
        }

        [Test]
        public void SimpleTest8()
        {
            string expr = ExpressionsForTest.Expr8;
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(expr);
            List<Token<TokenType>> tokens = lp.Parse();

            Assert.AreEqual("(", tokens[0].TokenValue);
            Assert.AreEqual(TokenType.Left_Parenthesis, tokens[0].TokenType);
            Assert.AreEqual("a", tokens[1].TokenValue);
            Assert.AreEqual(TokenType.ID, tokens[1].TokenType);
            Assert.AreEqual("!=", tokens[2].TokenValue);
            Assert.AreEqual(TokenType.CompareOperator, tokens[2].TokenType);
            Assert.AreEqual("0", tokens[3].TokenValue);
            Assert.AreEqual(TokenType.Long, tokens[3].TokenType);
            Assert.AreEqual(")", tokens[4].TokenValue);
            Assert.AreEqual(TokenType.Right_Parenthesis, tokens[4].TokenType);
            Assert.AreEqual("?", tokens[5].TokenValue);
            Assert.AreEqual(TokenType.QuestionMark, tokens[5].TokenType);
            Assert.AreEqual("tb", tokens[6].TokenValue);
            Assert.AreEqual(TokenType.ID, tokens[6].TokenType);
            Assert.AreEqual("[", tokens[7].TokenValue);
            Assert.AreEqual(TokenType.Left_Square_Bracket, tokens[7].TokenType);
            Assert.AreEqual("a", tokens[8].TokenValue);
            Assert.AreEqual(TokenType.ID, tokens[8].TokenType);
            Assert.AreEqual("]", tokens[9].TokenValue);
            Assert.AreEqual(TokenType.Right_Square_Bracket, tokens[9].TokenType);
            Assert.AreEqual(":", tokens[10].TokenValue);
            Assert.AreEqual(TokenType.Colon, tokens[10].TokenType);
            Assert.AreEqual("b", tokens[11].TokenValue);
            Assert.AreEqual(TokenType.ID, tokens[11].TokenType);
        }

        [Test]
        public void SimpleTest9()
        {
            string expr = ExpressionsForTest.Expr9;
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(expr);
            List<Token<TokenType>> tokens = lp.Parse();

            Assert.AreEqual("(", tokens[0].TokenValue);
            Assert.AreEqual(TokenType.Left_Parenthesis, tokens[0].TokenType);
            Assert.AreEqual("a", tokens[1].TokenValue);
            Assert.AreEqual(TokenType.ID, tokens[1].TokenType);
            Assert.AreEqual("+", tokens[2].TokenValue);
            Assert.AreEqual(TokenType.ArithmeticOperator, tokens[2].TokenType);
            Assert.AreEqual("b", tokens[3].TokenValue);
            Assert.AreEqual(TokenType.ID, tokens[3].TokenType);
            Assert.AreEqual(">", tokens[4].TokenValue);
            Assert.AreEqual(TokenType.CompareOperator, tokens[4].TokenType);
            Assert.AreEqual("0", tokens[5].TokenValue);
            Assert.AreEqual(TokenType.Long, tokens[5].TokenType);
            Assert.AreEqual(")", tokens[6].TokenValue);
            Assert.AreEqual(TokenType.Right_Parenthesis, tokens[6].TokenType);
            Assert.AreEqual("?", tokens[7].TokenValue);
            Assert.AreEqual(TokenType.QuestionMark, tokens[7].TokenType);
            Assert.AreEqual("T", tokens[8].TokenValue);
            Assert.AreEqual(TokenType.String, tokens[8].TokenType);
            Assert.AreEqual(":", tokens[9].TokenValue);
            Assert.AreEqual(TokenType.Colon, tokens[9].TokenType);
            Assert.AreEqual("F", tokens[10].TokenValue);
            Assert.AreEqual(TokenType.String, tokens[10].TokenType);
        }

        [Test]
        public void SimpleTest10()
        {
            string expr = ExpressionsForTest.Expr10;
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(expr);
            List<Token<TokenType>> tokens = lp.Parse();

            Assert.AreEqual("GetData", tokens[0].TokenValue);
            Assert.AreEqual(TokenType.ID, tokens[0].TokenType);
            Assert.AreEqual("(", tokens[1].TokenValue);
            Assert.AreEqual(TokenType.Left_Parenthesis, tokens[1].TokenType);
            Assert.AreEqual("rex", tokens[2].TokenValue);
            Assert.AreEqual(TokenType.String, tokens[2].TokenType);
            Assert.AreEqual(")", tokens[3].TokenValue);
            Assert.AreEqual(TokenType.Right_Parenthesis, tokens[3].TokenType);
            Assert.AreEqual(".", tokens[4].TokenValue);
            Assert.AreEqual(TokenType.Dot, tokens[4].TokenType);
            Assert.AreEqual("Name", tokens[5].TokenValue);
            Assert.AreEqual(TokenType.ID, tokens[5].TokenType);
            Assert.AreEqual(".", tokens[6].TokenValue);
            Assert.AreEqual(TokenType.Dot, tokens[6].TokenType);
            Assert.AreEqual("ToString", tokens[7].TokenValue);
            Assert.AreEqual(TokenType.ID, tokens[7].TokenType);
            Assert.AreEqual("(", tokens[8].TokenValue);
            Assert.AreEqual(TokenType.Left_Parenthesis, tokens[8].TokenType);
            Assert.AreEqual(")", tokens[9].TokenValue);
            Assert.AreEqual(TokenType.Right_Parenthesis, tokens[9].TokenType);

            Assert.AreEqual(15, tokens[5].Position);
            Assert.AreEqual(20, tokens[7].Position);
        }

        [Test]
        public void SimpleTest11()
        {
            string expr = ExpressionsForTest.Expr11;
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(expr);
            List<Token<TokenType>> tokens = lp.Parse();

            Assert.AreEqual("-", tokens[0].TokenValue);
            Assert.AreEqual(TokenType.ArithmeticOperator, tokens[0].TokenType);
            Assert.AreEqual("(", tokens[1].TokenValue);
            Assert.AreEqual(TokenType.Left_Parenthesis, tokens[1].TokenType);
            Assert.AreEqual("a", tokens[2].TokenValue);
            Assert.AreEqual(TokenType.ID, tokens[2].TokenType);
            Assert.AreEqual("+", tokens[3].TokenValue);
            Assert.AreEqual(TokenType.ArithmeticOperator, tokens[3].TokenType);
            Assert.AreEqual("7", tokens[4].TokenValue);
            Assert.AreEqual(TokenType.Long, tokens[4].TokenType);
            Assert.AreEqual(")", tokens[5].TokenValue);
            Assert.AreEqual(TokenType.Right_Parenthesis, tokens[5].TokenType);
            Assert.AreEqual("/", tokens[6].TokenValue);
            Assert.AreEqual(TokenType.ArithmeticOperator, tokens[6].TokenType);
            Assert.AreEqual("7", tokens[7].TokenValue);
            Assert.AreEqual(TokenType.Long, tokens[7].TokenType);
        }

        [Test]
        public void SimpleTest12()
        {
            string expr = ExpressionsForTest.Expr12;
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(expr);
            List<Token<TokenType>> tokens = lp.Parse();

            Assert.AreEqual("-", tokens[0].TokenValue);
            Assert.AreEqual(TokenType.ArithmeticOperator, tokens[0].TokenType);
            Assert.AreEqual("a", tokens[1].TokenValue);
            Assert.AreEqual(TokenType.ID, tokens[1].TokenType);
            Assert.AreEqual("+", tokens[2].TokenValue);
            Assert.AreEqual(TokenType.ArithmeticOperator, tokens[2].TokenType);
            Assert.AreEqual("7", tokens[3].TokenValue);
            Assert.AreEqual(TokenType.Long, tokens[3].TokenType);
            Assert.AreEqual("/", tokens[4].TokenValue);
            Assert.AreEqual(TokenType.ArithmeticOperator, tokens[4].TokenType);
            Assert.AreEqual("(", tokens[5].TokenValue);
            Assert.AreEqual(TokenType.Left_Parenthesis, tokens[5].TokenType);
            Assert.AreEqual("-", tokens[6].TokenValue);
            Assert.AreEqual(TokenType.ArithmeticOperator, tokens[6].TokenType);
            Assert.AreEqual("5", tokens[7].TokenValue);
            Assert.AreEqual(TokenType.Long, tokens[7].TokenType);
            Assert.AreEqual(")", tokens[8].TokenValue);
            Assert.AreEqual(TokenType.Right_Parenthesis, tokens[8].TokenType);
        }

        [Test]
        public void SimpleTest13()
        {
            string expr = ExpressionsForTest.Expr13;
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(expr);
            List<Token<TokenType>> tokens = lp.Parse();

            Assert.AreEqual("person", tokens[0].TokenValue);
            Assert.AreEqual(TokenType.ID, tokens[0].TokenType);
            Assert.AreEqual("[", tokens[1].TokenValue);
            Assert.AreEqual(TokenType.Left_Square_Bracket, tokens[1].TokenType);
            Assert.AreEqual("index", tokens[2].TokenValue);
            Assert.AreEqual(TokenType.ID, tokens[2].TokenType);
            Assert.AreEqual("]", tokens[3].TokenValue);
            Assert.AreEqual(TokenType.Right_Square_Bracket, tokens[3].TokenType);
            Assert.AreEqual(".", tokens[4].TokenValue);
            Assert.AreEqual(TokenType.Dot, tokens[4].TokenType);
            Assert.AreEqual("Name", tokens[5].TokenValue);
            Assert.AreEqual(TokenType.ID, tokens[5].TokenType);
            Assert.AreEqual(".", tokens[6].TokenValue);
            Assert.AreEqual(TokenType.Dot, tokens[6].TokenType);
            Assert.AreEqual("Last", tokens[7].TokenValue);
            Assert.AreEqual(TokenType.ID, tokens[7].TokenType);
        }

        [Test]
        public void SimpleTest14()
        {
            string expr = ExpressionsForTest.Expr14;
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(expr);
            List<Token<TokenType>> tokens = lp.Parse();

            Assert.AreEqual(TokenType.Regex, tokens[0].TokenType);
            Assert.AreEqual(TokenType.Left_Square_Bracket, tokens[1].TokenType);
            Assert.AreEqual(TokenType.String, tokens[2].TokenType);
            Assert.AreEqual(@"^\d3*$", tokens[2].TokenValue);
            Assert.AreEqual(TokenType.Right_Square_Bracket, tokens[3].TokenType);
        }

        [Test]
        public void SimpleTest15()
        {
            string expr = ExpressionsForTest.Expr15;
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(expr);
            List<Token<TokenType>> tokens = lp.Parse();

            Assert.AreEqual(TokenType.Hash, tokens[0].TokenType);
            Assert.AreEqual(TokenType.Left_Square_Bracket, tokens[1].TokenType);
            Assert.AreEqual(TokenType.ID, tokens[2].TokenType);
            Assert.AreEqual(TokenType.Colon, tokens[3].TokenType);
            Assert.AreEqual("1", tokens[4].TokenValue);
            Assert.AreEqual(TokenType.Comma, tokens[5].TokenType);
            Assert.AreEqual(TokenType.ID, tokens[6].TokenType);
            Assert.AreEqual(TokenType.Colon, tokens[7].TokenType);
            Assert.AreEqual("5", tokens[8].TokenValue);
            Assert.AreEqual(TokenType.Right_Square_Bracket, tokens[9].TokenType);
        }

        [Test]
        public void SimpleTest16()
        {
            string expr = ExpressionsForTest.Expr16;
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(expr);
            List<Token<TokenType>> tokens = lp.Parse();

            Assert.AreEqual(TokenType.Array, tokens[0].TokenType);
            Assert.AreEqual(TokenType.Left_Square_Bracket, tokens[1].TokenType);
            Assert.AreEqual("4", tokens[2].TokenValue);
            Assert.AreEqual(TokenType.Colon, tokens[3].TokenType);
            Assert.AreEqual("7", tokens[4].TokenValue);
            Assert.AreEqual(TokenType.Right_Square_Bracket, tokens[5].TokenType);
        }

        [Test]
        public void SimpleTest17()
        {
            string expr = ExpressionsForTest.Expr17;
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(expr);
            List<Token<TokenType>> tokens = lp.Parse();

            Assert.AreEqual(TokenType.Array, tokens[0].TokenType);
            Assert.AreEqual(TokenType.Left_Square_Bracket, tokens[1].TokenType);
            Assert.AreEqual("1", tokens[2].TokenValue);
            Assert.AreEqual(TokenType.Comma, tokens[3].TokenType);
            Assert.AreEqual("1", tokens[4].TokenValue);
            Assert.AreEqual(TokenType.Comma, tokens[5].TokenType);
            Assert.AreEqual("2", tokens[6].TokenValue);
            Assert.AreEqual(TokenType.Comma, tokens[7].TokenType);
            Assert.AreEqual("3", tokens[8].TokenValue);
            Assert.AreEqual(TokenType.Comma, tokens[9].TokenType);
            Assert.AreEqual("5", tokens[10].TokenValue);
            Assert.AreEqual(TokenType.Right_Square_Bracket, tokens[11].TokenType);
        }
    }
}