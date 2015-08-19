using System;
using System.Collections.Generic;

using RexToy.Compiler.Lexical;

namespace RexToy.AOP
{
    public class LexicalParser : LexicalParser<TokenType, StatusMatrix>
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
            this.Current = this.CreateNewToken(this.Index);
            char ch = GetNextChar();
            while (ch != (char)0)
            {
                this.Status = this.Matrix.GetStatusTransform(this.Status, ch);
                if (this.Status == ParserStatus.Error)
                    ExceptionHelper.ThrowWeaveSyntax(this.Text);

                if (!this.Matrix.TokenTerminated)
                    this.Current.Add(ch);

                ch = GetNextChar();
            }

            AcceptLastToken(null, new LexicalParseEventArgs(ch, this.Status));//Note:Last token.

            PostProcess();

            return this.Tokens;
        }

        private void PostProcess()
        {
            //Note:Combine ..
            List<Token<TokenType>> list = new List<Token<TokenType>>();
            for (int i = 0; i < this.Tokens.Count; i++)
            {
                if (i < this.Tokens.Count - 1)
                {
                    if (this.Tokens[i].TokenType == TokenType.DOT && this.Tokens[i + 1].TokenType == TokenType.DOT)
                    {
                        var any = this.CreateNewToken(this.Tokens[i].Position, TokenType.ANY);
                        any.Add("..");
                        list.Add(any);
                        i++;
                    }
                    else
                    {
                        list.Add(this.Tokens[i]);
                    }
                }
                else
                    list.Add(this.Tokens[i]);
            }
            this.Tokens = list;
        }
    }
}
