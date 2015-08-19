using System;
using System.Collections.Generic;

using RexToy.Logging;

namespace RexToy.ExpressionLanguage
{
    class EvalContext : IEvalContext
    {
        private Dictionary<string, object> _dict;
        public EvalContext()
        {
            _dict = new Dictionary<string, object>();
        }

        #region IEvalContext Members

        public object Resolve(string param)
        {
            object val;
            bool b = _dict.TryGetValue(param, out val);
            if (!b)
                ExceptionHelper.ThrowUnresolveException(param);
            return val;
        }

        public void Assign(string param, object val)
        {
            _dict[param] = val;
        }

        #endregion
    }
}
