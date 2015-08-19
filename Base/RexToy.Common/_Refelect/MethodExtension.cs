using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;

namespace RexToy
{
    public static class MethodExtension
    {
        public static bool IsVarArgs(this MethodBase method)
        {
            ParameterInfo[] paramInfos = method.GetParameters();
            if (paramInfos.Length == 0)
                return false;

            ParameterInfo last = paramInfos[paramInfos.Length - 1];
            ParamArrayAttribute paAttr = last.GetSingleAttribute<ParamArrayAttribute>();
            return paAttr != null;
        }

        public static bool HasOptionalArgs(this MethodBase method)
        {
            ParameterInfo[] paramInfos = method.GetParameters();
            if (paramInfos.Length == 0)
                return false;
            for (int i = paramInfos.Length - 1; i >= 0; i--)
            {
                if (paramInfos[i].IsOptional)
                    return true;
            }
            return false;
        }

        public static Type[] GetParamsTypeArray(this MethodBase method)
        {
            ParameterInfo[] paramInfos = method.GetParameters();
            Type[] result = new Type[paramInfos.Length];
            for (int index = 0; index < result.Length; index++)
                result[index] = paramInfos[index].ParameterType;
            return result;
        }

        public static int DeclareParamCount(this MethodBase method)
        {
            ParameterInfo[] paramInfos = method.GetParameters();
            return paramInfos.Length;
        }

        public static int FirstOptArgIndex(this MethodBase method)
        {
            ParameterInfo[] paramInfos = method.GetParameters();
            if (paramInfos.Length == 0)
                return -1;
            for (int i = 0; i < paramInfos.Length; i++)
            {
                if (paramInfos[i].IsOptional)
                    return i;
            }
            return -1;
        }

        public static bool IsOptArg(this MethodBase method, int index)
        {
            ParameterInfo[] paramInfos = method.GetParameters();
            return paramInfos[index].IsOptional;
        }

        public static Type GetVarArgElementType(this MethodBase method)
        {
            ParameterInfo[] paramInfos = method.GetParameters();
            if (paramInfos.Length == 0)
                return null;

            ParameterInfo last = paramInfos[paramInfos.Length - 1];
            ParamArrayAttribute paAttr = last.GetSingleAttribute<ParamArrayAttribute>();
            if (paAttr == null)
                return null;
            else
            {
                Type elementType = last.ParameterType.GetElementType();
                return elementType;
            }
        }
    }
}
