using System;
using System.Collections.Generic;
using System.Text;

namespace RexToy.AOP
{
    internal static class ParserStatus
    {
        public const int Error = -1;
        public const int Start = 0;
        public const int ID = 1;
        public const int Separator = 2;
        public const int Space = 3;
    }
}
