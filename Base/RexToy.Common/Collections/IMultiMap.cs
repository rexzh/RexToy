using System;
using System.Collections.Generic;

namespace RexToy.Collections
{
    public interface IMultiMap<K, V> : IEnumerable<KeyValuePair<K, ICollection<V>>>
    {
        ICollection<V> this[K key] { get; }

        void Add(K key, V val);
        bool Remove(K key);

        bool ContainsKey(K key);

        bool TryGetValue(K key, out ICollection<V> values);

        int KeyCount { get; }
        int ValueCount { get; }

        IEnumerable<K> Keys { get; }
    }
}
