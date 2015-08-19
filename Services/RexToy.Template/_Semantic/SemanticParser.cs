using System;
using System.Collections.Generic;
using System.IO;

using RexToy.DesignPattern;
using RexToy.Compiler.Lexical;
using RexToy.Compiler.Semantic;
using RexToy.Template.Tokens;

namespace RexToy.Template
{
    using RexToy.Template.AST;

    internal class SemanticParser : SemanticParser<TokenType, TemplateAST>
    {
        public SemanticParser()
        {
        }

        //BNF
        //para ::= if para [else para] endif
        //para ::= for para endfor
        public override TemplateAST Parse()
        {
            TemplateAST ast = new TemplateAST(ParseParagraph());
            Token<TokenType> t = PeekNextToken();
            if (!t.End)
            {
                ExceptionHelper.ThrowParseError(t.Position);
                return null;
            }
            return ast;
        }

        private Node ParseParagraph()
        {
            Token<TokenType> t = PeekNextToken();
            ParaNode node = new ParaNode();
            while (!t.End)
            {
                switch (t.TokenType)
                {
                    case TokenType.If:
                        node.ParamNodes.Add(ParseIfBlock());
                        break;

                    case TokenType.For:
                        node.ParamNodes.Add(ParseForBlock());
                        break;

                    case TokenType.Let:
                        LetNode let = new LetNode(GetNextToken());
                        node.ParamNodes.Add(let);
                        break;

                    case TokenType.Remark:
                        RemarkNode rem = new RemarkNode(GetNextToken());
                        node.ParamNodes.Add(rem);
                        break;

                    case TokenType.Expression:
                    case TokenType.Text:
                        SimpleNode sNode = new SimpleNode(GetNextToken());
                        node.ParamNodes.Add(sNode);
                        break;

                    case TokenType.Include:
                        IncludeNode include = new IncludeNode(GetNextToken());
                        node.ParamNodes.Add(include);
                        break;

                    case TokenType.Else://Note: if next token is else, then the trueNode of if is finished.
                    case TokenType.End:
                        if (node.ParamNodes.Count == 1)
                            return node.ParamNodes[0];
                        else
                            return node;

                    case TokenType.Break:
                        GetNextToken();
                        node.ParamNodes.Add(new BreakNode());
                        break;

                    case TokenType.Continue:
                        GetNextToken();
                        node.ParamNodes.Add(new ContinueNode());
                        break;
                }

                t = PeekNextToken();
            }

            if (node.ParamNodes.Count == 1)
                return node.ParamNodes[0];
            else
                return node;
        }

        private ForNode ParseForBlock()
        {
            Token<TokenType> forToken = GetNextToken();
            ForNode forNode = new ForNode(forToken);
            forNode.BlockNode = ParseParagraph();
            var end = GetNextToken();//Note:End for
            if (end.TokenType != TokenType.End)
            {
                ExceptionHelper.ThrowForBlockNotEnd();
                return null;
            }
            return forNode;
        }

        private IfNode ParseIfBlock()
        {
            Token<TokenType> ifToken = GetNextToken();
            IfNode ifNode = new IfNode(ifToken);
            ifNode.TrueNode = ParseParagraph();
            Token<TokenType> t = PeekNextToken();
            switch (t.TokenType)
            {
                case TokenType.Else:
                    GetNextToken();
                    ifNode.FalseNode = ParseParagraph();
                    if (GetNextToken().TokenType != TokenType.End)
                    {
                        ExceptionHelper.ThrowIfBlockNotEnd();
                        return null;
                    }
                    break;

                case TokenType.End:
                    if (GetNextToken().TokenType != TokenType.End)
                    {
                        ExceptionHelper.ThrowIfBlockNotEnd();
                        return null;
                    }
                    break;

                default:
                    Assertion.Fail("Unexpected token in if block.");
                    break;
            }
            return ifNode;
        }
    }
}
