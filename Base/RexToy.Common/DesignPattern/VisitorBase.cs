using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;

namespace RexToy.DesignPattern
{
    public abstract class VisitorBase<R>
    {
        private const string VISIT = "Visit";
        [DebuggerStepThrough]
        public R Visit(Visitable visitable)
        {
            Type t = visitable.GetType();

            object[] _params = new object[] { visitable };
            MethodBase mb = this.GetType().GetMethod(VISIT, Type.GetTypeArray(_params));

            Assertion.IsNotNull(mb, "Visitor class not define method to visit type {0}", t);
            return (R)mb.Invoke(this, _params);
        }
    }
    
    public abstract class VisitorBase<R, T1> : VisitorBase<R>
    {
        public abstract R Visit(T1 obj);
    }

    [SuppressMessage("Microsoft.Design", "CA1005")]
    public abstract class VisitorBase<R, T1, T2> : VisitorBase<R>
    {
        public abstract R Visit(T1 obj);
        public abstract R Visit(T2 obj);
    }

    [SuppressMessage("Microsoft.Design", "CA1005")]
    public abstract class VisitorBase<R, T1, T2, T3> : VisitorBase<R>
    {
        public abstract R Visit(T1 obj);
        public abstract R Visit(T2 obj);
        public abstract R Visit(T3 obj);
    }

    [SuppressMessage("Microsoft.Design", "CA1005")]
    public abstract class VisitorBase<R, T1, T2, T3, T4> : VisitorBase<R>
    {
        public abstract R Visit(T1 obj);
        public abstract R Visit(T2 obj);
        public abstract R Visit(T3 obj);
        public abstract R Visit(T4 obj);
    }

    [SuppressMessage("Microsoft.Design", "CA1005")]
    public abstract class VisitorBase<R, T1, T2, T3, T4, T5> : VisitorBase<R>
    {
        public abstract R Visit(T1 obj);
        public abstract R Visit(T2 obj);
        public abstract R Visit(T3 obj);
        public abstract R Visit(T4 obj);
        public abstract R Visit(T5 obj);
    }

    [SuppressMessage("Microsoft.Design", "CA1005")]
    public abstract class VisitorBase<R, T1, T2, T3, T4, T5, T6> : VisitorBase<R>
    {
        public abstract R Visit(T1 obj);
        public abstract R Visit(T2 obj);
        public abstract R Visit(T3 obj);
        public abstract R Visit(T4 obj);
        public abstract R Visit(T5 obj);
        public abstract R Visit(T6 obj);
    }

    [SuppressMessage("Microsoft.Design", "CA1005")]
    public abstract class VisitorBase<R, T1, T2, T3, T4, T5, T6, T7> : VisitorBase<R>
    {
        public abstract R Visit(T1 obj);
        public abstract R Visit(T2 obj);
        public abstract R Visit(T3 obj);
        public abstract R Visit(T4 obj);
        public abstract R Visit(T5 obj);
        public abstract R Visit(T6 obj);
        public abstract R Visit(T7 obj);
    }

    [SuppressMessage("Microsoft.Design", "CA1005")]
    public abstract class VisitorBase<R, T1, T2, T3, T4, T5, T6, T7, T8> : VisitorBase<R>
    {
        public abstract R Visit(T1 obj);
        public abstract R Visit(T2 obj);
        public abstract R Visit(T3 obj);
        public abstract R Visit(T4 obj);
        public abstract R Visit(T5 obj);
        public abstract R Visit(T6 obj);
        public abstract R Visit(T7 obj);
        public abstract R Visit(T8 obj);
    }

    [SuppressMessage("Microsoft.Design", "CA1005")]
    public abstract class VisitorBase<R, T1, T2, T3, T4, T5, T6, T7, T8, T9> : VisitorBase<R>
    {
        public abstract R Visit(T1 obj);
        public abstract R Visit(T2 obj);
        public abstract R Visit(T3 obj);
        public abstract R Visit(T4 obj);
        public abstract R Visit(T5 obj);
        public abstract R Visit(T6 obj);
        public abstract R Visit(T7 obj);
        public abstract R Visit(T8 obj);
        public abstract R Visit(T9 obj);
    }

