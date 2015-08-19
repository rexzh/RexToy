using System;

namespace RexToy.Template
{
    internal static class ParserStatus
    {
        public const int Error = -1;
        public const int Start = 0;
        public const int Pound = 1;
        public const int TextBlock = 2;
        public const int ScriptBlock = 3;
        public const int ScriptQuot = 4;
    }
}
