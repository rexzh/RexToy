using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTest.ExpressionLanguage
{
    class ExpressionsForTest
    {
        public static string Expr1 = "DateTime.Parse(\"2000-1-1\")";
        public static string Expr2 = "3.2+ 1.5";
        public static string Expr3 = "_table[\"key\"]";
        public static string Expr4 = "\"Key\"+\"Value\"";
        public static string Expr5 = "1.2+\"$\"";
        public static string Expr6 = "Update(1, true)";
        public static string Expr7 = "(a>b) ? a : b";
        public static string Expr8 = "(a!=0)?tb[a]:b";
        public static string Expr9 = "(a+b>0)?\"T\":\"F\"";
        public static string Expr10 = "GetData(\"rex\").Name.ToString()";
        public static string Expr11 = "-(a+7)/7";
        public static string Expr12 = "-a+7/(-5)";
        public static string Expr13 = "person[index].Name.Last";
        public static string Expr14 = "regex['^\\d3*$']";
        public static string Expr15 = "hash[x:1,y:5]";
        public static string Expr16 = "array[4:7]";
        public static string Expr17 = "array[1,1,2,3,5]";
    }
}
