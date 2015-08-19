using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RexToy.ORM
{
    public static class QueryFunctionExtension
    {
        private const string SHOULD_NOT_CALL = "Function should not be invoke directly, only use for translate to SQL.";
        public static bool In(this int item, IEnumerable<int> collection)
        {
            throw new NotImplementedException(SHOULD_NOT_CALL);
        }

        public static bool In(this int item, params int[] collection)
        {
            throw new NotImplementedException(SHOULD_NOT_CALL);
        }

        public static bool In(this string item, IEnumerable<string> collection)
        {
            throw new NotImplementedException(SHOULD_NOT_CALL);
        }

        public static bool In(this string item, params string[] collection)
        {
            throw new NotImplementedException(SHOULD_NOT_CALL);
        }

        public static bool In(this long item, IEnumerable<long> collection)
        {
            throw new NotImplementedException(SHOULD_NOT_CALL);
        }

        public static bool In(this long item, params long[] collection)
        {
            throw new NotImplementedException(SHOULD_NOT_CALL);
        }

        public static bool Like(this string str, string pattern)
        {
            throw new NotImplementedException(SHOULD_NOT_CALL);
        }

        public static bool True<T>(this T instance)
        {
            throw new NotImplementedException(SHOULD_NOT_CALL);
        }

        public static bool False<T>(this T instance)
        {
            throw new NotImplementedException(SHOULD_NOT_CALL);
        }

        //Extend:Between.. etc
    }
}
