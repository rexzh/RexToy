using System;
using System.Collections.Generic;
using System.Reflection;

namespace RexToy.Validation
{
    public static class ExceptionHelper
    {
        public static void ThrowInvalidPropertyAttribute(Attribute attribute, PropertyInfo pInfo)
        {
            string msg = string.Format("[{0}] can not apply to property [{1}.{2} - type is [{3}]", attribute.GetType().Name, pInfo.ReflectedType, pInfo.Name, pInfo.PropertyType.Name);
            throw new ValidationException(msg);
        }

        public static void ThrowInvalidValidateMethod(MethodInfo method)
        {
            throw new ValidationException(string.Format("[{0}.{1}] can not be validate method, signature must be void {1}(ValidateResult).", method.ReflectedType, method.Name));
        }
    }
}
