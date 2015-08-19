using System;
using System.Collections.Generic;
using System.Reflection;

namespace RexToy.ExpressionLanguage
{
    class EvalSmartInvoker : SmartInvoker
    {
        public static ISmartInvoker CreateInstance(object obj, bool bindInstance)
        {
            return new EvalSmartInvoker(obj, bindInstance);
        }

        private EvalSmartInvoker(object obj, bool bindInstance)
            : base(obj, ReflectorPolicy.CreateInstance(false, true, false, bindInstance))
        {
        }

        private bool TryConvertOneArgument(object val, Type targetType, out object result)
        {
            //Extend:try improve performance here
            try
            {
                result = TypeCast.ChangeToTypeOrNullableType(val, targetType);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }

        private bool TryConvertArgumentArray(MethodBase method, object[] args, object[] convertedArgs, int length)
        {
            Type[] paramTypeArray = method.GetParamsTypeArray();
            for (int i = 0; i < length; i++)
            {
                if (!TryConvertOneArgument(args[i], paramTypeArray[i], out convertedArgs[i]))
                    return false;
            }
            return true;
        }

        private void RetrieveOptArgDefault(MethodBase method, object[] convertedArgs, int startIndex, int lenth)
        {
            ParameterInfo[] paramInfoArray = method.GetParameters();
            for (int i = 0; i < lenth; i++)
            {
                convertedArgs[startIndex + i] = paramInfoArray[startIndex + i].DefaultValue;
            }
        }

        private bool TryBuildVarArgArray(MethodBase method, object[] args, object[] convertedArgs)
        {
            Type[] paramTypeArray = method.GetParamsTypeArray();
            int last = paramTypeArray.Length - 1;
            object val;
            Array varArr;

            if (args.Length < convertedArgs.Length)
            {
                varArr = (Array)Activator.CreateInstance(paramTypeArray[last], 0);
            }
            else if (args.Length > convertedArgs.Length)
            {
                varArr = (Array)Activator.CreateInstance(paramTypeArray[last], args.Length - paramTypeArray.Length + 1);
                for (int i = 0; i < varArr.Length; i++)
                {
                    if (!TryConvertOneArgument(args[i + paramTypeArray.Length - 1], method.GetVarArgElementType(), out val))
                        return false;
                    else
                        varArr.SetValue(val, i);
                }
            }
            else
            {
                varArr = (Array)Activator.CreateInstance(paramTypeArray[last], 1);
                if (!TryConvertOneArgument(args[last], method.GetVarArgElementType(), out val))
                    return false;
                else
                    varArr.SetValue(val, 0);
            }

            convertedArgs[last] = varArr;
            return true;
        }

        protected override bool Convert(MethodBase method, object[] args, out object[] convertedArgs)
        {
            convertedArgs = new object[method.DeclareParamCount()];
            //Note:if there is args omit(opt/var), use default value or empty array to adjust the number of args.
            if (args.Length >= convertedArgs.Length)
            {
                if (method.IsVarArgs())//Note:Pass in args is more than declare, try convert and build vararg array.
                {
                    if (!TryConvertArgumentArray(method, args, convertedArgs, convertedArgs.Length - 1))
                        return false;
                    return TryBuildVarArgArray(method, args, convertedArgs);
                }
                else if (args.Length == convertedArgs.Length)//Note:if equal, it might be exact match.
                    return TryConvertArgumentArray(method, args, convertedArgs, args.Length);
            }
            else//Note:Check with optarg/vararg, both is possible
            {
                if (method.IsOptArg(args.Length) || method.IsVarArgs())
                {
                    if (!TryConvertArgumentArray(method, args, convertedArgs, args.Length))
                        return false;
                    if (method.IsVarArgs())
                    {
                        RetrieveOptArgDefault(method, convertedArgs, args.Length, method.DeclareParamCount() - args.Length - 1);
                        return TryBuildVarArgArray(method, args, convertedArgs);
                    }
                    else
                    {
                        RetrieveOptArgDefault(method, convertedArgs, args.Length, method.DeclareParamCount() - args.Length);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
