using System;
using System.Collections.Generic;
using System.Text;

using RexToy.Compiler.Lexical;

namespace RexToy.Template
{
    using RexToy.Template.Tokens;

    internal class LexicalParser : LexicalParser<TokenType, StatusMatrix>
    {
        public LexicalParser()
            : base()
        {
        }

        public override void SetParseContent(string text)
        {
            this.Text = text;
            this.Index = 0;
            this.Status = ParserStatus.Start;
            this.Tokens = new List<Token<TokenType>>(0);
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

                if (!this.Matrix.TokenTerminated)
                    this.Current.Add(ch);

                ch = GetNextChar();
            }

            AcceptLastToken(null, new LexicalParseEventArgs(ch, this.Status));//Note:Last token.

            PostParse();

            return this.Tokens;
        }

        private void PostParse()
        {
            JoinToken();
            SmartTrim();
        }

        private void JoinToken()
        {
            List<Token<TokenType>> tokens = new List<Token<TokenType>>();
            for (int index = 0; index < this.Tokens.Count; index++)
            {
                if (index < this.Tokens.Count - 1
                    && this.Tokens[index].TokenType == TokenType.Text
                    && this.Tokens[index + 1].TokenType == TokenType.Text)
                {
                    var t = this.CreateNewToken(this.Tokens[index].Position, TokenType.Text);
                    t.Add(this.Tokens[index].TokenValue);
                    t.Add(this.Tokens[index + 1].TokenValue);

                    tokens.Add(t);
                    index++;
                }
                else
                    tokens.Add(this.Tokens[index]);
            }

            this.Tokens = tokens;
        }

        private bool IsScript(Token<TokenType> t)
        {
            return t.TokenType != TokenType.None && t.TokenType != TokenType.Text;
        }

        private void SmartTrim()
        {
            char colon = ':';
            char[] spaceTab = new char[] { ' ', '\t' };
            char[] newLine = new char[] { '\r', '\n' };
            for (int i = 0; i < this.Tokens.Count; i++)
            {
                if (!IsScript(this.Tokens[i]))
                    continue;

                var t = this.Tokens[i];
                bool removePrev = t.TokenValue.StartsWith(colon);
                bool removeNext = t.TokenValue.EndsWith(colon);

                var clean = this.CreateNewToken(t.Position, t.TokenType);
                clean.Add(t.TokenValue.RemoveBegin(colon).RemoveEnd(colon));
                this.Tokens[i] = clean;

                if (removePrev && i > 0)
                {
                    var prev = this.Tokens[i - 1];
                    if (prev.TokenType == TokenType.Text)
                    {
                        var newPrev = this.CreateNewToken(prev.Position, TokenType.Text);
                        newPrev.Add(prev.TokenValue.TrimEnd(spaceTab).TrimEnd(newLine));
                        this.Tokens[i - 1] = newPrev;
                    }
                }

                if (removeNext && i < this.Tokens.Count - 1)
                {
                    var next = this.Tokens[i + 1];
                    if (next.TokenType == TokenType.Text)
                    {
                        var newNext = this.CreateNewToken(next.Position, TokenType.Text);
                        newNext.Add(next.TokenValue.TrimStart(spaceTab).TrimStart(newLine));
                        this.Tokens[i + 1] = newNext;
                    }
                }
            }
        }
    }
}
