using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructure
{
    public class HashMap<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private class HashItem
        {
            public TKey Key { get; private set; }
            public TValue Value { get; private set; }
            public HashItem Next { get; set; }

            public HashItem(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }
        }

        private const int DefaultTableSize = 8;
        
        private HashItem[] _table;

        public int Count { get; private set; }

        public HashMap() 
            : this(DefaultTableSize) { }

        public HashMap(int tableSize)
        {
            _table = new HashItem[tableSize];
        }

        public TValue Lookup(TKey key)
        {
            var item = GetItemByKey(key);
            return item != null
                ? item.Value
                : default(TValue);
        }

        public void Add(TKey key, TValue value)
        {
            if (_table.Length == Count)
                Grow();

            var hash = GetHash(key);
            var item = _table[hash];
            var newItem = new HashItem(key, value);

            if (item == null)
            {
                _table[hash] = newItem;
            }
            else
            {
                var tempItem = item;
                while (tempItem != null)
                {
                    if (tempItem.Key.Equals(newItem.Key))
                        throw new InvalidOperationException($"An item with the same key has already been added: {newItem.Key}");
                    
                    if (tempItem.Next == null)
                    {
                        tempItem.Next = newItem;
                        break;
                    }
                    
                    tempItem = tempItem.Next;
                }
            }

            Count++;
        }

        public bool ExistsKey(TKey key)
        {
            return GetItemByKey(key) != null;
        }

        public void Remove(TKey key)
        {
            var hash = GetHash(key);
            var item = _table[hash];

            var prevItem = (HashItem)null;
            var currItem = item;
            while (currItem != null)
            {
                if (currItem.Key.Equals(key))
                {
                    if (prevItem != null)
                        prevItem.Next = currItem.Next;
                    else
                        _table[hash] = currItem.Next;
                    
                    Count--;
                    break;
                }

                prevItem = currItem;
                currItem = currItem.Next;
            }
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var item in _table)
            {
                if (item == null)
                    continue;

                yield return KeyValuePair.Create(item.Key, item.Value);

                var tempItem = item;
                while (tempItem != null)
                {
                    if (tempItem.Next != null)
                        yield return KeyValuePair.Create(tempItem.Next.Key, tempItem.Next.Value);

                    tempItem = tempItem.Next;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private HashItem GetItemByKey(TKey key)
        {
            var hash = GetHash(key);

            var item = _table[hash];
            while (item != null)
            {
                if (item.Key.Equals(key))
                    return item;

                item = item.Next;
            }

            return null;
        }

        private int GetHash(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            return Hash.GetByAdler32(key.ToString()) % _table.Length;
        }

        private void Grow()
        {
            var newSize = _table.Length << 1;
            var newTable = new HashItem[newSize];
            _table.CopyTo(newTable, 0);
            _table = newTable;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var map = new HashMap<int, string>();
            map.Add(1, "a");
            map.Add(2, "b");            
            map.Add(3, "c");
            //map.Add(3, "d");

            if (map.KeyExists(2))
                Console.WriteLine("Value: " + map.Lookup(2));

            map.Remove(3);
            map.Add(3, "e");

            map.Add(4, "f");
            map.Add(5, "g");
            map.Add(6, "h");
            map.Add(7, "i");
            map.Add(8, "j");
            map.Add(9, "k");

            foreach (var item in map)
                Console.WriteLine(item);

            Console.WriteLine("Count: " + map.Count);

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }

    public static class Hash
    {   
        private const int Module = 65521;
        private const int NumBit = 16;

        public static int GetByAdler32(string value)
        {
            if (value is null)
                throw new ArgumentNullException(nameof(value));

            var s1 = 1;
            var s2 = 0;

            for (int i = 0; i < value.Length; i++)
            {
                s1 = (s1 + value[i]) % Module;
                s2 = (s2 + s1) % Module;
            }

            return (s2 << NumBit) + s1;
        }
    }
}