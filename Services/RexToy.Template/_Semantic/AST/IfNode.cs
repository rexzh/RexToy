using System;
using System.Collections.Generic;
using System.Text;

using RexToy.Compiler.Lexical;
using RexToy.Compiler.Semantic;
using RexToy.Template.Tokens;

namespace RexToy.Template.AST
{
    public class IfNode : Node_2
    {
        public Node TrueNode
        {
            get { return Node1; }
            set { Node1 = value; }
        }

        public Node FalseNode
        {
            get { return Node2; }
            set { Node2 = value; }
        }

        private string _expr;
        public string Expr
        {
            get { return _expr; }
        }

        public IfNode(Token<TokenType> t)
        {
            //Note:Syntax: if <expr>            
            StringBuilder statement = new StringBuilder(t.TokenValue);
            statement.RemoveSpaceAtBegin();
            statement.RemoveKeywordAtBegin(TemplateKeywords.If);
            _expr = statement.ToString().Trim();
            if (_expr.Length == 0)
                ExceptionHelper.ThrowSyntaxError(t.TokenValue);
        }
    }
}
