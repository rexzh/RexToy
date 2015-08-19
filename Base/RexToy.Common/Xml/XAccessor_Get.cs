using System;
using System.Collections.Generic;

namespace RexToy.Xml
{
    public partial class XAccessor
    {
        public string GetStringValue(string xpath = null)
        {
            if (string.IsNullOrEmpty(xpath))
                return _node.InnerText;
            else
            {
                XAccessor x = NavigateToSingleOrNull(xpath);
                if (x != null)
                    return x._node.InnerText;
                else
                    return null;
            }
        }

        public T? GetValue<T>(string xpath = null) where T : struct
        {
            string str = GetStringValue(xpath);
            if (str == null)
                return null;
            T val = (T)Convert.ChangeType(str, typeof(T));
            return val;
        }

        public T? GetEnumValue<T>(string xpath = null) where T : struct
        {
            string str = GetStringValue(xpath);
            if (str == null)
                return null;
            if (Enum.IsDefined(typeof(T), str))
                return EnumEx.Parse<T>(str);
            else
                ExceptionHelper.ThrowInvalidCastException(str, typeof(T?));
            return null;
        }
    }
}
