using System;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleApp1
{
    public class HashItem<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public HashItem<TKey, TValue> Next { get; set; }

        public HashItem(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        public override string ToString()
        {
            return "Key: " + Key + " Value: " + Value;
        }
    }

    public class HashMap<TKey, TValue> : IEnumerable<HashItem<TKey, TValue>>
    {
        private const int DefaultTableSize = 8;

        private readonly uint _tableSize;
        private HashItem<TKey, TValue>[] _table;

        public HashMap() : this(DefaultTableSize) { }

        public HashMap(uint tableSize)
        {
            _tableSize = tableSize;
            _table = new HashItem<TKey, TValue>[tableSize];
        }

        public TValue Lookup(TKey key)
        {
            var item = GetItemByKey(key, out uint hash);
            if (item != null)
                return item.Value;

            return default(TValue);
        }

        public void Add(TKey key, TValue value)
        {
            var item = GetItemByKey(key, out uint hash);
            var newItem = new HashItem<TKey, TValue>(key, value);

            if (item == null)
            {
                _table[hash] = newItem;
            }
            else
            {
                if (item.Key.Equals(newItem.Key))
                    throw new InvalidOperationException(nameof(key));

                GetNext(item).Next = newItem;
            }
        }

        public bool KeyExists(TKey key)
        {
            return GetItemByKey(key, out uint hash) != null;
        }

        public void Remove(TKey key)
        {
            var hash = GetHash(key);
            _table[hash] = null;
        }

        private uint GetHash(TKey key)
        {
            if (key.Equals(default(TKey)))
                throw new ArgumentException(nameof(key));

            return GetHashAdler32(key.ToString()) % _tableSize;
        }

        private uint GetHashAdler32(string value)
        {
            const int module = 65521;
            const int numBit = 16;
            uint s1 = 1;
            uint s2 = 0;

            for (int i = 0; i < value.Length; i++)
            {
                s1 = (s1 + value[i]) % module;
                s2 = (s2 + s1) % module;
            }

            return (s2 << numBit) + s1;
        }

        public IEnumerator<HashItem<TKey, TValue>> GetEnumerator()
        {
            foreach (var item in _table)
            {
                if (item != null)
                {
                    yield return item;

                    var temp = item;
                    while (temp != null)
                    {
                        if (temp.Next != null)
                            yield return temp.Next;

                        temp = temp.Next;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private HashItem<TKey, TValue> GetNext(HashItem<TKey, TValue> item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (item.Next == null)
                return item;

            return GetNext(item.Next);
        }

        private HashItem<TKey, TValue> GetItemByKey(TKey key, out uint hash)
        {
            hash = GetHash(key);

            var item = _table[hash];
            while (item != null)
            {
                if (item.Key.Equals(key))
                    return item;

                item = item.Next;
            }

            return null;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var map = new HashMap<int, string>();
            map.Add(1, "a");
            map.Add(2, "b");
            //map.Add(2, "c");

            if (map.KeyExists(2))
                Console.WriteLine(map.Lookup(2));

            map.Remove(2);
            map.Add(2, "d");

            foreach (var item in map)
                Console.WriteLine(item);

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}