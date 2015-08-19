using System;
using System.Collections.Generic;
using System.Text;

using RexToy.Compiler.Lexical;
using RexToy.Compiler.Semantic;
using RexToy.ExpressionLanguage.Tokens;

namespace RexToy.ExpressionLanguage.AST
{
    public class OperatorNode : Node_2
    {
        private Token<TokenType> _opToken;
        public Token<TokenType> Token
        {
            get { return _opToken; }
        }

        internal OperatorNode(Token<TokenType> opToken, Node lhs, Node rhs)
        {
            opToken.ThrowIfNullArgument(nameof(opToken));
            lhs.ThrowIfNullArgument(nameof(lhs));
            rhs.ThrowIfNullArgument(nameof(rhs));

            _opToken = opToken;
            Node1 = lhs;
            Node2 = rhs;
        }

        public Node Lhs
        {
            get { return Node1; }
        }

        public Node Rhs
        {
            get { return Node2; }
        }
    }
}
