using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1.CustomDictionary
{
    public class CustomDictionary<TKey, TValue> : ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
    {
        private HashSet<TKey> _keys;
        private List<TValue> _values;
        private List<LinkedList<KeyValuePair<TKey, TValue>>> _lst;
        private int _capacity = 4;
        private int _count = 0;
        private List<int> _hashCode;
        public CustomDictionary()
        {
            _keys = new HashSet<TKey>();
            _values = new List<TValue>();
            _lst = new List<LinkedList<KeyValuePair<TKey, TValue>>>();
            _hashCode = new List<int>();
        }
        public int Count => _count;

        public bool IsReadOnly => false;

        public object Current => throw new NotImplementedException();

        public void Add(TKey key, TValue value)
        {
            int hash = Math.Abs(key.GetHashCode());
            
            if (_count >= _capacity)
            {
                _capacity *= 2;
            }
            if (_keys.Contains(key))
            {
                throw new Exception("Dictionory alredy have this key!");
            }
            else
            {
                var temp = new KeyValuePair<TKey, TValue>(key, value);
                _keys.Add(key);
                _values.Add(value);
                _count++;
                int index = _hashCode.IndexOf(hash);
                if (index == -1)
                {
                    var tmp = new LinkedList<KeyValuePair<TKey, TValue>>();
                    tmp.AddLast(temp);
                    _lst.Add(tmp);
                    _hashCode.Add(hash);
                }
                else
                {
                    _lst[index].AddLast(temp);
                }
            }
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            int hash = Math.Abs(item.Key.GetHashCode());
            if (_count >= _capacity)
            {
                _capacity *= 2;
            }
            if (_keys.Contains(item.Key))
            {
                throw new Exception("Dictionory alredy have this key!");
            }
            else
            {
                _keys.Add(item.Key);
                _values.Add(item.Value);
                _count++;
                int index = _hashCode.IndexOf(hash);
                if (index == -1)
                {
                    var tmp = new LinkedList<KeyValuePair<TKey, TValue>>();
                    tmp.AddLast(item);
                    _lst.Add(tmp);
                    _hashCode.Add(hash);
                }
                else
                {
                    _lst[index].AddLast(item);
                }
            }
        }

        public void Clear()
        {
            _keys.Clear(); 
            _values.Clear();
            _lst.Clear();
            _hashCode.Clear();
            _count = 0;
            _capacity = 4;
        }

        public bool Contains(KeyValuePair<TKey, TValue> pair)
        {
            foreach (var item in _lst)
            {
                if (item.Contains(pair))
                {
                    return true;
                }
            }
            return false;
        }

        public bool Contains(TKey item)
        {
            return _keys.Contains(item);
        }

        public bool Contains(TValue item)
        {
            return _values.Contains(item);
        }
        public List<TKey> GetKeys()
        {
            return _keys.ToList<TKey>();
        }
        public List<TValue> GetValues()
        {
            return _values;
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
                }
                i += item.Count;
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            for (int i = 0; i < _count; i++)
            {
                foreach (var item in _lst[i])
                {
                    yield return item;
                }
            }
        }

        public bool Remove(KeyValuePair<TKey, TValue> pair)
        {
            bool flag = false;
            var temp = new LinkedList<KeyValuePair<TKey, TValue>>(); ;
            foreach (var item in _lst)
            {
                if (item.Contains(pair))
                {
                    flag = true;
                    temp = item;
                }
            }
            if (flag)
            {
                if (temp.Count == 1)
                {
                    int index = _lst.IndexOf(temp);
                    _lst.Remove(temp);
                    _keys.Remove(pair.Key);
                    _values.Remove(pair.Value);
                    _hashCode.RemoveAt(index);
                }
                else
                {
                    temp.Remove(pair);
                    _keys.Remove(pair.Key);
                    _values.Remove(pair.Value);
                }
                return true;
            }
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
