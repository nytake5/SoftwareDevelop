using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.CustomDictionary
{
    public class CustomDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
    {
        private List<TKey> _keys;
        private List<TValue> _values;
        private LinkedList<KeyValuePair<TKey, TValue>>[] _lst;
        private int _count = 0;
        public CustomDictionary()
        {
            _keys = new List<TKey>();
            _values = new List<TValue>();
            _lst = new LinkedList<KeyValuePair<TKey, TValue>>[4];
            for (int i = 0; i < _lst.Length; i++)
            {
                _lst[i] = new LinkedList<KeyValuePair<TKey, TValue>>();
            }
        }
        public int Count => _count;

        public bool IsReadOnly => false;

        public object Current => throw new NotImplementedException();

        public ICollection<TKey> Keys => _keys;

        public ICollection<TValue> Values => _values;

        public TValue this[TKey key]
        {
            get
            {
                int hash = Math.Abs(key.GetHashCode()) % _lst.Length;
                if (_lst[hash].Count != 0)
                {
                    foreach (var item in _lst[hash])
                    {
                        if (item.Key.Equals(key))
                        {
                            return item.Value;
                        }
                    }
                }
                return (TValue)default;
            }
            set
            {
                int hash = Math.Abs(key.GetHashCode()) % _lst.Length;
                if (_lst[hash].Count != 0)
                {
                    var temp = new KeyValuePair<TKey, TValue>();
                    foreach (var item in _lst[hash])
                    {
                        if (item.Key.Equals(key))
                        {
                            temp = item;
                        }
                    }
                    _lst[hash].Remove(temp);
                    _lst[hash].AddLast(new KeyValuePair<TKey, TValue>(temp.Key, value));
                }
            }
        }

        public void Add(TKey key, TValue value)
        {
            int hash = Math.Abs(key.GetHashCode()) % _lst.Length;
            
            if (_count * 3 > _lst.Length)
            {
                int tempCapacity = _lst.Length * 3;
                LinkedList<KeyValuePair<TKey, TValue>>[] temp = new LinkedList<KeyValuePair<TKey, TValue>>[tempCapacity];
                foreach (var linkList in _lst)
                {
                    foreach (var item in linkList)
                    {
                        int hashLoc = Math.Abs(item.Key.GetHashCode()) % tempCapacity;
                        if (temp[hashLoc] == null)
                        {
                            temp[hashLoc] = new LinkedList<KeyValuePair<TKey, TValue>>();
                        }
                        temp[hashLoc].AddLast(item);
                    }
                }
                hash = Math.Abs(key.GetHashCode()) % tempCapacity;
                _lst = temp;
            }
            if (_keys.Contains(key))
            {
                throw new ArgumentException("Dictionory alredy have this key!");
            }
            else
            {
                var temp = new KeyValuePair<TKey, TValue>(key, value);
                if (_lst[hash] == null)
                {
                    _lst[hash] = new LinkedList<KeyValuePair<TKey, TValue>>();
                }
                _lst[hash].AddLast(temp);
                _keys.Add(key);
                _values.Add(value);
                _count++;
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _keys = new List<TKey>();
            _values = new List<TValue>();
            _lst = new LinkedList<KeyValuePair<TKey, TValue>>[4];
            _count = 0;
            
        }

        public bool Contains(KeyValuePair<TKey, TValue> pair)
        {
            int hash = Math.Abs(pair.Key.GetHashCode()) % _lst.Length;
            if (_lst[hash].Count != 0)
            {
                return true;
            }
            return false;
        }


        public void CopyTo(CustomDictionary<TKey, TValue> cusDic, int arrayIndex)
        {
            cusDic = new CustomDictionary<TKey, TValue>();
            int i = 0;
            foreach (var item in _lst)
            {
                if (i >= arrayIndex)
                {
                    foreach (var item1 in item)
                    {
                        cusDic.Add(item1.Key, item1.Value);
                    }
                }
                i += item.Count;
            }
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            array = new KeyValuePair<TKey, TValue>[_count];
            int i = 0;
            int j = 0;
            foreach (var item in _lst)
            {
                if (i >= arrayIndex)
                {
                    foreach (var item1 in item)
                    {
                        array[j] = item1;
                    }
                    j++;
                }
                i += item.Count;
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < _lst.Length; i++)
            {
                if (_lst[i] != null)
                {
                    foreach (var item in _lst[i])
                    {
                        yield return item;
                    }
                }
            }
        }

        public bool Remove(KeyValuePair<TKey, TValue> pair)
        {
            int hash = Math.Abs(pair.Key.GetHashCode()) % _lst.Length;
            if (_lst[hash].Count == 0)
            {
                return false;
            }
            else
            {
                return _lst[hash].Remove(pair);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool ContainsKey(TKey key)
        {
            return _keys.Contains(key);
        }

        public bool Remove(TKey key)
        {
            int hash = Math.Abs(key.GetHashCode()) % _lst.Length;
            if (_lst[hash].Count == 0)
            {
                return false;
            }
            else
            {
                KeyValuePair<TKey, TValue> temp = default;
                foreach (var item in _lst[hash])
                {
                    if (item.Key.Equals(key))
                    {
                        temp = item;
                    }
                }
                if (temp.Equals((KeyValuePair<TKey, TValue>)default))
                {
                    return false;
                }
                else
                {
                    _lst[hash].Remove(temp);
                }
                return true;
            }
        }

        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            int hash = Math.Abs(key.GetHashCode()) % _lst.Length;
            if (_lst[hash].Count == 0)
            {
                value = default;
                return false;
            }
            else
            {
                foreach (var item in _lst[hash])
                {
                    if (item.Key.Equals(key))
                    {
                        value = item.Value;
                        return true;
                    }
                }
            }
            value = (TValue)default;
            return false;
        }
    }
}
