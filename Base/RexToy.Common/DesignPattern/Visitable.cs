using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RexToy.DesignPattern
{
    public abstract class Visitable
    {
        [DebuggerStepThrough]
        public R Accept<R>(VisitorBase<R> v)
        {
            return v.Visit(this);
        }
    }
}
