using System;
using System.Collections.Generic;
using System.Text;

namespace RexToy.Json
{
    internal static class ParserStatus
    {
        public const int Error = -1;
        public const int Start = 0;
        public const int ID = 1;
        public const int Separator = 2;
        public const int Space = 3;
        public const int Quot = 4;
        public const int Escaping = 9999;
    }
}
