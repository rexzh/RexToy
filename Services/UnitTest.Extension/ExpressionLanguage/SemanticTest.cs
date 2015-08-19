using System;
using System.Collections.Generic;
using NUnit.Framework;

using RexToy;
using RexToy.Compiler.Lexical;
using RexToy.ExpressionLanguage;
using RexToy.ExpressionLanguage.AST;
using RexToy.ExpressionLanguage.Tokens;

namespace UnitTest.ExpressionLanguage
{
    [TestFixture]
    public class SemanticTest
    {
        [Test]
        public void SimpleTest1()
        {
            string expr = ExpressionsForTest.Expr1;
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(expr);
            List<Token<TokenType>> tokens = lp.Parse();

            SymanticParser sp = new SymanticParser();
            sp.SetParseContent(tokens);
            ExpressionLanguageAST ast = sp.Parse();

            Assert.IsTrue(ast.Root is MethodNode);
            MethodNode mNode = ast.Root as MethodNode;
            Assert.IsTrue(mNode.Variable is SimpleNode);
        }

        [Test]
        public void SimpleTest2()
        {
            string expr = ExpressionsForTest.Expr2;
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(expr);
            List<Token<TokenType>> tokens = lp.Parse();

            SymanticParser sp = new SymanticParser();
            sp.SetParseContent(tokens);
            ExpressionLanguageAST ast = sp.Parse();

            Assert.IsTrue(ast.Root is OperatorNode);
            OperatorNode opNode = ast.Root as OperatorNode;
            Assert.AreEqual("+", opNode.Token.TokenValue);
            Assert.IsTrue(opNode.Lhs is SimpleNode);
            Assert.IsTrue(opNode.Rhs is SimpleNode);
        }

        [Test]
        public void SimpleTest3()
        {
            string expr = ExpressionsForTest.Expr3;
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(expr);
            List<Token<TokenType>> tokens = lp.Parse();

            SymanticParser sp = new SymanticParser();
            sp.SetParseContent(tokens);
            ExpressionLanguageAST ast = sp.Parse();

            Assert.IsTrue(ast.Root is IndexerNode);
            IndexerNode iNode = ast.Root as IndexerNode;
            Assert.IsTrue(iNode.Variable is SimpleNode);
            Assert.IsTrue((iNode.Args as ParamListNode).Values.Count == 1);
        }

        [Test]
        public void SimpleTest4()
        {
            string expr = ExpressionsForTest.Expr4;
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(expr);
            List<Token<TokenType>> tokens = lp.Parse();

            SymanticParser sp = new SymanticParser();
            sp.SetParseContent(tokens);
            ExpressionLanguageAST ast = sp.Parse();

            Assert.IsTrue(ast.Root is OperatorNode);
        }

        [Test]
        public void SimpleTest5()
        {
            string expr = ExpressionsForTest.Expr5;
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(expr);
            List<Token<TokenType>> tokens = lp.Parse();

            SymanticParser sp = new SymanticParser();
            sp.SetParseContent(tokens);
            ExpressionLanguageAST ast = sp.Parse();

            Assert.IsTrue(ast.Root is OperatorNode);
        }

        [Test]
        public void SimpleTest6()
        {
            string expr = ExpressionsForTest.Expr6;
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(expr);
            List<Token<TokenType>> tokens = lp.Parse();

            SymanticParser sp = new SymanticParser();
            sp.SetParseContent(tokens);
            ExpressionLanguageAST ast = sp.Parse();

            Assert.IsTrue(ast.Root is MethodNode);
            MethodNode mNode = ast.Root as MethodNode;
            ParamListNode plNode = mNode.Args as ParamListNode;
            Assert.IsTrue(plNode.Values.Count == 2);
        }

        [Test]
        public void SimpleTest7()
        {
            string expr = ExpressionsForTest.Expr7;
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(expr);
            List<Token<TokenType>> tokens = lp.Parse();

            SymanticParser sp = new SymanticParser();
            sp.SetParseContent(tokens);
            ExpressionLanguageAST ast = sp.Parse();

            Assert.IsTrue(ast.Root is TernaryNode);
            TernaryNode tNode = ast.Root as TernaryNode;
            Assert.IsTrue(tNode.Condition is OperatorNode);
            Assert.IsTrue(tNode.TrueValue is SimpleNode);
            Assert.IsTrue(tNode.FalseValue is SimpleNode);
        }

        [Test]
        public void SimpleTest8()
        {
            string expr = ExpressionsForTest.Expr8;
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(expr);
            List<Token<TokenType>> tokens = lp.Parse();

            SymanticParser sp = new SymanticParser();
            sp.SetParseContent(tokens);
            ExpressionLanguageAST ast = sp.Parse();

            Assert.IsTrue(ast.Root is TernaryNode);
            TernaryNode tNode = ast.Root as TernaryNode;
            Assert.IsTrue(tNode.Condition is OperatorNode);
            Assert.IsTrue(tNode.TrueValue is IndexerNode);
            Assert.IsTrue(tNode.FalseValue is SimpleNode);
        }

