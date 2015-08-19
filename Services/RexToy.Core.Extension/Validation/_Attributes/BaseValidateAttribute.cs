using System;
using System.Reflection;
using System.Collections.Generic;

namespace RexToy.Validation
{
    public abstract class BaseValidateAttribute : Attribute
    {
        public abstract void Check(IValidateResult result, object instance);
    }
}
