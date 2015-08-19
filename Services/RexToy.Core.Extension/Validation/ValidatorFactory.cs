using System;
using System.Collections.Generic;

using RexToy.Collections;

namespace RexToy.Validation
{
    public class ValidatorFactory
    {
        public static ValidatorFactory CreateInstance(ICache<Type, IValidator> cache = null)
        {
            return new ValidatorFactory(cache ?? NoCache.GetInstance<Type, IValidator>());
        }

        private ValidatorFactory(ICache<Type, IValidator> cache)
        {
            _cache = cache;
        }

        private ICache<Type, IValidator> _cache;

        public IValidator CreateValidator<T>()
        {
            return CreateValidator(typeof(T));
        }

        public IValidator CreateValidator(Type targetType)
        {
            targetType.ThrowIfNullArgument(nameof(targetType));

            IValidator v = _cache.Get(targetType);
            if (v == null)
            {
                v = new Validator(targetType);
                _cache.Put(targetType, v);
            }

            return v;
        }
    }
}
