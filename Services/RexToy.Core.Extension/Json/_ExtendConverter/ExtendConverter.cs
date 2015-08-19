using System;
using System.Collections.Generic;

using RexToy.DesignPattern;

namespace RexToy.Json
{
    public class ExtendConverter : Singleton<ExtendConverter>, IExtendConverter
    {
        private List<IExtendConverter> _converters;
        private ExtendConverter()
        {
            _converters = new List<IExtendConverter>();
        }

        public void Register(Type converterType)
        {
            converterType.ThrowIfNullArgument(nameof(converterType));
            foreach (IExtendConverter cvt in _converters)
            {
                if (cvt.GetType() == converterType)
                    return;
            }
            IExtendConverter converter = Activator.CreateInstance(converterType) as IExtendConverter;
            _converters.Add(converter);
        }

        #region IExtendConverter Members

        public bool CanConvert(Type t)
        {
            foreach (IExtendConverter cvt in _converters)
            {
                if (cvt.CanConvert(t))
                    return true;
            }
            return false;
        }

        public string ToJsonString(object instance)
        {
            if (instance == null)
                return JsonConstant.Null;
            foreach (IExtendConverter cvt in _converters)
            {
                if (cvt.CanConvert(instance.GetType()))
                    return cvt.ToJsonString(instance);
            }
            ExceptionHelper.ThrowNoConverterDefine(instance.GetType());
            return null;
        }

        public object FromJson(Type type, object obj, bool ignoreTypeSafe = false)
        {
            type.ThrowIfNullArgument(nameof(type));
            foreach (IExtendConverter cvt in _converters)
            {
                if (cvt.CanConvert(type))
                    return cvt.FromJson(type, obj, ignoreTypeSafe);
            }
            ExceptionHelper.ThrowNoConverterDefine(type);
            return null;
        }

        #endregion
    }
}
