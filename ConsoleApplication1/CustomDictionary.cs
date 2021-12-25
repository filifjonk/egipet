using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ksusha
{
    class CustomDictionary<Tkey, Tvalue> : IDictionary<Tkey, Tvalue>
    {
        internal struct egipetskaya_sila
        {
            internal Tkey key;
            internal Tvalue value;
            
            internal int malenki_hashCode;
            
        }

        private List<List<egipetskaya_sila>> HashTable;
        private List<Tkey> KeyCollection;
        private List<Tvalue> ValueCollection;
        private int count;

        public CustomDictionary()
        {
            HashTable = new List<List<egipetskaya_sila>>(10);
            for (int i = 0; i < 10; ++i)
            {
                HashTable.Add(new List<egipetskaya_sila>());
            }

            KeyCollection = new List<Tkey>();
            ValueCollection = new List<Tvalue>();
            count = 0;
        }

        public Tvalue this[Tkey key]
        {
            get
            {
                int x = key.GetHashCode();
                int index = x % HashTable.Capacity;
                
                if (HashTable[index].Count != 0)
                {
                    foreach (egipetskaya_sila i in HashTable[index])
                    {
                        if (i.malenki_hashCode == x)
                        {
                            return i.value;
                        }
                    }
                }

                throw new Exception("not found(");
            }
            set
            {
                int x = key.GetHashCode();
                int index = x % HashTable.Capacity;
                if (HashTable[index].Count != 0)
                {
                    for (int i = 0; i < HashTable[index].Count; ++i)
                    {
                        if (HashTable[index][i].key.Equals(key))
                        {
                            HashTable[index][i] = new egipetskaya_sila {malenki_hashCode = x, key = key, value = value};
                        }
                    }
                }
            }
        }

        public ICollection<Tkey> Keys => KeyCollection;

        public ICollection<Tvalue> Values => ValueCollection;

        public int Count => count;

        public bool IsReadOnly => false;

        public void baAdd(Tkey key, Tvalue value, int x, int index)
        {
            egipetskaya_sila temp = new egipetskaya_sila {malenki_hashCode = x, key = key, value = value};
            HashTable[index].Add(temp);
            KeyCollection.Add(key);
            ValueCollection.Add(value);
            ++count;
        }

        public void Add(Tkey key, Tvalue value)
        {
            int x = key.GetHashCode();
            int index = Math.Abs(x % HashTable.Capacity);
            if (HashTable[index].Count != 0)
            {
                foreach (egipetskaya_sila i in HashTable[index])
                {
                    if (i.malenki_hashCode == x)
                    {
                        throw new Exception("existed");
                    }
                }
            }

            baAdd(key, value, x, index);
        }

        public void Add(KeyValuePair<Tkey, Tvalue> item)
        {
            if (Contains(item))
            {
                throw new Exception("existed");
            }

            int x = item.Key.GetHashCode();
            int index = x % HashTable.Capacity;
            baAdd(item.Key, item.Value, x, index);
        }

        public void Clear()
        {
            HashTable = new List<List<egipetskaya_sila>>();
            KeyCollection = new List<Tkey>();
            ValueCollection = new List<Tvalue>();
            count = 0;
        }

        public bool Contains(KeyValuePair<Tkey, Tvalue> item)
        {
            Tkey key = item.Key;
            Tvalue value = item.Value;
            int x = key.GetHashCode();
            int index = x % HashTable.Capacity;
            if (HashTable[index].Count != 0)
            {
                foreach (egipetskaya_sila i in HashTable[index])
                {
                    if (i.malenki_hashCode == x && i.value.Equals(value))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool ContainsKey(Tkey key)
        {
            int x = key.GetHashCode();
            int index = x % HashTable.Capacity;
            if (HashTable[index].Count != 0)
            {
                foreach (egipetskaya_sila i in HashTable[index])
                {
                    if (i.malenki_hashCode == x)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void CopyTo(KeyValuePair<Tkey, Tvalue>[] array, int arrayIndex)
        {
            for (int i = 0; i < count; ++i)
            {
                array[arrayIndex + i] = new KeyValuePair<Tkey, Tvalue>(KeyCollection[i], ValueCollection[i]);
            }
        }

        public IEnumerator<KeyValuePair<Tkey, Tvalue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(Tkey key)
        {
            int x = key.GetHashCode();
            int index = x % HashTable.Capacity;
            egipetskaya_sila tempEgipetskayaSila = new egipetskaya_sila();
            bool flag = false;
            if (HashTable[index].Count != 0)
            {
                foreach (egipetskaya_sila i in HashTable[index])
                {
                    if (i.malenki_hashCode == x)
                    {   tempEgipetskayaSila = i;
                        flag = true;
                        
                        break;
                    }
                }
            }

            if (flag)
            {
                KeyCollection.Remove(key);
                ValueCollection.Remove(tempEgipetskayaSila.value);
                HashTable[index].Remove(tempEgipetskayaSila);
                --count;
                return true;
            }

            return false;
        }

        public bool Remove(KeyValuePair<Tkey, Tvalue> item)
        {
            Tkey key = item.Key;
            
            int x = key.GetHashCode();
            
            int index = x % HashTable.Capacity;
            
            egipetskaya_sila tempEgipetskayaSila = new egipetskaya_sila();
            
            bool flag = false;
            if (HashTable[index].Count != 0)
            {
                foreach (egipetskaya_sila i in HashTable[index])
                {
                    if (i.malenki_hashCode == x)
                    {
                        flag = true;
                        tempEgipetskayaSila = i;
                        break;
                    }
                }
            }

            if (flag)
            {
                KeyCollection.Remove(key);
                ValueCollection.Remove(tempEgipetskayaSila.value);
                HashTable[index].Remove(tempEgipetskayaSila);
                --count;
                return true;
            }

            return false;
        }

        public bool TryGetValue(Tkey key, out Tvalue value)
        {
            int tempHash = key.GetHashCode();
            int index = tempHash % HashTable.Capacity;
            if (HashTable[index].Count != 0)
            {
                foreach (egipetskaya_sila i in HashTable[index])
                {
                    if (i.malenki_hashCode == tempHash)
                    {
                        value = i.value;
                        return true;
                    }
                }
            }

            value = default(Tvalue);
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}