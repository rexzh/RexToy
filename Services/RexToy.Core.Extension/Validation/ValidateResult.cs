using System;
using System.Collections.Generic;

using RexToy.Collections;

namespace RexToy.Validation
{
    public class ValidateResult : IValidateResult
    {
        public static IValidateResult NoError = new NoErrorValidateResult();

        protected Dictionary<string, string> _errors;
        public IEnumerable<string> ErrorKeys
        {
            get { return _errors.Keys; }
        }

        internal ValidateResult()
        {
            _errors = new Dictionary<string, string>();
        }

        public bool HasError
        {
            get { return _errors.Count > 0; }
        }

        public bool IsError(string propertyName)
        {
            return _errors.ContainsKey(propertyName);
        }

        public string this[string propertyName]
        {
            get
            {
                string val;
                _errors.TryGetValue(propertyName, out val);
                return val;
            }
        }

        public void Set(string key, string msg)
        {
            _errors[key] = msg;
        }
    }
}
