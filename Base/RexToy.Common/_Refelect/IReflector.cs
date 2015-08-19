using System;
using System.Collections.Generic;
using System.Reflection;

namespace RexToy
{
    public interface IReflector
    {
        IReflectPolicy Policy { get; }
        object BoundInstance { get; }
        Type BoundType { get; }

        IEnumerable<FieldInfo> FindAllFields();
        IEnumerable<string> FindAllFieldNames();
        Type GetFieldType(string fieldName);
        object GetFieldValue(string fieldName, bool throwOnNotExist = true);
        void SetFieldValue(string fieldName, object val, bool throwOnNotExist = true);
        bool ExistField(string fieldName);

        IEnumerable<PropertyInfo> FindAllProperties();
        IEnumerable<string> FindAllPropertyNames();
        Type GetPropertyType(string propName);
        object GetPropertyValue(string propName, bool throwOnNotExist = true);
        void SetPropertyValue(string propName, object val, bool throwOnNotExist = true);
        bool ExistProperty(string propName);

        object GetPropertyOrFieldValue(string memberName, bool throwOnNotExist = true);
        void SetPropertyOrFieldValue(string memberName, object val, bool throwOnNotExist = true);

        object GetIndexerValue(object[] objects);
        void SetIndexerValue(object[] objects, object val);

        IEnumerable<ConstructorInfo> FindAllConstructors();

        IEnumerable<MethodInfo> GetMethods(string methodName);
        object Invoke(string method, object[] args);
    }
}
