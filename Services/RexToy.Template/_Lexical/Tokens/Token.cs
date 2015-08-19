using System;
using System.Collections.Generic;

using RexToy.Compiler.Lexical;

namespace RexToy.Template.Tokens
{
    using TTokenType = RexToy.Template.Tokens.TokenType;

    public class Token : Token<TokenType>
    {
        public Token(long index)
            : base(index)
        {
        }

        public Token(long index, TokenType tokenType)
            : base(index)
        {
            this.TokenType = tokenType;
        }

        public override void EvalType(int status)
        {
            switch (status)
            {
                case ParserStatus.TextBlock:
                    this.TokenType = TTokenType.Text;
                    break;

                case ParserStatus.Start:
                    if (this.InternalValue.StartsWith(StringPair.Template_Bracket.Begin))
                    {
                        this.TokenType = TTokenType.Script;
                        this.InternalValue.UnBracketing(StringPair.Template_Bracket);

                        string str = this.InternalValue.ToString().RemoveBegin(':').RemoveEnd(':');

                        if (str.StartsWith(TemplateKeywords.Let))
                        {
                            this.TokenType = TTokenType.Let;//Note: note the space after [let]
                            break;
                        }

                        if (str.StartsWith(TemplateKeywords.If))//Note: note the space after [if]
                        {
                            this.TokenType = TTokenType.If;
                            break;
                        }

                        if (str == TemplateKeywords.Else)
                        {
                            this.TokenType = TTokenType.Else;
                            break;
                        }

                        if (str.StartsWith(TemplateKeywords.For))//Note: note the space after [for]
                        {
                            this.TokenType = TTokenType.For;
                            break;
                        }

                        if (str == TemplateKeywords.End)
                        {
                            this.TokenType = TTokenType.End;
                            break;
                        }

                        if (str.StartsWith(TemplateKeywords.Remark))
                        {
                            this.TokenType = TTokenType.Remark;
                            break;
                        }

                        if (str.StartsWith(TemplateKeywords.Include))
                        {
                            this.TokenType = TTokenType.Include;
                            break;
                        }

                        if (str == TemplateKeywords.Break)
                        {
                            this.TokenType = TTokenType.Break;
                            break;
                        }

                        if (str == TemplateKeywords.Continue)
                        {
                            this.TokenType = TTokenType.Continue;
                            break;
                        }

                        this.TokenType = TTokenType.Expression;
                    }
                    else//Note:It's escape, remove the first '#'
                    {
                        this.TokenType = TTokenType.Text;
                        this.InternalValue.RemoveBegin('#');
                    }
                    break;

                default:
                    ExceptionHelper.ThrowTokenEvalTypeError(this.Position);
                    break;
            }
        }
    }
}
