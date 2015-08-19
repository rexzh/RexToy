using System;
using System.Collections.Generic;

using RexToy.Compiler.Lexical;
using RexToy.Compiler.Semantic;
using RexToy.ExpressionLanguage.Tokens;

namespace RexToy.ExpressionLanguage.AST
{
    public class RegexNode : Node
    {
        internal RegexNode(Token<TokenType> token)
        {
            token.ThrowIfNullArgument(nameof(token));
            _pattern = token.TokenValue;
        }

        private string _pattern;
        public string Pattern
        {
            get { return _pattern; }
        }
    }
}
