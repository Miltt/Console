using System;
using System.Collections;
using System.Collections.Generic;
using Cnsl.Common.Extensions;

namespace Cnsl.DataStructures
{
    public class HashTable<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private class HashItem
        {
            public TKey Key { get; }
            public TValue Value { get; }
            public HashItem Next { get; set; }

            public HashItem(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }
        }

        private const int DefaultSize = 32;
        
        private HashItem[] _table;

        public int Count { get; private set; }

        public HashTable() 
            : this(DefaultSize) { }

        public HashTable(int size)
        {
            if (size < 0)
                throw new ArgumentException("Must be at least 1", nameof(size));

            _table = new HashItem[size];
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
}