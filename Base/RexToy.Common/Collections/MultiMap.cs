using System;
using System.Collections.Generic;

namespace RexToy.Collections
{
    public class MultiMap<K, V> : IMultiMap<K, V>
    {
        private Dictionary<K, List<V>> _dict;
        public MultiMap()
        {
            _dict = new Dictionary<K, List<V>>();
        }

        public MultiMap(int capacity)
        {
            _dict = new Dictionary<K, List<V>>(capacity);
        }

        #region IMultiMap
        public IEnumerable<K> Keys
        {
            get { return _dict.Keys; }
        }

        public ICollection<V> this[K key]
        {
            get { return _dict[key]; }
        }

        public void Add(K key, V val)
        {
            if (_dict.ContainsKey(key))
            {
                _dict[key].Add(val);
            }
            else
            {
                List<V> l = new List<V>();
                l.Add(val);
                _dict[key] = l;
            }
        }

        public bool Remove(K key)
        {
            return _dict.Remove(key);
        }

        public bool ContainsKey(K key)
        {
            return _dict.ContainsKey(key);
        }

        public bool TryGetValue(K key, out ICollection<V> values)
        {
            List<V> l;
            bool b = _dict.TryGetValue(key, out l);
            values = l;
            return b;
        }

        public int KeyCount
        {
            get { return _dict.Count; }
        }

        public int ValueCount
        {
            get
            {
                int sum = 0;
                foreach (var kvp in _dict)
                {
                    sum += kvp.Value.Count;
                }
                return sum;
            }
        }

        public IEnumerator<KeyValuePair<K, ICollection<V>>> GetEnumerator()
        {
            var e = (IEnumerator<KeyValuePair<K, List<V>>>)_dict.GetEnumerator();
            return (IEnumerator<KeyValuePair<K, ICollection<V>>>)e;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _dict.GetEnumerator();
        }
        #endregion
    }
}
