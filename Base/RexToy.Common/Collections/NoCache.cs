using System;
using System.Collections.Generic;

namespace RexToy.Collections
{
    public static class NoCache
    {
        public static ICache<TKey, TValue> GetInstance<TKey, TValue>()
        {
            return new NoCacheImpl<TKey, TValue>();
        }

        class NoCacheImpl<TKey, TValue> : ICache<TKey, TValue>
        {
            internal NoCacheImpl() { }

            public void Put(TKey key, TValue value)
            {                
            }

            public TValue Get(TKey key)
            {
                return default(TValue);
            }
        }
    }
}
