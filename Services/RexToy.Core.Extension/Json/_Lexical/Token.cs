using System;
using System.Collections.Generic;
using System.Text;

using RexToy.Compiler.Lexical;
using JsonTokenType = RexToy.Json.TokenType;

namespace RexToy.Json
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
                        if (ch == JsonConstant.LeftBracket)
                            this.TokenType = JsonTokenType.LEFT_BRACKET;
                        if (ch == JsonConstant.RightBracket)
                            this.TokenType = JsonTokenType.RIGHT_BRACKET;
                        if (ch == JsonConstant.LeftSquareBracket)
                            this.TokenType = JsonTokenType.LEFT_SQUARE_BRACKET;
                        if (ch == JsonConstant.RightSquareBracket)
                            this.TokenType = JsonTokenType.RIGHT_SQUARE_BRACKET;
                        if (ch == JsonConstant.Colon)
                            this.TokenType = JsonTokenType.COLON;
                        if (ch == JsonConstant.Comma)
                            this.TokenType = JsonTokenType.COMMA;
                    }
                    break;

                case ParserStatus.ID:
                    if (str == JsonConstant.Null)
                        this.TokenType = JsonTokenType.NULL;
                    break;
            }
        }

        public override string ToString()
        {
            return string.Format("[{0}:{1}]", this.TokenType, this.InternalValue);
        }
    }
}
