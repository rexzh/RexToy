using System;
using System.Collections.Generic;
using System.Text;

using RexToy.Compiler.Lexical;

namespace RexToy.ExpressionLanguage.Tokens
{
    using ELTokenType = RexToy.ExpressionLanguage.Tokens.TokenType;

    public class Token : Token<ELTokenType>
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
            string val = this.InternalValue.ToString();
            switch (status)
            {
                case ParserStatus.Separator:
                    if (val == "(")
                        this.TokenType = ELTokenType.Left_Parenthesis;
                    if (val == ")")
                        this.TokenType = ELTokenType.Right_Parenthesis;
                    if (val == "[")
                        this.TokenType = ELTokenType.Left_Square_Bracket;
                    if (val == "]")
                        this.TokenType = ELTokenType.Right_Square_Bracket;
                    if (val == "?")
                        this.TokenType = ELTokenType.QuestionMark;
                    if (val == ":")
                        this.TokenType = ELTokenType.Colon;
                    if (val == ",")
                        this.TokenType = ELTokenType.Comma;
                    break;

                case ParserStatus.ID:
                case ParserStatus.Start:
                    if (val.BracketedBy(StringPair.DoubleQuote))
                    {
                        this.TokenType = ELTokenType.String;
                        this.InternalValue.UnBracketing(StringPair.DoubleQuote);
                    }
                    else if (val.BracketedBy(StringPair.SingleQuote))
                    {
                        this.TokenType = ELTokenType.String;
                        this.InternalValue.UnBracketing(StringPair.SingleQuote);
                    }
                    else
                    {
                        bool isNumber = true;
                        for (int index = 0; index < val.Length; index++)
                        {
                            if (!char.IsDigit(val[index]))
                            {
                                isNumber = false;
                                break;
                            }
                        }
                        if (isNumber)
                        {
                            this.TokenType = ELTokenType.Long;
                            break;
                        }

                        bool b;
                        if (bool.TryParse(val, out b))
                        {
                            this.TokenType = ELTokenType.Boolean;
                            break;
                        }

                        if (val == "array")
                        {
                            this.TokenType = ELTokenType.Array;
                            break;
                        }
                        if (val == "hash")
                        {
                            this.TokenType = ELTokenType.Hash;
                            break;
                        }
                        if (val == "regex")
                        {
                            this.TokenType = ELTokenType.Regex;
                            break;
                        }
                        if (val == "new")
                        {
                            this.TokenType = ELTokenType.New;
                            break;
                        }

                        this.TokenType = ELTokenType.ID;
                    }
                    break;

                case ParserStatus.Dot:
                    this.TokenType = ELTokenType.Dot;
                    break;

                case ParserStatus.Operator:
                    if (val == ">" || val == "<" || val == ">=" || val == "<=" || val == "==" || val == "<>" || val == "!=")
                        this.TokenType = ELTokenType.CompareOperator;
                    if (val == "+" || val == "-" || val == "*" || val == "/" || val == "%" || val == "^")
                        this.TokenType = ELTokenType.ArithmeticOperator;
                    if (val == "||" || val == "&&" || val == "!")
                        this.TokenType = ELTokenType.LogicalOperator;
                    break;
            }
        }

        public override string ToString()
        {
            return string.Format("[{0}:{1}]", this.TokenType, this.InternalValue);
        }
    }
}
