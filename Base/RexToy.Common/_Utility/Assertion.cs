using System;
using System.Diagnostics;

namespace RexToy
{
    public static class Assertion
    {
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public static void IsTrue(bool val, string msg, params object[] args)
        {
            if (!val)
                throw new AssertException(msg.TryFormat(args) + Assertion.CallPosition());
        }

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public static void IsFalse(bool val, string msg, params object[] args)
        {
            if (val)
                throw new AssertException(msg.TryFormat(args) + Assertion.CallPosition());
        }

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public static void AreEqual(object expected, object result, string msg, params object[] args)
        {
            if (!expected.Equals(result))
                throw new AssertException(msg.TryFormat(args) + Assertion.CallPosition());
        }

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public static void IsNotNull(object val, string msg, params object[] args)
        {
            if (val == null)
                throw new AssertException(msg.TryFormat(args) + Assertion.CallPosition());
        }

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public static void IsNull(object val, string msg, params object[] args)
        {
            if (val != null)
                throw new AssertException(msg.TryFormat(args) + Assertion.CallPosition());
        }

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public static void Fail(string msg, params object[] args)
        {
            throw new AssertException(msg.TryFormat(args) + Assertion.CallPosition());
        }

        private static string CallPosition()
        {
            StackFrame frame = new StackFrame(2, true);
            return frame.ToString();
        }
    }
}
