using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Reflection;

namespace RexToy
{
    public static class TypeExtension
    {
        public static object DefaultValue(this Type t)
        {
            t.ThrowIfNullArgument(nameof(t));
            //if (t.IsInterface || t.IsAbstract)
            //    return null;
            if (t.IsClass)
                return null;
            else
                return Activator.CreateInstance(t);
        }

        public static bool Implemented(this Type t, Type interfaceType)
        {
            Assertion.IsTrue(interfaceType.IsInterface, "{0} is not interface type", interfaceType.FullName);
            return t.GetInterface(interfaceType.FullName, false) != null;
        }

        public static bool IsOrIsSubclassOf(this Type t, Type baseType)
        {
            return (t == baseType) || t.IsSubclassOf(baseType);
        }

        [DebuggerStepThrough]
        public static bool HasDefaultConstructor(this Type t)
        {
            IReflector r = Reflector.Bind(t, ReflectorPolicy.CreateInstance(true, false, false, false));
            foreach (var ctor in r.FindAllConstructors())
            {
                if (ctor.GetParameters().Length == 0)
                    return true;
            }
            return false;
        }

        [DebuggerStepThrough]
        public static bool HasPublicConstructor(this Type t)
        {
            IReflector r = Reflector.Bind(t, ReflectorPolicy.CreateInstance(false, false, false, false));
            foreach (var ctor in r.FindAllConstructors())
            {
                if (ctor.IsPublic)
                    return true;
            }
            return false;
        }

        private static string Delimiter = ", ";
        public static string GetScatterName(this Type t)
        {
            if (t.IsGenericType)
            {
                Type gt = t.GetGenericTypeDefinition();
                int idx = gt.FullName.IndexOf('`');
                string name = gt.FullName.Substring(0, idx);

                string argName = string.Empty;
                foreach (Type argType in t.GetGenericArguments())
                {
                    argName += GetScatterName(argType);
                    argName += Delimiter;
                }
                name += argName.RemoveEnd(Delimiter).Bracketing(StringPair.AngleBracket);
                return name;
            }
            else
                return t.FullName;
        }
    }
}
