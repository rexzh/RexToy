using System;
using System.Collections.Generic;
using System.Text;

using RexToy.Compiler.Lexical;
using RexToy.Compiler.Semantic;
using RexToy.ExpressionLanguage.Tokens;

namespace RexToy.ExpressionLanguage.AST
{
    public class SimpleNode : Node
    {
        private Token<TokenType> _token;
        internal SimpleNode(Token<TokenType> token)
        {
            token.ThrowIfNullArgument(nameof(token));
            _token = token;
        }

        public Token<TokenType> Token
        {
            get { return _token; }
        }
    }
}
