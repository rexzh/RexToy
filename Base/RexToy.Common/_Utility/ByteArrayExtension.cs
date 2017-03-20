using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RexToy
{
    public static class ByteArrayExtension
    {
        public static string ToHexString(this byte[] raw)
        {
            StringBuilder hex = new StringBuilder(raw.Length * 2);
            foreach (byte b in raw)
                hex.AppendFormat("{0:x2} ", b);
            return hex.ToString();
        }
    }
}
