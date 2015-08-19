using System;
using System.Diagnostics;
using System.Reflection;

namespace RexToy
{
    public static class ExceptionExtension
    {
        public const string WRAP_EXP = "Error occur, check inner exception for detail.";

        public static void ThrowIfNullArgument(this object x, string paramName)
        {
            if (x == null)
            {
                MethodBase method = new StackFrame(1).GetMethod();
                string call = string.Format("method [{0}.{1}]", method.ReflectedType.Name, method.Name);
                throw new ArgumentNullException(paramName, call);
            }
        }

        public static void ThrowIfNullOrEmptyArgument(this string x, string paramName)
        {
            if (string.IsNullOrEmpty(x))
            {
                MethodBase method = new StackFrame(1).GetMethod();
                string call = string.Format("method [{0}.{1}]", method.ReflectedType.Name, method.Name);
                throw new ArgumentNullException(paramName, call);
            }
        }

        public static void ThrowIfEnumOutOfRange(this Enum e)
        {
            if (!e.IsValid())
                throw new ArgumentOutOfRangeException(string.Format("[{0}] is not a valid value of type [{1}].", e, e.GetType()));
        }

        public static T CreateWrapException<T>(this Exception ex, string msg = WRAP_EXP) where T : Exception
        {
            T wrap = Activator.CreateInstance(typeof(T), msg, ex) as T;
            return wrap;
        }
    }
}
