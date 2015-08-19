using System;
using System.Collections.Generic;

using RexToy.Compiler.Lexical;
using RexToy.Compiler.Semantic;
using RexToy.Template.Tokens;

namespace RexToy.Template.AST
{
    public class SimpleNode : Node
    {
        private Token<TokenType> _token;
        public SimpleNode(Token<TokenType> token)
        {
            _token = token;
        }

        public Token<TokenType> Token
        {
            get { return _token; }
        }
    }
}