    [SuppressMessage("Microsoft.Design", "CA1005")]
    public abstract class VisitorBase<R, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : VisitorBase<R>
    {
        public abstract R Visit(T1 obj);
        public abstract R Visit(T2 obj);
        public abstract R Visit(T3 obj);
        public abstract R Visit(T4 obj);
        public abstract R Visit(T5 obj);
        public abstract R Visit(T6 obj);
        public abstract R Visit(T7 obj);
        public abstract R Visit(T8 obj);
        public abstract R Visit(T9 obj);
        public abstract R Visit(T10 obj);
    }

    [SuppressMessage("Microsoft.Design", "CA1005")]
    public abstract class VisitorBase<R, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : VisitorBase<R>
    {
        public abstract R Visit(T1 obj);
        public abstract R Visit(T2 obj);
        public abstract R Visit(T3 obj);
        public abstract R Visit(T4 obj);
        public abstract R Visit(T5 obj);
        public abstract R Visit(T6 obj);
        public abstract R Visit(T7 obj);
        public abstract R Visit(T8 obj);
        public abstract R Visit(T9 obj);
        public abstract R Visit(T10 obj);
        public abstract R Visit(T11 obj);
    }

    [SuppressMessage("Microsoft.Design", "CA1005")]
    public abstract class VisitorBase<R, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : VisitorBase<R>
    {
        public abstract R Visit(T1 obj);
        public abstract R Visit(T2 obj);
        public abstract R Visit(T3 obj);
        public abstract R Visit(T4 obj);
        public abstract R Visit(T5 obj);
        public abstract R Visit(T6 obj);
        public abstract R Visit(T7 obj);
        public abstract R Visit(T8 obj);
        public abstract R Visit(T9 obj);
        public abstract R Visit(T10 obj);
        public abstract R Visit(T11 obj);
        public abstract R Visit(T12 obj);
    }

    [SuppressMessage("Microsoft.Design", "CA1005")]
    public abstract class VisitorBase<R, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : VisitorBase<R>
    {
        public abstract R Visit(T1 obj);
        public abstract R Visit(T2 obj);
        public abstract R Visit(T3 obj);
        public abstract R Visit(T4 obj);
        public abstract R Visit(T5 obj);
        public abstract R Visit(T6 obj);
        public abstract R Visit(T7 obj);
        public abstract R Visit(T8 obj);
        public abstract R Visit(T9 obj);
        public abstract R Visit(T10 obj);
        public abstract R Visit(T11 obj);
        public abstract R Visit(T12 obj);
        public abstract R Visit(T13 obj);
    }

    [SuppressMessage("Microsoft.Design", "CA1005")]
    public abstract class VisitorBase<R, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : VisitorBase<R>
    {
        public abstract R Visit(T1 obj);
        public abstract R Visit(T2 obj);
        public abstract R Visit(T3 obj);
        public abstract R Visit(T4 obj);
        public abstract R Visit(T5 obj);
        public abstract R Visit(T6 obj);
        public abstract R Visit(T7 obj);
        public abstract R Visit(T8 obj);
        public abstract R Visit(T9 obj);
        public abstract R Visit(T10 obj);
        public abstract R Visit(T11 obj);
        public abstract R Visit(T12 obj);
        public abstract R Visit(T13 obj);
        public abstract R Visit(T14 obj);
    }

    [SuppressMessage("Microsoft.Design", "CA1005")]
    public abstract class VisitorBase<R, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15> : VisitorBase<R>
    {
        public abstract R Visit(T1 obj);
        public abstract R Visit(T2 obj);
        public abstract R Visit(T3 obj);
        public abstract R Visit(T4 obj);
        public abstract R Visit(T5 obj);
        public abstract R Visit(T6 obj);
        public abstract R Visit(T7 obj);
        public abstract R Visit(T8 obj);
        public abstract R Visit(T9 obj);
        public abstract R Visit(T10 obj);
        public abstract R Visit(T11 obj);
        public abstract R Visit(T12 obj);
        public abstract R Visit(T13 obj);
        public abstract R Visit(T14 obj);
        public abstract R Visit(T15 obj);
    }
}