        [Test]
        public void SimpleTest9()
        {
            string expr = ExpressionsForTest.Expr9;
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(expr);
            List<Token<TokenType>> tokens = lp.Parse();

            SymanticParser sp = new SymanticParser();
            sp.SetParseContent(tokens);
            ExpressionLanguageAST ast = sp.Parse();

            Assert.IsTrue(ast.Root is TernaryNode);
            TernaryNode tNode = ast.Root as TernaryNode;
            Assert.IsTrue(tNode.TrueValue is SimpleNode);
            Assert.IsTrue(tNode.FalseValue is SimpleNode);
            OperatorNode opNode = tNode.Condition as OperatorNode;
            Assert.IsTrue(opNode.Lhs is OperatorNode);
            Assert.IsTrue(opNode.Rhs is SimpleNode);
        }

        [Test]
        public void SimpleTest10()
        {
            string expr = ExpressionsForTest.Expr10;
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(expr);
            List<Token<TokenType>> tokens = lp.Parse();

            SymanticParser sp = new SymanticParser();
            sp.SetParseContent(tokens);
            ExpressionLanguageAST ast = sp.Parse();

            Assert.IsTrue(ast.Root is MethodNode);
            MethodNode mNode = ast.Root as MethodNode;
            Assert.IsTrue(mNode.Variable is PropertyNode);
            PropertyNode pNode = mNode.Variable as PropertyNode;
            Assert.IsTrue(pNode.Variable is MethodNode);
        }

        [Test]
        public void SimpleTest11()
        {
            string expr = ExpressionsForTest.Expr11;
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(expr);
            List<Token<TokenType>> tokens = lp.Parse();

            SymanticParser sp = new SymanticParser();
            sp.SetParseContent(tokens);
            ExpressionLanguageAST ast = sp.Parse();

            Assert.IsTrue(ast.Root is OperatorNode);
            OperatorNode opNode = ast.Root as OperatorNode;
            Assert.IsTrue(opNode.Lhs is UnaryNode);
            Assert.IsTrue(opNode.Rhs is SimpleNode);
        }

        [Test]
        public void SimpleTest12()
        {
            string expr = ExpressionsForTest.Expr12;
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(expr);
            List<Token<TokenType>> tokens = lp.Parse();

            SymanticParser sp = new SymanticParser();
            sp.SetParseContent(tokens);
            ExpressionLanguageAST ast = sp.Parse();

            Assert.IsTrue(ast.Root is OperatorNode);
            OperatorNode opNode = ast.Root as OperatorNode;
            Assert.IsTrue(opNode.Lhs is UnaryNode);
            Assert.IsTrue(opNode.Rhs is OperatorNode);
            Assert.IsTrue((opNode.Rhs as OperatorNode).Rhs is UnaryNode);
        }

        [Test]
        public void SimpleTest13()
        {
            string expr = ExpressionsForTest.Expr13;
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(expr);
            List<Token<TokenType>> tokens = lp.Parse();

            SymanticParser sp = new SymanticParser();
            sp.SetParseContent(tokens);
            ExpressionLanguageAST ast = sp.Parse();

            Assert.IsTrue(ast.Root is PropertyNode);
            PropertyNode pNode = ast.Root as PropertyNode;
            pNode = pNode.Variable as PropertyNode;
            Assert.IsTrue(pNode.Variable is IndexerNode);
        }

        [Test]
        public void SimpleTest14()
        {
            string expr = ExpressionsForTest.Expr14;
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(expr);
            List<Token<TokenType>> tokens = lp.Parse();

            SymanticParser sp = new SymanticParser();
            sp.SetParseContent(tokens);
            ExpressionLanguageAST ast = sp.Parse();
            
            Assert.IsTrue(ast.Root is RegexNode);
        }

        [Test]
        public void SimpleTest15()
        {
            string expr = ExpressionsForTest.Expr15;
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(expr);
            List<Token<TokenType>> tokens = lp.Parse();

            SymanticParser sp = new SymanticParser();
            sp.SetParseContent(tokens);
            ExpressionLanguageAST ast = sp.Parse();

            Assert.IsTrue(ast.Root is HashNode);
        }

        [Test]
        public void SimpleTest16()
        {
            string expr = ExpressionsForTest.Expr16;
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(expr);
            List<Token<TokenType>> tokens = lp.Parse();

            SymanticParser sp = new SymanticParser();
            sp.SetParseContent(tokens);
            ExpressionLanguageAST ast = sp.Parse();

            Assert.IsTrue(ast.Root is AutoIntArrayNode);
        }

        [Test]
        public void SimpleTest17()
        {
            string expr = ExpressionsForTest.Expr17;
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(expr);
            List<Token<TokenType>> tokens = lp.Parse();

            SymanticParser sp = new SymanticParser();
            sp.SetParseContent(tokens);
            ExpressionLanguageAST ast = sp.Parse();

            Assert.IsTrue(ast.Root is ArrayNode);
        }

        [Test]
        public void TernaryTest1()
        {
            string expr = "a?(b?c:d):e";
            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(expr);
            List<Token<TokenType>> tokens = lp.Parse();

            SymanticParser sp = new SymanticParser();
            sp.SetParseContent(tokens);
            ExpressionLanguageAST ast = sp.Parse();

            TernaryNode tNode = ast.Root as TernaryNode;
            Assert.IsTrue(tNode.TrueValue is TernaryNode);
        }
    }
}
