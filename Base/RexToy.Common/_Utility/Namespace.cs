using System;
using System.Collections.Generic;


namespace RexToy
{
    public static class Namespace
    {
        public static string Combine(string ns1, string ns2)
        {
            return ns1 + Type.Delimiter + ns2;
        }

        public static string Combine(string ns1, string ns2, string ns3)
        {
            return ns1 + Type.Delimiter + ns2 + Type.Delimiter + ns3;
        }

        public static string Combine(params string[] segment)
        {
            return segment.Join(Type.Delimiter);
        }
    }
}
