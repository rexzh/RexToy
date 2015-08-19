using System;
using System.Collections.Generic;

namespace RexToy.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NotNullValidateAttribute : ValidatePropertyAttribute
    {
        private const string MSG = "Validate error: [{1}.{0}] is null.";

        protected override void DoCheck(IValidateResult result, object instance, object propertyValue)
        {
            if (propertyValue == null)
            {
                string msg = string.Format(MSG, _pInfo.Name, instance.GetType().Name);
                this.LogValidateResult(result, msg);
            }
        }
    }
}
