using System;
using System.Collections.Generic;
using System.Text;

namespace RexToy
{
    public static class StringBuilderExtension
    {
        public static int IndexOf(this StringBuilder str, string sub)
        {
            str.ThrowIfNullArgument(nameof(str));
            sub.ThrowIfNullArgument(nameof(sub));

            return str.ToString().IndexOf(sub);
        }

        public static int IndexOf(this StringBuilder str, char ch)
        {
            str.ThrowIfNullArgument(nameof(str));

            return str.ToString().IndexOf(ch);
        }

        public static int LastIndexOf(this StringBuilder str, string sub)
        {
            str.ThrowIfNullArgument(nameof(str));
            sub.ThrowIfNullArgument(nameof(sub));

            return str.ToString().LastIndexOf(sub);
        }

        public static int LastIndexOf(this StringBuilder str, char ch)
        {
            str.ThrowIfNullArgument(nameof(str));

            return str.ToString().LastIndexOf(ch);
        }

        public static bool StartsWith(this StringBuilder str, string begin)
        {
            str.ThrowIfNullArgument(nameof(str));
            begin.ThrowIfNullArgument(nameof(begin));

            return str.ToString().StartsWith(begin);
        }

        public static bool StartsWith(this StringBuilder str, char ch)
        {
            str.ThrowIfNullArgument(nameof(str));

            if (str.Length == 0)
                return false;
            return str[0] == ch;
        }

        public static bool EndsWith(this StringBuilder str, string end)
        {
            str.ThrowIfNullArgument(nameof(str));
            end.ThrowIfNullArgument(nameof(end));

            return str.ToString().EndsWith(end);
        }

        public static bool EndsWith(this StringBuilder str, char ch)
        {
            str.ThrowIfNullArgument(nameof(str));

            if (str.Length == 0)
                return false;
            return str[str.Length - 1] == ch;
        }

        public static StringBuilder RemoveBegin(this StringBuilder str, int length)
        {
            str.ThrowIfNullArgument(nameof(str));

            return str.Remove(0, length);
        }

        public static StringBuilder RemoveBegin(this StringBuilder str, char ch)
        {
            str.ThrowIfNullArgument(nameof(str));

            if (str.Length == 0)
                return str;
            if (str[0] == ch)
                str.Remove(0, 1);
            return str;
        }

        public static StringBuilder RemoveBegin(this StringBuilder str, string begin)
        {
            str.ThrowIfNullArgument(nameof(str));
            begin.ThrowIfNullArgument(nameof(begin));

            if (str.StartsWith(begin))
                str.Remove(0, begin.Length);
            return str;
        }

        public static StringBuilder RemoveEnd(this StringBuilder str, int length)
        {
            str.ThrowIfNullArgument(nameof(str));

            return str.Remove(str.Length - length, length);
        }

        public static StringBuilder RemoveEnd(this StringBuilder str, char ch)
        {
            str.ThrowIfNullArgument(nameof(str));

            if (str.Length == 0)
                return str;
            if (str[str.Length - 1] == ch)
                str.Remove(str.Length - 1, 1);
            return str;
        }

        public static StringBuilder RemoveEnd(this StringBuilder str, string end)
        {
            str.ThrowIfNullArgument(nameof(str));
            end.ThrowIfNullArgument(nameof(end));

            if (str.EndsWith(end))
                str.Remove(str.Length - end.Length, end.Length);
            return str;
        }

        public static StringBuilder Bracketing(this StringBuilder str, StringPair pair)
        {
            str.ThrowIfNullArgument(nameof(str));

            return str.Insert(0, pair.Begin).Append(pair.End);
        }

        public static StringBuilder UnBracketing(this StringBuilder str, StringPair pair)
        {
            str.ThrowIfNullArgument(nameof(str));

            return str.RemoveBegin(pair.Begin).RemoveEnd(pair.End);
        }

        public static bool BracketedBy(this StringBuilder str, StringPair pair)
        {
            str.ThrowIfNullArgument(nameof(str));

            return str.StartsWith(pair.Begin) && str.EndsWith(pair.End);
        }
    }
}
