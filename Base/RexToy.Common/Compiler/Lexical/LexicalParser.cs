using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace RexToy.Compiler.Lexical
{
    public abstract class LexicalParser<TTokenType, TStatusMatrix>
        where TStatusMatrix : StatusMatrixBase, new()
    {
        private int _index;
        protected int Index
        {
            get { return _index; }
            set { _index = value; }
        }

        private string _text;
        protected string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        private int _status;
        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        private IList<Token<TTokenType>> _tokens;
        protected IList<Token<TTokenType>> Tokens
        {
            get { return _tokens; }
            set { _tokens = value; }
        }

        private TStatusMatrix _matrix;
        protected TStatusMatrix Matrix
        {
            get { return _matrix; }
            set { _matrix = value; }
        }

        private bool _keepSpaceChar;

        protected LexicalParser(bool keepSpaceChar = false)
        {
            _matrix = new TStatusMatrix();
            _matrix.TokenTerminate += this.AcceptLastToken;
            _keepSpaceChar = keepSpaceChar;
        }

        public abstract void SetParseContent(string text);

        [SuppressMessage("Microsoft.Design", "CA1024")]
        protected char GetNextChar()
        {
            if (_index == _text.Length)
                return (char)0;
            else
            {
                return _text[_index++];
            }
        }

        protected char PeekNextChar()
        {
            if (_index == _text.Length)
                return (char)0;
            else
                return _text[_index];
        }

        private Token<TTokenType> _curr;
        protected Token<TTokenType> Current
        {
            get { return _curr; }
            set { _curr = value; }
        }

        public abstract IList<Token<TTokenType>> Parse();
        protected abstract Token<TTokenType> CreateNewToken(long index);
        protected abstract Token<TTokenType> CreateNewToken(long index, TTokenType tokenType);

        protected virtual void AcceptLastToken(object sender, LexicalParseEventArgs e)
        {
            string val = _keepSpaceChar ? this.Current.TokenValue : this.Current.TokenValue.Trim();
            if (val.Length != 0)//Note:throw zero length token away.
            {
                this.Current.EvalType(_status);
                _tokens.Add(this.Current);
            }

            this.Current = this.CreateNewToken(_index - 1);//Note:Next token begin.
            if (e.Ch != (char)0)
                this.Current.Add(e.Ch);
        }
    }
}
