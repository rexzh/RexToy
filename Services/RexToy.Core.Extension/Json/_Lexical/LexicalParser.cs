using System;
using System.Collections.Generic;
using System.Text;

using RexToy.Compiler.Lexical;

namespace RexToy.Json
{
    internal class LexicalParser : LexicalParser<TokenType, StatusMatrix>
    {
        public override void SetParseContent(string text)
        {
            this.Text = text.Trim();
            this.Index = 0;
            this.Status = ParserStatus.Start;
            this.Tokens = new List<Token<TokenType>>();
        }

        protected override Token<TokenType> CreateNewToken(long index, TokenType tokenType)
        {
            return new Token(index, tokenType);
        }

        protected override Token<TokenType> CreateNewToken(long index)
        {
            return new Token(index);
        }

        public override IList<Token<TokenType>> Parse()
        {
            this.Current = new Token(this.Index);
            char ch = GetNextChar();
            while (ch != (char)0)
            {
                this.Status = this.Matrix.GetStatusTransform(this.Status, ch);
                if (this.Status == ParserStatus.Error)
                    ExceptionHelper.ThrowParseException(this.Index - 1);

                if (this.Status == ParserStatus.Escaping)
                {
                    char next = PeekNextChar();
                    if(this.Matrix.GetCharType(next) == CharType.Quot)//Escaping
                    {
                        GetNextChar();
                        this.Current.Add(JsonConstant.Quot);
                        this.Status = ParserStatus.Quot;
                    }
                    else
                    {
                        this.Current.Add(ch);
                        this.Status = ParserStatus.Quot;
                    }
                }
                else
                {
                    if (!this.Matrix.TokenTerminated)
                        this.Current.Add(ch);
                }

                ch = GetNextChar();
            }

            AcceptLastToken(null, new LexicalParseEventArgs(ch, this.Status));//Note:Last token.

            return this.Tokens;
        }
    }
}
