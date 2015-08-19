using System;
using System.Collections;
using System.Collections.Generic;

namespace RexToy.Collections
{
    public sealed class StringKeyDictionary<TValue> : IDictionary<string, TValue>
    {
        private Dictionary<string, TValue> _dict;
        public StringKeyDictionary(bool ignoreCase = false)
        {
            _dict = new Dictionary<string, TValue>();
            _ignoreCase = ignoreCase;
        }

        private string TransformKey(string key)
        {
            if (key == null)
                throw new ArgumentNullException("key");
            return _ignoreCase ? key.ToUpper() : key;
        }

        private KeyValuePair<string, TValue> TransformKeyValuePair(KeyValuePair<string, TValue> item)
        {
            return _ignoreCase ? (new KeyValuePair<string, TValue>(item.Key.ToUpper(), item.Value)) : item;
        }

        #region IStringKeyDictionary<TValue> Members

        private bool _ignoreCase;
        public bool IgnoreCase
        {
            get { return _ignoreCase; }
        }

        #endregion

        #region IDictionary<string,TValue> Members

        public void Add(string key, TValue value)
        {
            key = TransformKey(key);
            _dict.Add(key, value);
        }

        public bool ContainsKey(string key)
        {
            key = TransformKey(key);
            return _dict.ContainsKey(key);
        }

        public ICollection<string> Keys
        {
            get { return _dict.Keys; }
        }

        public bool Remove(string key)
        {
            key = TransformKey(key);
            return _dict.Remove(key);
        }

        public bool TryGetValue(string key, out TValue value)
        {
            key = TransformKey(key);
            return _dict.TryGetValue(key, out value);
        }

        public ICollection<TValue> Values
        {
            get { return _dict.Values; }
        }

        public TValue this[string key]
        {
            get { return _dict[TransformKey(key)]; }
            set { _dict[TransformKey(key)] = value; }
        }

        #endregion

        #region ICollection<KeyValuePair<string,TValue>> Members

        void ICollection<KeyValuePair<string, TValue>>.Add(KeyValuePair<string, TValue> item)
        {
            (_dict as IDictionary<string, TValue>).Add(TransformKeyValuePair(item));
        }

        public void Clear()
        {
            _dict.Clear();
        }

        bool ICollection<KeyValuePair<string, TValue>>.Contains(KeyValuePair<string, TValue> item)
        {
            return (_dict as IDictionary<string, TValue>).Contains(TransformKeyValuePair(item));
        }

        void ICollection<KeyValuePair<string, TValue>>.CopyTo(KeyValuePair<string, TValue>[] array, int arrayIndex)
        {
            (_dict as IDictionary<string, TValue>).CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _dict.Count; }
        }

        bool ICollection<KeyValuePair<string, TValue>>.IsReadOnly
        {
            get { return false; }
        }

        bool ICollection<KeyValuePair<string, TValue>>.Remove(KeyValuePair<string, TValue> item)
        {
            return (_dict as IDictionary<string, TValue>).Remove(TransformKeyValuePair(item));
        }

        #endregion

        #region IEnumerable<KeyValuePair<string,TValue>> Members

        public IEnumerator<KeyValuePair<string, TValue>> GetEnumerator()
        {
            return _dict.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _dict.GetEnumerator();
        }

        #endregion
    }
}
