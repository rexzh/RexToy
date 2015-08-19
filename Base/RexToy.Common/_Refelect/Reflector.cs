using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Reflection;

namespace RexToy
{
    public partial class Reflector : IReflector
    {
        private const string ITEM = "Item";

        [DebuggerStepThrough]
        public static IReflector Bind(object obj, IReflectPolicy policy)
        {
            obj.ThrowIfNullArgument(nameof(obj));

            Reflector r = new Reflector();
            if (policy.BindInstance)
            {
                r._instance = obj;
                r._type = obj.GetType();
            }
            else
            {
                r._type = obj as Type;
                if (r._type == null)
                    ThrowHelper.ThrowInvalidType("obj");
            }
            r._policy = policy;
            return r;
        }

        [DebuggerStepThrough]
        public static IReflector Bind(object obj)
        {
            return Reflector.Bind(obj, ReflectorPolicy.InstancePublic);
        }

        private object _instance;
        private Type _type;
        private IReflectPolicy _policy;

        #region IReflector Members


        public object BoundInstance
        {
            [DebuggerStepThrough]
            get { return _instance; }
        }

        public Type BoundType
        {
            [DebuggerStepThrough]
            get { return _type; }
        }

        public IReflectPolicy Policy
        {
            [DebuggerStepThrough]
            get { return _policy; }
        }

        [DebuggerStepThrough]
        public IEnumerable<FieldInfo> FindAllFields()
        {
            return _type.GetFields(_policy.BindingFlags);
        }

        [DebuggerStepThrough]
        public IEnumerable<string> FindAllFieldNames()
        {
            foreach (FieldInfo info in FindAllFields())
                yield return info.Name;
        }

        [DebuggerStepThrough]
        public Type GetFieldType(string fieldName)
        {
            FieldInfo info = _type.GetField(fieldName, _policy.BindingFlags);
            if (info == null)
                ThrowHelper.ThrowMemberNotExist(_type, fieldName, MemberTypes.Field);
            return info.FieldType;
        }

        [DebuggerStepThrough]
        public object GetFieldValue(string fieldName, bool throwOnNotExist)
        {
            FieldInfo info = _type.GetField(fieldName, _policy.BindingFlags);
            if (info == null)
            {
                if (throwOnNotExist)
                    ThrowHelper.ThrowMemberNotExist(_type, fieldName, MemberTypes.Field);
                return null;
            }
            else
                return info.GetValue(_instance);
        }

        [DebuggerStepThrough]
        public void SetFieldValue(string fieldName, object val, bool throwOnNotExist)
        {
            FieldInfo info = _type.GetField(fieldName, _policy.BindingFlags);
            if (info == null)
            {
                if (throwOnNotExist)
                    ThrowHelper.ThrowMemberNotExist(_type, fieldName, MemberTypes.Field);
            }
            else
                info.SetValue(_instance, val);
        }

        [DebuggerStepThrough]
        public bool ExistField(string fieldName)
        {
            FieldInfo info = _type.GetField(fieldName, _policy.BindingFlags);
            return info != null;
        }

        [DebuggerStepThrough]
        public IEnumerable<PropertyInfo> FindAllProperties()
        {
            return _type.GetProperties(_policy.BindingFlags);
        }

        [DebuggerStepThrough]
        public IEnumerable<string> FindAllPropertyNames()
        {
            foreach (PropertyInfo info in FindAllProperties())
                yield return info.Name;
        }

        [DebuggerStepThrough]
        public Type GetPropertyType(string propName)
        {
            PropertyInfo info = _type.GetProperty(propName, _policy.BindingFlags);
            if (info == null)
                ThrowHelper.ThrowMemberNotExist(_type, propName, MemberTypes.Property);
            return info.PropertyType;
        }

        [DebuggerStepThrough]
        public object GetPropertyValue(string propName, bool throwOnNotExist)
        {
            PropertyInfo info = _type.GetProperty(propName, _policy.BindingFlags);
            if (info == null)
            {
                if (throwOnNotExist)
                    ThrowHelper.ThrowMemberNotExist(_type, propName, MemberTypes.Property);
                return null;
            }
            else
                return info.GetValue(_instance, null);
        }

        [DebuggerStepThrough]
        public void SetPropertyValue(string propName, object val, bool throwOnNotExist)
        {
            PropertyInfo info = _type.GetProperty(propName, _policy.BindingFlags);
            if (info == null)
            {
                if (throwOnNotExist)
                    ThrowHelper.ThrowMemberNotExist(_type, propName, MemberTypes.Property);
            }
            else
                info.SetValue(_instance, val, null);
        }

        [DebuggerStepThrough]
        public bool ExistProperty(string propName)
        {
            PropertyInfo info = _type.GetProperty(propName, _policy.BindingFlags);
            return info != null;
        }

        [DebuggerStepThrough]
        public object GetIndexerValue(object[] objects)
        {
            Type[] types = Type.GetTypeArray(objects);
            PropertyInfo pi = _type.GetProperty(ITEM, _policy.BindingFlags, null, null, types, null);
            if (pi == null)
                ThrowHelper.ThrowMemberNotExist(_type, ITEM, MemberTypes.Method);
            return pi.GetValue(_instance, objects);
        }

        [DebuggerStepThrough]
        public void SetIndexerValue(object[] objects, object val)
        {
            Type[] types = Type.GetTypeArray(objects);
            PropertyInfo pi = _type.GetProperty(ITEM, _policy.BindingFlags, null, null, types, null);
            pi.SetValue(_instance, val, objects);
        }

        [DebuggerStepThrough]
        public IEnumerable<ConstructorInfo> FindAllConstructors()
        {
            return _type.GetConstructors(_policy.BindingFlags);
        }

        [DebuggerStepThrough]
        public IEnumerable<MethodInfo> GetMethods(string methodName)
        {
            MethodInfo[] methods = _type.GetMethods(_policy.BindingFlags);
            foreach (MethodInfo mi in methods)
            {
                if (mi.Name == methodName)
                    yield return mi;
            }
        }

        [DebuggerStepThrough]
        public object Invoke(string method, object[] args)
        {
            try
            {
                return _type.InvokeMember(method, _policy.BindingFlags, null, _instance, args);
            }
            catch (Exception ex)
            {
                throw ex.CreateWrapException<ReflectorException>();
            }
        }

        [DebuggerStepThrough]
        public object GetPropertyOrFieldValue(string memberName, bool throwOnNotExist)
        {
            PropertyInfo pInfo = _type.GetProperty(memberName, _policy.BindingFlags);
            if (pInfo != null)
                return pInfo.GetValue(_instance, null);

            FieldInfo fInfo = _type.GetField(memberName, _policy.BindingFlags);
            if (fInfo != null)
                return fInfo.GetValue(_instance);

            if (pInfo == null && fInfo == null && throwOnNotExist)
                ThrowHelper.ThrowMemberNotExist(_type, memberName, MemberTypes.Property | MemberTypes.Field);
            return null;
        }

        [DebuggerStepThrough]
        public void SetPropertyOrFieldValue(string memberName, object val, bool throwOnNotExist)
        {
            PropertyInfo pInfo = _type.GetProperty(memberName, _policy.BindingFlags);
            if (pInfo != null)
                pInfo.SetValue(_instance, val, null);

            FieldInfo fInfo = _type.GetField(memberName, _policy.BindingFlags);
            if (fInfo != null)
                fInfo.SetValue(_instance, val);

            if (pInfo == null && fInfo == null && throwOnNotExist)
                ThrowHelper.ThrowMemberNotExist(_type, memberName, MemberTypes.Property | MemberTypes.Field);
        }

        #endregion
    }
}
