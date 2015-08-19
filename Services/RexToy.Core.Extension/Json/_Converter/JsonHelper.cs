using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RexToy.Json
{
    public static class JsonHelper
    {
        public static object Render(object json, Type target, bool ignoreTypeSafe)
        {
            if (json == null)
                return null;

            JsonObject jObj = json as JsonObject;
            if (jObj != null)
            {
                return jObj.Render(target, ignoreTypeSafe);
            }

            JsonArray jArr = json as JsonArray;
            if (jArr != null)
            {
                return jArr.Render(target, ignoreTypeSafe);
            }

            return ConvertToSimpleType(json as string, target, ignoreTypeSafe);
        }

        internal static object ConvertToSimpleType(string jsonText, Type targetType, bool ignoreTypeSafe = false)
        {
            IExtendConverter cvt = ExtendConverter.Instance();
            object result = null;

            if (cvt.CanConvert(targetType))
            {
                try
                {
                    result = cvt.FromJson(targetType, jsonText, ignoreTypeSafe);
                }
                catch (Exception ex)
                {
                    throw ex.CreateWrapException<JsonExtendConverterException>();
                }
            }
            else
            {
                if (ignoreTypeSafe)
                {
                    //Note: If we don't care type safe, always remove quoter if exist, then cast
                    if (jsonText.StartsWith(JsonConstant.Quot) && jsonText.EndsWith(JsonConstant.Quot) && jsonText.Length > 1)
                        jsonText = jsonText.UnBracketing(StringPair.DoubleQuote);

                    result = TypeCast.ChangeToTypeOrNullableType(jsonText, targetType);
                }
                else
                {
                    //Note: If we care type, strict follow JSON standard
                    if (targetType == typeof(string))
                    {
                        if ((!jsonText.StartsWith(JsonConstant.Quot)) || (!jsonText.EndsWith(JsonConstant.Quot)))
                            ExceptionHelper.ThrowSyntaxNoQuotError();
                        result = jsonText.UnBracketing(StringPair.DoubleQuote);
                    }
                    else
                        result = TypeCast.ChangeToTypeOrNullableType(jsonText, targetType);
                }
            }
            return result;
        }
    }
}
