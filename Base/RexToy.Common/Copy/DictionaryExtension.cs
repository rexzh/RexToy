using System;
using System.Collections.Generic;

using RexToy.Logging;

namespace RexToy.Copy
{
    public static class DictionaryExtension
    {
        private static ILog _log = LogContext.GetLogger("RexToy.Copy");

        public static void ShallowCopy(this IDictionary<string, string> src, object dest, CopyOptions option = CopyOptions.BaseOnBoth, bool throwOnNotExist = true)
        {
            src.ThrowIfNullArgument(nameof(src));
            dest.ThrowIfNullArgument(nameof(dest));
            option.ThrowIfEnumOutOfRange();

            switch (option)
            {
                case CopyOptions.BaseOnSource:
                    CopyBySource(src, dest, throwOnNotExist);
                    break;

                case CopyOptions.BaseOnDest:
                    CopyByDest(src, dest, throwOnNotExist);
                    break;

                case CopyOptions.BaseOnBoth:
                    CopyByBoth(src, dest);
                    break;
            }
        }

        private static void CopyBySource(IDictionary<string, string> src, object dest, bool throwOnNotExist)
        {
            IReflector r = Reflector.Bind(dest);
            foreach (var key in src.Keys)
            {
                if (!r.ExistProperty(key))
                {
                    if (throwOnNotExist)
                        ExceptionHelper.ThrowDestPropertyNotExist(dest.GetType(), key);
                    else
                        _log.Warning("Destination type [{0}] does not have property [{1}].", dest.GetType(), key);
                }
                else
                {
                    try
                    {
                        Type pType = r.GetPropertyType(key);
                        object val = TypeCast.ChangeToTypeOrNullableType(src[key], pType);
                        r.SetPropertyValue(key, val);
                    }
                    catch (Exception ex)
                    {
                        throw ex.CreateWrapException<CopyException>();
                    }
                }
            }
        }

        private static void CopyByDest(IDictionary<string, string> src, object dest, bool throwOnNotExist)
        {
            IReflector r = Reflector.Bind(dest);

            foreach (var property in r.FindAllPropertyNames())
            {
                if (src.ContainsKey(property))
                {
                    try
                    {
                        Type pType = r.GetPropertyType(property);
                        object val = TypeCast.ChangeToTypeOrNullableType(src[property], pType);
                        r.SetPropertyValue(property, val);
                    }
                    catch (Exception ex)
                    {
                        throw ex.CreateWrapException<CopyException>();
                    }
                }
                else
                {
                    if (throwOnNotExist)
                        ExceptionHelper.ThrowSourceKeyOrPropertyNotExist(property);
                    else
                        _log.Warning("Source dictionary does not have key [{0}].", property);
                }
            }
        }

        private static void CopyByBoth(IDictionary<string, string> src, object dest)
        {
            IReflector r = Reflector.Bind(dest);
            foreach (var key in src.Keys)
            {
                if (r.ExistProperty(key))
                {
                    try
                    {
                        Type pType = r.GetPropertyType(key);
                        object val = TypeCast.ChangeToTypeOrNullableType(src[key], pType);
                        r.SetPropertyValue(key, val);
                    }
                    catch (Exception ex)
                    {
                        throw ex.CreateWrapException<CopyException>();
                    }
                }
            }
        }
    }
}
