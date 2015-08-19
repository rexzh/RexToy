using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace RexToy.Json
{
    public class JsonObject
    {
        private Dictionary<string, object> _dict;

        public JsonObject()
        {
            _dict = new Dictionary<string, object>();
        }

        public void AddKVPair(string key, object val)
        {
            _dict.Add(key, val);
        }

        public object this[string key]
        {
            get
            {
                if (_dict.ContainsKey(key))
                    return _dict[key];
                else
                    return null;
            }
        }

        public IEnumerable<string> Keys
        {
            get { return _dict.Keys; }
        }

        internal object Render(Type type, bool ignoreTypeSafe)
        {
            if (!type.HasDefaultConstructor())
                ExceptionHelper.ThrowNoDefaultConstructor(type);

            IExtendConverter cvt = ExtendConverter.Instance();
            if (cvt.CanConvert(type))
            {
                return cvt.FromJson(type, this, ignoreTypeSafe);
            }

            object instance = Activator.CreateInstance(type, true);
            IReflector r = Reflector.Bind(instance, ReflectorPolicy.InstancePublicIgnoreCase);

            foreach (KeyValuePair<string, object> kvp in _dict)
            {
                string propertyName = kvp.Key;
                Type targetType = r.GetPropertyType(propertyName);
                var data = JsonHelper.Render(kvp.Value, targetType, ignoreTypeSafe);
                r.SetPropertyValue(propertyName, data);
            }

            return instance;
        }

        internal T Render<T>(bool ignoreTypeSafe)
        {
            return (T)this.Render(typeof(T), ignoreTypeSafe);
        }
    }
}