using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using RexToy.Compiler.Lexical;

namespace RexToy.AOP
{
    public class StatusMatrix : StatusMatrixBase
    {
        [SuppressMessage("Microsoft.Performance", "CA1814")]
        private readonly int[,] m = new int[,]
        {
            {ParserStatus.Separator,ParserStatus.ID,ParserStatus.Space,ParserStatus.Error},
            {ParserStatus.Separator,ParserStatus.ID,ParserStatus.Space,ParserStatus.Error},
            {ParserStatus.Separator,ParserStatus.ID,ParserStatus.Space,ParserStatus.Error},
            {ParserStatus.Separator,ParserStatus.ID,ParserStatus.Space,ParserStatus.Error}            
        };

        public override int GetCharType(char ch)
        {
            if (ch == (char)0)
                return CharType.End;

            if (ch == '*' || ch == '.' || ch == ',' || ch == '(' || ch == ')' || ch == '+')
                return CharType.Separator;

            if (char.IsLetterOrDigit(ch) || ch == '_')
                return CharType.Letter;

            if (char.IsWhiteSpace(ch))
                return CharType.Space;

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
