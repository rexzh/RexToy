using System;
using System.Collections.Generic;

namespace RexToy.Validation
{
    class NoErrorValidateResult : IValidateResult
    {
        public IEnumerable<string> ErrorKeys
        {
            get { return new string[] { }; }
        }

        public bool HasError
        {
            get { return false; }
        }

        public bool IsError(string propertyName)
        {
            return false;
        }

        public void Set(string key, string msg)
        {
        }

        public string this[string propertyName]
        {
            get { return null; }
        }
    }
}
