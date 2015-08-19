using System;
using System.Collections.Generic;

namespace RexToy.Template
{
    class TemplateContext : ITemplateContext
    {
        protected Dictionary<string, object> _table;
        public TemplateContext()
        {
            _table = new Dictionary<string, object>();
        }

        #region ITextTemplateContext Members

        public bool ExistVar(string name)
        {
            return _table.ContainsKey(name);
        }

        public void AddVar(string name)
        {
            _table.Add(name, null);
        }

        public void RemoveVar(string name)
        {
            _table.Remove(name);
        }

        #endregion

        #region IEvalContext Members

        public virtual object Resolve(string param)
        {
            if (_table.ContainsKey(param))
                return _table[param];
            else
                return null;
        }

        public void Assign(string param, object value)
        {
            _table[param] = value;
        }

        #endregion
    }
}
