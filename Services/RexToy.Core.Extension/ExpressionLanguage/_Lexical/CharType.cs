using System;
using System.Collections.Generic;
using System.Text;

namespace RexToy.ExpressionLanguage
{
    internal static class CharType
    {
        public const int Separator = 0;//Note: {?:[](),}
        public const int Letter = 1;//Note: {a-zA-Z_}
        public const int Dot = 2;//Note: {.}
        public const int DQuot = 3;//Note{"}
        public const int SQuot = 4;//Note{'}
        public const int Space = 5;
        public const int Operator = 6;//Note: {+-*/=<>&|!%^}
        public const int Other = 7;
        public const int End = 8;
    }
}
