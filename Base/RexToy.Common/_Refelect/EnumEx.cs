using System;
using System.Collections.Generic;
using System.Reflection;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace RexToy
{
    public static class EnumEx
    {
        public static T Parse<T>(int val, bool check = true)
        {
            return (T)Parse(typeof(T), val, check);
        }

        public static object Parse(Type enumType, int val, bool check = true)
        {
            if (!enumType.IsEnum)
                ThrowHelper.ThrowInvalidEnumType(enumType);
            if (check && (!Enum.IsDefined(enumType, val)))
                throw ThrowHelper.CreateInvalidEnumValue(enumType, val);
            string str = Enum.Format(enumType, val, "g");
            return Enum.Parse(enumType, str);
        }

        [SuppressMessage("Microsoft.Design", "CA1031")]
        public static object Parse(Type enumType, string str, bool ignoreCase = false)
        {
            if (!enumType.IsEnum)
                ThrowHelper.ThrowInvalidEnumType(enumType);

            try
            {
                return Enum.Parse(enumType, str, ignoreCase);
            }
            catch (Exception ex)
            {
                throw ThrowHelper.CreateInvalidEnumValue(ex, enumType, str);
            }
        }

        public static T Parse<T>(string str, bool ignoreCase = false)
        {
            return (T)EnumEx.Parse(typeof(T), str, ignoreCase);
        }

        public static IEnumerable<T> GetValues<T>()
        {
            if (!typeof(T).IsEnum)
                ThrowHelper.ThrowInvalidEnumType(typeof(T));
            foreach (T val in Enum.GetValues(typeof(T)))
                yield return val;
        }

        public static bool IsValid(this Enum val)
        {
            Type enumType = val.GetType();
            return Enum.IsDefined(enumType, val);
        }

        public static string GetDescription(this Enum val, string fallback)
        {
            fallback.ThrowIfNullArgument(nameof(fallback));

            FieldInfo fi = val.GetType().GetField(val.ToString());
            DescriptionAttribute attr = fi.GetSingleAttribute<DescriptionAttribute>(false);
            if (attr != null)
                return attr.Description;
            else
                return fallback;
        }

        public static string GetDescription(this Enum val)
        {
            val.ThrowIfEnumOutOfRange();

            FieldInfo fi = val.GetType().GetField(val.ToString());
            DescriptionAttribute attr = fi.GetSingleAttribute<DescriptionAttribute>(false);
            if (attr != null)
                return attr.Description;
            else
                return val.ToString();
        }
    }
}
