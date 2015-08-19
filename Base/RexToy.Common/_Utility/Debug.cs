using System;
using System.Diagnostics;

using DBG = System.Diagnostics.Debug;

namespace RexToy
{
    public static class Debug
    {
        public static void Write(object value)
        {
            DBG.Write(value);
        }

        public static void Write(string message)
        {
            DBG.Write(message);
        }

        public static void Write(object value, string category)
        {
            DBG.Write(value, category);
        }

        public static void Write(string message, string category)
        {
            DBG.Write(message, category);
        }

        public static void WriteIf(bool condition, object value)
        {
            DBG.WriteIf(condition, value);
        }

        public static void WriteIf(bool condition, string message)
        {
            DBG.WriteIf(condition, message);
        }

        public static void WriteIf(bool condition, object value, string category)
        {
            DBG.WriteIf(condition, value, category);
        }

        public static void WriteIf(bool condition, string message, string category)
        {
            DBG.WriteIf(condition, message, category);
        }

        public static void WriteLine(object value)
        {
            DBG.WriteLine(value);
        }

        public static void WriteLine(string message)
        {
            DBG.WriteLine(message);
        }

        public static void WriteLine(object value, string category)
        {
            DBG.WriteLine(value, category);
        }

        public static void WriteLine(string message, string category)
        {
            DBG.WriteLine(message, category);
        }

        public static void WriteLine(string format, params object[] args)
        {
            DBG.WriteLine(format, args);
        }

        public static void WriteLineIf(bool condition, object value)
        {
            DBG.WriteLineIf(condition, value);
        }

        public static void WriteLineIf(bool condition, string message)
        {
            DBG.WriteLineIf(condition, message);
        }

        public static void WriteLineIf(bool condition, object value, string category)
        {
            DBG.WriteLineIf(condition, value, category);
        }

        public static void WriteLineIf(bool condition, string message, string category)
        {
            DBG.WriteLineIf(condition, message, category);
        }
    }
}
