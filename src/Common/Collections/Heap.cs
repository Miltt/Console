using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Cnsl.Common.Collections
{
    public abstract class Heap<T> : IEnumerable<T>
    {
        private const int GrowMultiplier = 2;

        private T[] _heap = new T[0];
        private int _size = 0;
        private int _tail = 0;
        protected Comparer<T> _comparer;

        public int Count => _tail;
        public int Size => _size;

        protected Heap() 
            : this(Comparer<T>.Default) { }

        protected Heap(Comparer<T> comparer) 
            : this(Enumerable.Empty<T>(), comparer) { }

        protected Heap(IEnumerable<T> collection)
            : this(collection, Comparer<T>.Default) { }
        
        protected Heap(IEnumerable<T> collection, Comparer<T> comparer)
        {
            if (collection == null) 
                throw new ArgumentNullException(nameof(collection));
            if (comparer == null) 
                throw new ArgumentNullException(nameof(comparer));

            _comparer = comparer;

            foreach (var item in collection)
                AddInternal(item);

            for (var i = GetParent(_tail - 1); i >= 0; i--)
                ShiftDown(i);
        }

        protected abstract bool Compare(T i, T j);

        public void Add(T item)
        {
            AddInternal(item);
            ShiftUp(_tail - 1);
        }

        public T Extract()
        {
            if (_tail == 0) 
                throw new InvalidOperationException("Heap is empty");
            
            var item = _heap[0];
            
            _tail--;
            Swap(_tail, 0);
            ShiftDown(0);
            
            return item;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < _tail; i++)
                yield return _heap[i];
        }

        private void ShiftDown(int i)
        {
            var item = i;
            
            item = GetItem(LeftChild(i), item);
            item = GetItem(RightChild(i), item);
            
            if (item == i) 
                return;
            
            Swap(i, item);
            ShiftDown(item);
        }

        private void ShiftUp(int i)
        {
            if (i == 0 || Compare(_heap[GetParent(i)], _heap[i])) 
                return;

            Swap(i, GetParent(i));
            ShiftUp(GetParent(i));
        }

        private int GetItem(int i, int j)
            => i < _tail && !Compare(_heap[j], _heap[i]) ? i : j;

        private void Swap(int i, int j)
        {
            var tmp = _heap[i];
            _heap[i] = _heap[j];
            _heap[j] = tmp;
        }

        private void Grow()
        {
            var size = _size * GrowMultiplier + 1;
            var heap = new T[size];
            
            Array.Copy(_heap, heap, _size);
            _heap = heap;
            _size = size;
        }

        private void AddInternal(T item)
        {
            if (_tail == _size)
                Grow();

            _heap[_tail++] = item;
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator(); 
        
        private static int GetParent(int i)
            => (i + 1) / 2 - 1;

        private static int LeftChild(int i)
            => (i + 1) * 2 - 1;

        private static int RightChild(int i)
            => LeftChild(i) + 1;
    }
}