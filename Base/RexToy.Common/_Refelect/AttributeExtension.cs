using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace RexToy
{
    public static class AttributeExtension
    {
        public static T GetSingleAttribute<T>(this MemberInfo member, bool inherit = false) where T : Attribute
        {
            object[] attributes = member.GetCustomAttributes(typeof(T), inherit);
            return GetSingleOrNull<T>(attributes);
        }

        public static T GetSingleAttribute<T>(this ParameterInfo param) where T : Attribute
        {
            object[] attributes = param.GetCustomAttributes(typeof(T), false);
            return GetSingleOrNull<T>(attributes);
        }

        public static T GetSingleAttribute<T>(this Assembly assembly) where T : Attribute
        {
            object[] attributes = assembly.GetCustomAttributes(typeof(T), false);
            return GetSingleOrNull<T>(attributes);
        }

        private static T GetSingleOrNull<T>(object[] attributes) where T : Attribute
        {
            switch (attributes.Length)
            {
                case 0:
                    return null;
                case 1:
                    return (T)(attributes[0]);
                default:
                    ThrowHelper.ThrowMultipleAttributeExist(typeof(T));
                    return default(T);
            }
        }

        public static T[] GetAttributes<T>(this MemberInfo member, bool inherit = false) where T : Attribute
        {
            return (T[])member.GetCustomAttributes(typeof(T), inherit);
        }

        public static T[] GetAttributes<T>(this ParameterInfo param, bool inherit = false) where T : Attribute
        {
            return (T[])param.GetCustomAttributes(typeof(T), inherit);
        }
    }
}
