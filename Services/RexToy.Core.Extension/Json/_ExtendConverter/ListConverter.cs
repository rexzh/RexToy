using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Threading.Tasks;

using RexToy;

namespace RexToy.Json
{
    public class ListConverter : IExtendConverter
    {
        public bool CanConvert(Type t)
        {
            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(List<>))
            {
                return true;
            }

            return false;
        }

        public string ToJsonString(object instance)
        {
            var list = instance as IEnumerable;
            StringBuilder str = new StringBuilder();
            foreach (var item in list)
            {
                str.Append(item.ToJsonString()).Append(",");
            }
            if (str.Length > 0)
                str.RemoveEnd(',');

            return str.Bracketing(StringPair.SquareBracket).ToString();
        }

        public object FromJson(Type t, object obj, bool ignoreTypeSafe = false)
        {
            if (obj == null)
                return null;

            var arr = obj as JsonArray;
            if (arr != null)
            {
                var list = Activator.CreateInstance(t) as IList;
                var eleType = t.GetGenericArguments()[0];
                for (int i = 0; i < arr.Length; i++)
                {
                    var item = arr[i];
                    var data = JsonHelper.Render(item, eleType, ignoreTypeSafe);
                    list.Add(data);
                }
                return list;
            }

            ExceptionHelper.ThrowExtendConverterUnknownFormat(this.GetType(), obj.GetType());
            return null;
        }
    }
}
