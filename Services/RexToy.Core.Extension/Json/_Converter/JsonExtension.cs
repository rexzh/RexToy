using System;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;
using System.Reflection;

namespace RexToy.Json
{
    public static class JsonExtension
    {
        //Note:Json standard only support 6 data types (Null, Boolean, Number, String, Object and Array)
        public static string ToJsonString(this object instance)
        {
            if (instance == null)
                return JsonConstant.Null;

            //Note:As the instance!=null, we don't need check Nullable<T>
            Type t = instance.GetType();
            //Note:before go to normal, check if the customized extend converter can handle it.
            IExtendConverter cvt = ExtendConverter.Instance();
            if (cvt.CanConvert(t))
            {
                try
                {
                    return cvt.ToJsonString(instance);
                }
                catch (Exception ex)
                {
                    throw ex.CreateWrapException<JsonExtendConverterException>();
                }
            }

            TypeCode code = Type.GetTypeCode(t);
            switch (code)
            {
                case TypeCode.Boolean:
                    return instance.ToString().ToLower();

                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                case TypeCode.Byte:
                case TypeCode.SByte:
                    return instance.ToString();

                case TypeCode.String:
                case TypeCode.Char:
                    return ((string)instance).Bracketing(StringPair.DoubleQuote);
            }            

            StringBuilder str = new StringBuilder();
            if (!t.IsArray)//Note:Object
            {
                IReflector r = Reflector.Bind(instance, ReflectorPolicy.InstancePublicIgnoreCase);
                foreach (PropertyInfo propertyInfo in r.FindAllProperties())
                {
                    if (propertyInfo.Name == "Item")//Extend:Item is special name for indexer, not support currently.
                        continue;

                    string name = char.ToLower(propertyInfo.Name[0]) + propertyInfo.Name.Substring(1);
                    str.Append(name.Bracketing(StringPair.DoubleQuote));

                    str.Append(JsonConstant.Colon);
                    str.Append(r.GetPropertyValue(propertyInfo.Name).ToJsonString());
                    str.Append(JsonConstant.Comma);
                }
                str.RemoveEnd(JsonConstant.Comma);

                str.Bracketing(StringPair.CurlyBracket);
                return str.ToString();
            }
            else//Note:Array
            {
                Array array = instance as Array;
                if (array.Length > 0)
                {
                    foreach (object element in array)
                    {
                        str.Append(element.ToJsonString());
                        str.Append(JsonConstant.Comma);
                    }
                    str.RemoveEnd(JsonConstant.Comma);
                }

                str.Bracketing(StringPair.SquareBracket);
                return str.ToString();
            }
        }

        public static object ParseToJsonObject(this string jsonText)
        {
            jsonText.ThrowIfNullArgument(nameof(jsonText));

            LexicalParser lp = new LexicalParser();
            lp.SetParseContent(jsonText);
            SemanticParser sp = new SemanticParser();
            sp.SetParseContent(lp.Parse());
            return sp.Parse();
        }

        public static object ParseJson(this string jsonText, Type t, bool ignoreTypeSafe = false)
        {
            object json = jsonText.ParseToJsonObject();

            return JsonHelper.Render(json, t, ignoreTypeSafe);
        }

        public static T ParseJson<T>(this string jsonText, bool ignoreTypeSafe = false)
        {
            return (T)jsonText.ParseJson(typeof(T), ignoreTypeSafe);
        }
    }
}
