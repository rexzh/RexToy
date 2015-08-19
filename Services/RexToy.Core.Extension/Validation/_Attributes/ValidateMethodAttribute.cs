using System;
using System.Collections.Generic;
using System.Reflection;

namespace RexToy.Validation
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ValidateMethodAttribute : BaseValidateAttribute
    {
        private MethodInfo _method;
        public void SetMethodInfo(MethodInfo method)
        {
            _method = method;

            //Note:Check method signature.
            if (method.ReturnType != typeof(void))
            {
                ExceptionHelper.ThrowInvalidValidateMethod(method);
            }

            var pInfos = method.GetParameters();
            if (pInfos.Length != 1 || pInfos[0].ParameterType != typeof(IValidateResult))
            {
                ExceptionHelper.ThrowInvalidValidateMethod(method);
            }
        }

        public override void Check(IValidateResult result, object instance)
        {
            instance.ThrowIfNullArgument(nameof(instance));
            result.ThrowIfNullArgument(nameof(result));

            _method.Invoke(instance, new object[] { result });
        }
    }
}
