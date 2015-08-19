using System;
using System.Collections.Generic;

namespace RexToy.Validation
{
    public interface IValidator
    {
        IValidateResult Check(object instance, CheckPolicy policy = CheckPolicy.CheckAll);
    }
}
