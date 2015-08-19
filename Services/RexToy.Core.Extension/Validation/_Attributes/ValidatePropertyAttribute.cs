using System;
using System.Collections.Generic;
using System.Reflection;

namespace RexToy.Validation
{
    public abstract class ValidatePropertyAttribute : BaseValidateAttribute
    {
        protected PropertyInfo _pInfo;
        public void SetPropertyInfo(PropertyInfo pInfo)
        {
            _pInfo = pInfo;
        }

        protected void LogValidateResult(IValidateResult result, string msg)
        {
            result.Set(_pInfo.Name, msg);
        }

        public override void Check(IValidateResult result, object instance)
        {
            Assertion.IsNotNull(_pInfo, "Property info not set.");

            instance.ThrowIfNullArgument(nameof(instance));
            result.ThrowIfNullArgument(nameof(result));

            object val = _pInfo.GetValue(instance, null);
            DoCheck(result, instance, val);
        }

        protected abstract void DoCheck(IValidateResult result, object instance, object propertyValue);
    }
}
