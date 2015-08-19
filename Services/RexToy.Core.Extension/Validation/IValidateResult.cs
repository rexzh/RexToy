using System;
using System.Collections.Generic;

namespace RexToy.Validation
{
    public interface IValidateResult
    {
        IEnumerable<string> ErrorKeys { get; }
        bool HasError { get; }
        bool IsError(string propertyName);
        void Set(string key, string msg);
        string this[string propertyName] { get; }
    }
}
