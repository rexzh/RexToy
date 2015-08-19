using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RexToy.Json
{
    public class DictionaryConverter : IExtendConverter
    {
        public bool CanConvert(Type t)
        {
            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Dictionary<,>))
            {
                return true;
            }

            return false;
        }

        public string ToJsonString(object instance)
        {
            var dict = instance as IEnumerable;
            StringBuilder str = new StringBuilder();
            foreach (dynamic kvp in dict)
            {
                str.AppendFormat("\"{0}\":{1},", kvp.Key, JsonExtension.ToJsonString(kvp.Value));
            }
            if (str.Length > 0)
                str.RemoveEnd(',');

            return str.Bracketing(StringPair.CurlyBracket).ToString();
        }

        public object FromJson(Type t, object obj, bool ignoreTypeSafe = false)
        {
            if (obj == null)
                return null;

            var json = obj as JsonObject;
            if (json != null)
            {
                var dict = Activator.CreateInstance(t) as IDictionary;

                var eleType = t.GetGenericArguments()[1];
                foreach (string key in json.Keys)
                {
                    var item = json[key];
                    var data = JsonHelper.Render(item, eleType, ignoreTypeSafe);
                    dict[key] = data;
                }
                return dict;
            }

            ExceptionHelper.ThrowExtendConverterUnknownFormat(this.GetType(), obj.GetType());
            return null;
        }
    }
}
