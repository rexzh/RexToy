using System;
using System.Collections;
using System.Collections.Generic;

namespace RexToy.Json
{
    public class JsonArray
    {
        private ArrayList _array;

        public int Length
        {
            get { return _array.Count; }
        }

        public JsonArray()
        {
            _array = new ArrayList();
        }

        public void AddValue(object val)
        {
            _array.Add(val);
        }

        public object this[int index]
        {
            get
            {
                if (index > _array.Count - 1)
                    return null;
                else
                    return _array[index];
            }
        }

        public object[] ToObjectArray()
        {
            return _array.ToArray();
        }

        internal object Render(Type type, bool ignoreTypeSafe)
        {
            IExtendConverter cvt = ExtendConverter.Instance();
            if (cvt.CanConvert(type))
            {
                return cvt.FromJson(type, this, ignoreTypeSafe);
            }

            Type elementType = type.GetElementType();
            Array array = (Array)Activator.CreateInstance(elementType.MakeArrayType(), this.Length);

            for (int i = 0; i < _array.Count; i++)
            {
                object jsonElement = _array[i];
                var data = JsonHelper.Render(jsonElement, elementType, ignoreTypeSafe);

                array.SetValue(data, i);
            }

            return array;
        }

        internal T[] Render<T>(bool ignoreTypeSafe)
        {
            return (T[])this.Render(typeof(T), ignoreTypeSafe);
        }
    }
}
