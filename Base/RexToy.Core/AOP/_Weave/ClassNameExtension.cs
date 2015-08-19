using System;
using System.Collections.Generic;

namespace RexToy.AOP
{
    static class ClassNameExtension
    {
        public static string StripGenericAndRef(this string name)
        {
            name = name.RemoveEnd('&');
            int idx = name.IndexOf('`');
            if (idx < 0)
                return name;
            else
                return name.Substring(0, idx);
        }
    }
}
