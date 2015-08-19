using System;
using System.Collections.Generic;
using System.Text;

namespace RexToy.ExpressionLanguage
{
    internal static class ParserStatus
    {
        public const int Error = -1;
        public const int Start = 0;
        public const int ID = 1;
        public const int Dot = 2;
        public const int Separator = 3;
        public const int Space = 4;
        public const int Operator = 5;
        public const int DQuot = 6;
        public const int SQuot = 7;
        //Note:PeekNext is used for escape, for example:
        //"Hello""World" will parse as string Hello"World
        public const int SPeekNext = 9998;
        public const int DPeekNext = 9999;
    }
}
