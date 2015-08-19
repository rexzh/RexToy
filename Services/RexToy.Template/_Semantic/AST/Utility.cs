using System;
using System.Text;
using System.Text.RegularExpressions;

namespace RexToy.Template.AST
{
    static class Utility
    {
        private static char Tab = '\t';
        private static char Space = ' ';

        public static void RemoveKeywordAtBegin(this StringBuilder statement, string keyword)
        {
            if (!statement.StartsWith(keyword))
            {
                ExceptionHelper.ThrowKeywordExpected(keyword);
            }
            statement.RemoveBegin(keyword);
        }

        public static void RemoveSpaceAtBegin(this StringBuilder statement)
        {
            while (statement.Length > 0 && (statement[0] == Tab || statement[0] == Space))
                statement.RemoveBegin(1);
        }

        public static string ExtractFirstToken(this StringBuilder statement)
        {
            int tabIdx = statement.IndexOf(Tab);
            int spaceIdx = statement.IndexOf(Space);

            int idx;
            if (tabIdx * spaceIdx > 0)//Note:Detect if both is -1, or both is positive value.
                idx = (tabIdx < spaceIdx) ? tabIdx : spaceIdx;
            else//Note:one of it is -1, use positive one.
                idx = (tabIdx > 0) ? tabIdx : spaceIdx;

            if (idx < 0)
            {
                string token = statement.ToString();
                statement.Clear();
                return token;
            }
            else
            {
                string token = statement.ToString().Substring(0, idx);
                statement.RemoveBegin(token);
                return token;
            }
        }

        public static bool IsValidVariableName(this string var)
        {
            Regex regex = new Regex("[_a-zA-Z][_a-zA-Z0-9]*");
            return regex.IsMatch(var);
        }
    }
}
