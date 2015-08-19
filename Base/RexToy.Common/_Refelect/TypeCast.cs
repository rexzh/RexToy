using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace RexToy
{
    public static class TypeCast
    {
        public static T ChangeType<T>(object val)
        {
            return (T)ChangeType(val, typeof(T));
        }

        public static object ChangeType(object val, Type targetType)
        {
            if (val == null)
                return null;

            Type valType = val.GetType();
            if (valType.IsOrIsSubclassOf(targetType))
                return val;
            if (targetType.IsInterface && valType.Implemented(targetType))
                return val;

            TypeConverter cvt = TypeDescriptor.GetConverter(targetType);
            if (cvt != null && cvt.CanConvertFrom(val.GetType()))
                return cvt.ConvertFrom(val);
            else
                return Convert.ChangeType(val, targetType);
        }

        public static object ChangeToTypeOrNullableType(object val, Type targetType)
        {
            if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                if (val != null)
                    return ChangeType(val, Nullable.GetUnderlyingType(targetType));
                else
                    return null;
            }
            else
            {
                return ChangeType(val, targetType);
            }
        }

        public static T ChangeToTypeOrNullableType<T>(object val)
        {
            return (T)ChangeToTypeOrNullableType(val, typeof(T));
        }
    }
}
