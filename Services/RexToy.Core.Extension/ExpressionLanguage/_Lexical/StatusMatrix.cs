using System;
using System.Collections.Generic;
using System.Text;

using RexToy.Compiler.Lexical;

namespace RexToy.ExpressionLanguage
{
    internal class StatusMatrix : StatusMatrixBase
    {
        private readonly int[,] m = new int[,]
        {
            {ParserStatus.Separator,ParserStatus.ID,ParserStatus.Error,ParserStatus.DQuot,ParserStatus.SQuot,ParserStatus.Space,ParserStatus.Operator,ParserStatus.Error},
            {ParserStatus.Separator,ParserStatus.ID,ParserStatus.Dot,ParserStatus.Error,ParserStatus.Error,ParserStatus.Space,ParserStatus.Operator,ParserStatus.Error},
            {ParserStatus.Error,ParserStatus.ID,ParserStatus.Error,ParserStatus.Error,ParserStatus.Error,ParserStatus.Error,ParserStatus.Error,ParserStatus.Error},
            {ParserStatus.Separator,ParserStatus.ID,ParserStatus.Dot,ParserStatus.DQuot,ParserStatus.SQuot,ParserStatus.Space,ParserStatus.Operator,ParserStatus.Error},
            {ParserStatus.Separator,ParserStatus.ID,ParserStatus.Error,ParserStatus.DQuot,ParserStatus.SQuot,ParserStatus.Space,ParserStatus.Operator,ParserStatus.Error},
            {ParserStatus.Separator,ParserStatus.ID,ParserStatus.Error,ParserStatus.DQuot,ParserStatus.SQuot,ParserStatus.Space,ParserStatus.Operator,ParserStatus.Error},
            {ParserStatus.DQuot,ParserStatus.DQuot,ParserStatus.DQuot,ParserStatus.DPeekNext,ParserStatus.DQuot,ParserStatus.DQuot,ParserStatus.DQuot,ParserStatus.DQuot},
            {ParserStatus.SQuot,ParserStatus.SQuot,ParserStatus.SQuot,ParserStatus.SQuot,ParserStatus.SPeekNext,ParserStatus.SQuot,ParserStatus.SQuot,ParserStatus.SQuot},
        };

        public override int GetCharType(char ch)
        {
            if (ch == (char)0)
                return CharType.End;

            if (ch == '.')
                return CharType.Dot;

            if (ch == '?' || ch == ':' || ch == '(' || ch == ')' || ch == '[' || ch == ']' || ch == ',')
                return CharType.Separator;

            if (char.IsLetterOrDigit(ch) || ch == '_')
                return CharType.Letter;

            if (char.IsWhiteSpace(ch))
                return CharType.Space;

            if (ch == '"')
                return CharType.DQuot;

            if (ch == '\'')
                return CharType.SQuot;

            if (ch == '+' || ch == '-' || ch == '*' || ch == '/' || ch == '=' || ch == '<' || ch == '>' || ch == '&' || ch == '|' || ch == '!' || ch == '%' || ch == '^')
                return CharType.Operator;

            return CharType.Other;
        }

        public override int GetStatusTransform(int origin, char ch)
        {
            int chType = this.GetCharType(ch);

            int status = m[origin, chType];

            if (origin == ParserStatus.Start || origin == ParserStatus.Separator)
            {
                Fire_TokenTerminate(ch, origin);
                return status;
            }

            if (origin == ParserStatus.ID && chType != CharType.Letter)
            {
                Fire_TokenTerminate(ch, origin);
                return status;
            }

            if (origin == ParserStatus.Operator && chType != CharType.Operator)
            {
                Fire_TokenTerminate(ch, origin);
                return status;
            }

            if (origin == ParserStatus.Dot)
            {
                Fire_TokenTerminate(ch, origin);
                return status;
            }

            if (origin == ParserStatus.Space && chType != CharType.Space)
            {
                Fire_TokenTerminate(ch, origin);
                return status;
            }

            this.TokenTerminated = false;
            return status;
        }
    }
}
