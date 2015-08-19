using System;
using System.Collections.Generic;

namespace RexToy.Json
{
    public interface IExtendConverter
    {
        bool CanConvert(Type t);
        string ToJsonString(object instance);
        object FromJson(Type t, object json, bool ignoreTypeSafe = false);
    }
}
