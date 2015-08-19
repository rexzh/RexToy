using System;
using System.Collections.Generic;
using System.Text;

namespace RexToy.ExpressionLanguage
{
    public interface IEvalContext
    {
        void Assign(string param, object value);
        object Resolve(string param);
    }
}
