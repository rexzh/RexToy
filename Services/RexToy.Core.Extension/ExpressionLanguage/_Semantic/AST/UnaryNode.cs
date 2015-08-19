using System;
using System.Collections.Generic;
using System.Text;

using RexToy.Compiler.Lexical;
using RexToy.Compiler.Semantic;
using RexToy.ExpressionLanguage.Tokens;

namespace RexToy.ExpressionLanguage.AST
{
    public class UnaryNode : Node_1
    {
        private Token<TokenType> opToken;
        public Token<TokenType> Token
        {
            get { return opToken; }
        }

        internal UnaryNode(Token<TokenType> token, Node node)
        {            
            token.ThrowIfNullArgument(nameof(token));
            node.ThrowIfNullArgument(nameof(node));

            opToken = token;
            Node = node;
        }

        public Node OperandNode
        {
            get { return Node; }
        }
    }
}
