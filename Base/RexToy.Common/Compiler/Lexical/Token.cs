using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics.CodeAnalysis;

namespace RexToy.Compiler.Lexical
{
    public class Token<TTokenType>
    {
        [SuppressMessage("Microsoft.Security", "CA2104")]
        public static readonly Token<TTokenType> EndToken = new Token<TTokenType>();

        private bool _end;
        public bool End
        {
            get { return _end; }
        }

        private TTokenType _tokenType;
        public TTokenType TokenType
        {
            get { return _tokenType; }
            protected set { _tokenType = value; }
        }

        private StringBuilder _value;
        protected StringBuilder InternalValue
        {
            get { return _value; }
        }    

        public string TokenValue
        {
            get
            {
                if (_value != null)
                    return _value.ToString();
                else
                    return null;
            }
        }

        private long _position;
        public long Position
        {
            get { return _position; }
        }

        private Token()
        {
            _end = true;
        }

        public Token(long position)
        {
            _value = new StringBuilder();
            _position = position;
            _end = false;
        }

        public void Add(char ch)
        {
            _value.Append(ch);
        }

        public void Add(string str)
        {
            _value.Append(str);
        }

        //Note:we can not made it abstract, otherwise the whole class is abstract, then we can't define EndToken above
        public virtual void EvalType(int status)
        {
            Assertion.Fail("Token.EvalType() must be override. Are you return correct Token instance in LexicalParser.CreateNewToken()?");
        }
    }
}
