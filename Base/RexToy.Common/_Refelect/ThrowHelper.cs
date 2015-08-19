using System;
using System.Reflection;

namespace RexToy
{
    static class ThrowHelper
    {
        public static void ThrowInvalidType(string argName)
        {
            throw new ArgumentException(string.Format("[{0}] is not a valid Type instance."), argName);
        }

        public static void ThrowInvalidEnumType(Type t)
        {
            throw new ArgumentException("[{0}] is not a valid enum type.", t.Name);
        }

        public static ArgumentException CreateInvalidEnumValue(Type enumType, object val)
        {
            return new ArgumentException(string.Format("Value [{0}] is not valid for enum type [{1}].", val, enumType.Name));
        }

        public static ArgumentException CreateInvalidEnumValue(Exception inner, Type enumType, object val)
        {
            return new ArgumentException(string.Format("Value [{0}] is not valid for enum type [{1}].", val, enumType.Name), inner);
        }

        public static void ThrowMemberNotExist(Type t, string member, MemberTypes memberType)
        {
            string msg = string.Format("Type [{0}] don't have member([{1}]): [{2}].", t.Name, memberType, member);
            throw new ReflectorException(msg);
        }

        public static void ThrowAmbiguousMethod(Type t, string method)
        {
            string msg = string.Format("Ambiguous method call: [{0}.{1}].", t.Name, method);
            throw new ReflectorException(msg);
        }

        public static void ThrowDynamicLoad(string typeName)
        {
            string msg = string.Format("Load [{0}] fail.", typeName);
            throw new ReflectorException(msg);
        }

        public static void ThrowMultipleAttributeExist(Type attrType)
        {
            string msg = string.Format("Multiple [{0}] exist, try use GetAttributes() instead.", attrType.Name);
            throw new ReflectorException(msg);
        }

        public static void ThrowInvalidClrClassPath(string clrClassPath)
        {
            throw new ArgumentException(string.Format("Clr-ClassPath [{0}] format is not valid.", clrClassPath));
        }
    }
}
