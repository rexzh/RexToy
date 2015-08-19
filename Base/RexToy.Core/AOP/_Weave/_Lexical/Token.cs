using System;
using System.Collections.Generic;
using System.Text;

using RexToy.Compiler.Lexical;
using AOPTokenType = RexToy.AOP.TokenType;

namespace RexToy.AOP
{
    internal class Token : Token<TokenType>
    {
        public Token(long position)
            : base(position)
        {
        }

        public Token(long position, TokenType tokenType)
            : base(position)
        {
            this.TokenType = tokenType;
        }

        public override void EvalType(int status)
        {
            string str = this.InternalValue.ToString();
            switch (status)
            {
                case ParserStatus.Separator:
                    if (str.Length == 1)
                    {
                        char ch = str[0];
                        if (ch == '(')
                            this.TokenType = AOPTokenType.LEFT_P;
                        if (ch == ')')
                            this.TokenType = AOPTokenType.RIGHT_P;
                        if (ch == '*')
                            this.TokenType = AOPTokenType.ASTERISK;
                        if (ch == ',')
                            this.TokenType = AOPTokenType.COMMA;
                        if (ch == '.')
                            this.TokenType = AOPTokenType.DOT;
                        if (ch == '+')
                            this.TokenType = AOPTokenType.PLUS;
                    }
                    break;
            }
        }

        public override string ToString()
        {
            return string.Format("[{0}:{1}]", this.TokenType, this.InternalValue);
        }
    }
}
