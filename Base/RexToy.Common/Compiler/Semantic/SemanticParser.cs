using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using RexToy.Compiler.Lexical;

namespace RexToy.Compiler.Semantic
{
    public abstract class SemanticParser<TTokenType, TAST>
    {
        protected SemanticParser()
        {
        }

        private int _index;
        protected int Index
        {
            get { return _index; }
        }

        private IList<Token<TTokenType>> _tokens;
        protected IList<Token<TTokenType>> Tokens
        {
            get { return _tokens; }
        }

        public virtual void SetParseContent(IList<Token<TTokenType>> tokens)
        {
            this._tokens = tokens;
            _index = 0;
        }

        protected Token<TTokenType> PeekNextToken()
        {
            if (_index == _tokens.Count)
                return Token<TTokenType>.EndToken;
            return _tokens[_index];
        }

        [SuppressMessage("Microsoft.Design", "CA1024")]
        protected Token<TTokenType> GetNextToken()
        {
            if (_index == _tokens.Count)
                return Token<TTokenType>.EndToken;
            return _tokens[_index++];
        }

        public abstract TAST Parse();
    }
}