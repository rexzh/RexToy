using System;
using System.Collections.Generic;
using System.Reflection;

namespace RexToy.IoC
{
    static class PolicyUtility
    {
        private const string NULL = "null";

        public static bool IsReady(string val, Type targetType, IObjectBuildContext ctx)
        {
            if (string.IsNullOrEmpty(val))
                return ctx.Kernal.ReadyToBuild(targetType);

            if (val.BracketedBy(StringPair.Template_Bracket))
            {
                string id = val.UnBracketing(StringPair.Template_Bracket);
                if (id == NULL)
                    return true;
                else
                    return ctx.Kernal.ReadyToBuild(id);
            }
            else
                return true;
        }

        public static object Build(string val, Type targetType, IObjectBuildContext ctx)
        {
            //Note:If it is injection, save it in to ctx.InjectedParameters
            if (string.IsNullOrEmpty(val))
            {
                object result = ctx.Kernal.Lookup(targetType);
                ctx.InjectedParameters.Add(result);
                return result;
            }

            if (val.BracketedBy(StringPair.Template_Bracket))
            {
                string id = val.UnBracketing(StringPair.Template_Bracket);
                if (id == NULL)
                    return null;
                object result = ctx.Kernal.Lookup(id);
                ctx.InjectedParameters.Add(result);
                return result;
            }
            else
            {
                return TypeCast.ChangeToTypeOrNullableType(val, targetType);
            }
        }
    }
}
