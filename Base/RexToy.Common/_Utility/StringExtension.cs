using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics.CodeAnalysis;

namespace RexToy
{
    public static class StringExtension
    {
        [SuppressMessage("Microsoft.Design", "CA1031")]
        public static string TryFormat(this string s, params object[] args)
        {
            string result;
            try
            {
                result = string.Format(s, args);
            }
            catch
            {
                result = s + '|';
                for (int i = 0; i < args.Length; i++)
                    result += args[i];
            }
            return result;
        }

        public static bool StartsWith(this string str, char ch)
        {
            str.ThrowIfNullArgument(nameof(str));

            if (str.Length == 0)
                return false;
            return str[0] == ch;
        }

        public static bool EndsWith(this string str, char ch)
        {
            str.ThrowIfNullArgument(nameof(str));

            if (str.Length == 0)
                return false;
            return str[str.Length - 1] == ch;
        }

        public static string RemoveBegin(this string str, int length)
        {
            str.ThrowIfNullArgument(nameof(str));

            return str.Substring(length);
        }

        public static string RemoveBegin(this string str, string begin)
        {
            str.ThrowIfNullArgument(nameof(str));
            begin.ThrowIfNullArgument(nameof(begin));

            if (str.StartsWith(begin))
                return str.Substring(begin.Length);
            else
                return str;
        }

        public static string RemoveBegin(this string str, char c)
        {
            str.ThrowIfNullArgument(nameof(str));

            if (str.Length == 0)
                return str;
            if (str[0] == c)
                return str.Substring(1);
            else
                return str;
        }

        public static string RemoveEnd(this string str, int length)
        {
            str.ThrowIfNullArgument(nameof(str));            

            return str.Remove(str.Length - length);
        }

        public static string RemoveEnd(this string str, string end)
        {
            str.ThrowIfNullArgument(nameof(str));
            end.ThrowIfNullArgument(nameof(end));

            if (str.EndsWith(end))
                return str.Remove(str.Length - end.Length);
            else
                return str;
        }

        public static string RemoveEnd(this string str, char c)
        {
            str.ThrowIfNullArgument(nameof(str));

            if (str.Length == 0)
                return str;
            if (str[str.Length - 1] == c)
                return str.Remove(str.Length - 1);
            else
                return str;
        }

        public static string Bracketing(this string str, StringPair pair)
        {
            str.ThrowIfNullArgument(nameof(str));

            return pair.Begin + str + pair.End;
        }

        public static string UnBracketing(this string str, StringPair pair)
        {
            str.ThrowIfNullArgument(nameof(str));

            return str.RemoveBegin(pair.Begin).RemoveEnd(pair.End);
        }

        public static bool BracketedBy(this string str, StringPair pair)
        {
            str.ThrowIfNullArgument(nameof(str));

            return str.StartsWith(pair.Begin) && str.EndsWith(pair.End);
        }

        public static string[] Split(this string str, char delimiter, StringSplitOptions options)
        {
            str.ThrowIfNullArgument(nameof(str));

            char[] chArr = new char[] { delimiter };
            return str.Split(chArr, options);
        }

        public static bool IsPrefixWith(this string str, string prefix, char delimiter)
        {
            return str.StartsWith(prefix) && str[prefix.Length] == delimiter;
        }
    }
}
