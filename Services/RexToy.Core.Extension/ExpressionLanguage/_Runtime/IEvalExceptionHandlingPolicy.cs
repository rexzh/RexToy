using System;
using System.Collections.Generic;

namespace RexToy.ExpressionLanguage
{
    public interface IEvalExceptionHandlingPolicy
    {
        bool Throw { get; }
        void Handle(Exception ex);
    }
}
