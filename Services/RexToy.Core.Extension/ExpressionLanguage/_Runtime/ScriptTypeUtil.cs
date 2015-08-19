using System;
using System.Collections.Generic;
using System.Reflection;

namespace RexToy.ExpressionLanguage
{
    internal static class ScriptTypeUtil
    {
        public static bool IsDecimal(object val)
        {
            return val is double || val is float || val is decimal;
        }

        public static bool IsLong(object val)
        {
            return val is byte || val is sbyte || val is short || val is ushort || val is int || val is uint || val is long || val is ulong;
        }

        public static bool IsNumber(object val)
        {
            return IsDecimal(val) || IsLong(val);
        }

        public static decimal ConvertToDecimal(object val)
        {
            return Convert.ToDecimal(val);
        }

        public static long ConvertToLong(object val)
        {
            return Convert.ToInt64(val);
        }

        public static bool EvalToBoolean(object obj)
        {
            if (obj is bool)
                return (bool)obj;
            else
            {
                if (IsNumber(obj))
                {
                    decimal d = ConvertToDecimal(obj);
                    return d != 0;
                }
                else
                    return obj != null;
            }
        }
    }
}
