using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using RexToy;

namespace RexToy.Json
{
    public class EnumConverter : IExtendConverter
    {
        public bool CanConvert(Type t)
        {
            return t.IsEnum;
        }

        public string ToJsonString(object instance)
        {
            return instance.ToString().Bracketing(StringPair.DoubleQuote);
        }

        public object FromJson(Type t, object json, bool ignoreTypeSafe = false)
        {
            var str = (json as string).UnBracketing(StringPair.DoubleQuote);
            return EnumEx.Parse(t, str, true);
        }
    }
}
