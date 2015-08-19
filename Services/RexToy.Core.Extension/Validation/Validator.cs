using System;
using System.Collections.Generic;
using System.Reflection;

namespace RexToy.Validation
{
    class Validator : IValidator
    {
        private List<BaseValidateAttribute> _rules;
        public Validator(Type targetType)
        {
            _rules = new List<BaseValidateAttribute>();

            CustomizeValidateAttribute customVal = targetType.GetSingleAttribute<CustomizeValidateAttribute>();
            if (customVal != null)
            {
                foreach (var method in targetType.GetMethods())
                {
                    var validateMethodAttr = method.GetSingleAttribute<ValidateMethodAttribute>();
                    if (validateMethodAttr != null)
                    {
                        validateMethodAttr.SetMethodInfo(method);
                        _rules.Add(validateMethodAttr);
                    }
                }
            }

            foreach (var property in targetType.GetProperties())
            {
                foreach (var attr in Attribute.GetCustomAttributes(property))
                {
                    ValidatePropertyAttribute v = attr as ValidatePropertyAttribute;
                    if (v != null)
                    {
                        v.SetPropertyInfo(property);
                        _rules.Add(v);
                    }
                }
            }
        }

        public IValidateResult Check(object instance, CheckPolicy policy = CheckPolicy.CheckAll)
        {
            ValidateResult result = new ValidateResult();
            foreach (var rule in _rules)
            {
                rule.Check(result, instance);
                if (policy == CheckPolicy.StopOnError && result.HasError)
                    break;
            }

            return result;
        }
    }
}
