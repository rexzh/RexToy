using System;
using System.Collections.Generic;
using System.Diagnostics;

using RexToy.Compiler.Lexical;

namespace RexToy.ExpressionLanguage
{
    using RexToy.ExpressionLanguage.Tokens;

    internal class LexicalParser : LexicalParser<TokenType, StatusMatrix>
    {
        public override void SetParseContent(string text)
        {
            this.Text = text.Trim();
            this.Index = 0;
            this.Status = ParserStatus.Start;
            this.Tokens = new List<Token<TokenType>>();
        }

        protected override Token<TokenType> CreateNewToken(long index)
        {
            return new Token(index);
        }

        protected override Token<TokenType> CreateNewToken(long index, TokenType tokenType)
        {
            return new Token(index, tokenType);
        }

        public override IList<Token<TokenType>> Parse()
        {
            this.Current = new Token(this.Index);
            char ch = GetNextChar();
            while (ch != (char)0)
            {
                this.Status = this.Matrix.GetStatusTransform(this.Status, ch);
                if (this.Status == ParserStatus.Error)
                    ExceptionHelper.ThrowParseError(this.Index - 1);

                if (this.Status == ParserStatus.DPeekNext)
                {
                    char next = PeekNextChar();
                    if (this.Matrix.GetCharType(next) == CharType.DQuot)
                    {
                        GetNextChar();//Note:skip the next double quot.
                        this.Current.Add(ch);
                        this.Status = ParserStatus.DQuot;
                    }
                    else
                    {
                        this.Current.Add(ch);
                        this.Status = ParserStatus.Start;//Note:accept the close quot and set to Start,so when scan next char,this token will be accepted.
                    }
                }
                else if (this.Status == ParserStatus.SPeekNext)
                {
                    char next = PeekNextChar();
                    if (this.Matrix.GetCharType(next) == CharType.SQuot)
                    {
                        GetNextChar();//Note:skip the next single quot.
                        this.Current.Add(ch);
                        this.Status = ParserStatus.SQuot;
                    }
                    else
                    {
                        this.Current.Add(ch);
                        this.Status = ParserStatus.Start;//Note:accept the close quot and set to Start,so when scan next char,this token will be accepted.
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

            PostProcess();

            return this.Tokens;
        }

        //Note: Combine Number.Number into one token
        private void PostProcess()
        {
            List<Token<TokenType>> tokens = new List<Token<TokenType>>();
            for (int index = 0; index < this.Tokens.Count; index++)
            {
                if (index < this.Tokens.Count - 2
                    && this.Tokens[index].TokenType == TokenType.Long
                    && this.Tokens[index + 1].TokenType == TokenType.Dot
                    && this.Tokens[index + 2].TokenType == TokenType.Long)
                {
                    var t = this.CreateNewToken(this.Tokens[index].Position, TokenType.Decimal);
                    t.Add(this.Tokens[index].TokenValue + this.Tokens[index + 1].TokenValue + this.Tokens[index + 2].TokenValue);

                    tokens.Add(t);
                    index += 2;
                }
                else if (index < this.Tokens.Count - 1
                    && this.Tokens[index].TokenType == TokenType.Colon && this.Tokens[index + 1].TokenType == TokenType.Colon)
                {
                    var t = this.CreateNewToken(this.Tokens[index].Position, TokenType.ClassQualifier);
                    t.Add(this.Tokens[index].TokenValue + this.Tokens[index + 1].TokenValue);
                    tokens.Add(t);
                    index += 1;
                }
                else
                    tokens.Add(this.Tokens[index]);
            }

            this.Tokens = tokens;
        }
    }
}
