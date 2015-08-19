using System;
using System.Collections.Generic;

using RexToy.Compiler.Lexical;

namespace RexToy.Json
{
    internal class StatusMatrix : StatusMatrixBase
    {
        private readonly int[,] m = new int[,]
        {
            {ParserStatus.Separator,ParserStatus.ID,ParserStatus.Space,ParserStatus.Quot,ParserStatus.Error, ParserStatus.Error},
            {ParserStatus.Separator,ParserStatus.ID,ParserStatus.Space,ParserStatus.Error,ParserStatus.Error, ParserStatus.Error},
            {ParserStatus.Separator,ParserStatus.ID,ParserStatus.Space,ParserStatus.Quot,ParserStatus.Error, ParserStatus.Error},
            {ParserStatus.Separator,ParserStatus.ID,ParserStatus.Space,ParserStatus.Quot,ParserStatus.Error, ParserStatus.Error},
            {ParserStatus.Quot,ParserStatus.Quot,ParserStatus.Quot,ParserStatus.Start,ParserStatus.Quot, ParserStatus.Escaping}
        };

        public override int GetCharType(char ch)
        {
            if (ch == (char)0)
                return CharType.End;

            if (ch == JsonConstant.LeftBracket || ch == JsonConstant.RightBracket ||
                ch == JsonConstant.LeftSquareBracket || ch == JsonConstant.RightSquareBracket ||
                ch == JsonConstant.Comma || ch == JsonConstant.Colon)
                return CharType.Separator;

            if (char.IsLetterOrDigit(ch) || ch == '_' || ch == '.' || ch == '-')
                return CharType.Letter;

            if (char.IsWhiteSpace(ch))
                return CharType.Space;

            if (ch == JsonConstant.Quot)
                return CharType.Quot;

            if (ch == JsonConstant.BackSlash)
                return CharType.Backslash;

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

            if (origin == ParserStatus.ID && chType != CharType.Letter && chType != CharType.Backslash)
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
