using System;
using System.Collections.Generic;

namespace RexToy.Collections
{
    public interface ICache<TKey, TValue>
    {
        void Put(TKey key, TValue value);
        TValue Get(TKey key);
    }
}
