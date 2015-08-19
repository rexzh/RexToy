using System;
using System.Reflection;
using System.IO;

namespace RexToy
{
    public partial class Reflector
    {
        public static Type LoadType(string typeName, bool throwOnError = true, bool ignoreCase = false)
        {
            try
            {
                return Type.GetType(typeName, throwOnError, ignoreCase);
            }
            catch (Exception ex)
            {
                throw ex.CreateWrapException<ReflectorException>();
            }
        }

        public static Type LoadGenericType(string typeName, Type[] argTypes, bool throwOnError = true, bool ignoreCase = false)
        {
            string[] names = typeName.Split(',');
            Assertion.IsTrue(names.Length == 1 || names.Length == 2, "Generic type name [{0}] not valid.", typeName);

            string genericName = string.Format("{0}`{1}", names[0], argTypes.Length);
            string convertedName = (names.Length == 1) ? genericName : string.Format("{0},{1}", genericName, names[1]);

            Type generic = Type.GetType(convertedName, throwOnError, ignoreCase);

            if (generic == null)
            {
                if (throwOnError)
                    ThrowHelper.ThrowDynamicLoad(typeName);
                else
                    return null;
            }

            Type result = generic.MakeGenericType(argTypes);
            return result;
        }

        public static object LoadInstance(string typeName, bool throwOnError = true, bool ignoreCase = false, params object[] args)
        {
            Type type = null;
            try
            {
                type = Type.GetType(typeName, throwOnError, ignoreCase);
            }
            catch (Exception ex)
            {
                throw ex.CreateWrapException<ReflectorException>();
            }

            if (type == null)
            {
                if (throwOnError)
                    ThrowHelper.ThrowDynamicLoad(typeName);
                return null;
            }
            else
            {
                return Activator.CreateInstance(type, args);
            }
        }

        public static object LoadGenericInstance(string typeName, Type[] argTypes, bool throwOnError = true, bool ignoreCase = false, params object[] args)
        {
            Type type = LoadGenericType(typeName, argTypes, throwOnError, ignoreCase);
            if (type == null)
            {
                if (throwOnError)
                    ThrowHelper.ThrowDynamicLoad(typeName);
                return null;
            }
            else
            {
                return Activator.CreateInstance(type, args);
            }
        }

        public static T LoadInstance<T>(string typeName, bool throwOnError = true, bool ignoreCase = false, params object[] args)
        {
            object obj = LoadInstance(typeName, throwOnError, ignoreCase, args);

            try
            {
                return (T)obj;
            }
            catch (Exception ex)
            {
                if (throwOnError)
                    throw ex.CreateWrapException<ReflectorException>();
                else
                    return default(T);
            }
        }

        public static Assembly LoadAssembly(string file)
        {
            if (!File.Exists(file))
            {
                if (!(file.EndsWith(ClrClassPath.DLL) || file.EndsWith(ClrClassPath.EXE)))
                {
                    string dll = file + ClrClassPath.DLL;
                    if (File.Exists(dll))
                        return Assembly.LoadFrom(dll);

                    string exe = file + ClrClassPath.EXE;
                    if (File.Exists(exe))
                        return Assembly.LoadFrom(exe);
                }
            }

            return Assembly.LoadFrom(file);
        }

        //Extend:Load type from current directory
        //Extend:Load type from GAC directory
    }
}
