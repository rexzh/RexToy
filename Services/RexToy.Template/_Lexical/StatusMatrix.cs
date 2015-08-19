using System;
using System.Collections.Generic;

using RexToy.Compiler.Lexical;

namespace RexToy.Template
{
    internal class StatusMatrix : StatusMatrixBase
    {
        private readonly int[,] m = new int[,]
        {
            {ParserStatus.Pound,ParserStatus.TextBlock,ParserStatus.TextBlock,ParserStatus.TextBlock, ParserStatus.TextBlock},
            {ParserStatus.Pound,ParserStatus.ScriptBlock,ParserStatus.TextBlock,ParserStatus.TextBlock, ParserStatus.TextBlock},
            {ParserStatus.Pound,ParserStatus.TextBlock,ParserStatus.TextBlock,ParserStatus.TextBlock, ParserStatus.TextBlock},
            {ParserStatus.ScriptBlock,ParserStatus.Error,ParserStatus.Start,ParserStatus.ScriptBlock, ParserStatus.ScriptQuot},
            {ParserStatus.ScriptQuot,ParserStatus.ScriptQuot,ParserStatus.ScriptQuot,ParserStatus.ScriptQuot, ParserStatus.ScriptBlock}
        };

        public override int GetCharType(char ch)
        {
            if (ch == (char)0)
                return CharType.End;

            if (ch == '{')
                return CharType.LeftBracket;

            if (ch == '}')
                return CharType.RightBracket;

            if (ch == '#')
                return CharType.Pound;

            if (ch == '"')
                return CharType.Quot;

            return CharType.Other;
        }

        public override int GetStatusTransform(int origin, char ch)
        {
            int chType = this.GetCharType(ch);

            int status = m[origin, chType];

            if (origin == ParserStatus.Start)
            {
                Fire_TokenTerminate(ch, origin);
                return status;
            }

            if (origin == ParserStatus.TextBlock && status == ParserStatus.Pound)
            {
                Fire_TokenTerminate(ch, origin);
                return status;
            }

            this.TokenTerminated = false;
            return status;
        }
    }
}

