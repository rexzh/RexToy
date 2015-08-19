using System;
using System.Collections.Generic;

namespace RexToy.Collections
{
    public abstract class LazyLoadDictionary<TKey, TValue>
    {
        private bool _cache;
        private Dictionary<TKey, TValue> _dict;
        protected Dictionary<TKey, TValue> InternalDictionary
        {
            get { return _dict; }
        }

        protected LazyLoadDictionary(bool cache = true)
        {
            _cache = cache;
            _dict = new Dictionary<TKey, TValue>();
        }

        protected abstract TValue Load(TKey key);

        public TValue this[TKey key]
        {
            get
            {
                if (!_cache)
                    return this.Load(key);

                if (_dict.ContainsKey(key))
                    return _dict[key];
                else
                {
                    TValue val = this.Load(key);
                    lock (this)
                    {
                        if (val != null && !_dict.ContainsKey(key))
                        {
                            _dict.Add(key, val);
                        }
                        return val;
                    }
                }
            }
        }
    }
}
