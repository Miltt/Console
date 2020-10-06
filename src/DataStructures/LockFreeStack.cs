using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace Cnsl.DataStructures
{
    public class LockFreeStack<T> : IProducerConsumerCollection<T>, IReadOnlyCollection<T>
    {
        private class Node
        {
            public readonly T Value;
            public Node Next;

            public Node()
                : this(default(T)) { }

            public Node(T value)
            {
                Value = value;
                Next = null;
            }
        }

        private Node _head;

        public int Count => GetCount();
        public bool IsEmpty => _head is null;
        public bool IsSynchronized => false;
        public object SyncRoot => throw new NotSupportedException();

        public LockFreeStack()
        {
            _head = null;
        }

        public LockFreeStack(IEnumerable<T> collection)
        {
            if (collection is null)
                throw new ArgumentNullException(nameof(collection));
            
            var head = (Node)null;
            foreach (var item in collection)
            {
                var node = new Node(item);
                node.Next = head;
                head = node;
            }

            _head = head;
        }

        public void Push(T item)
        {
            var node = new Node(item);
            node.Next = _head;
            
            if (CompareAndSwap(ref _head, node, node.Next))
                return;
            
            PushInternal(node);
        }

        public bool TryPop(out T result)
        {
            var head = _head;
            if (head == null)
            {
                result = default(T);
                return false;
            }
            
            if (CompareAndSwap(ref _head, head.Next, head))
            {
                result = head.Value;
                return true;
            }
 
            return TryPopInternal(out result);
        }

        public bool TryPeek(out T result)
        {
            var head = _head; 
            if (head is null)
            {
                result = default(T);
                return false;
            }
            
            result = head.Value;
            return true;
        }

        public T[] ToArray()
        {
            return ToList().ToArray();
        }

        public List<T> ToList()
        {
            var result = new List<T>();

            foreach (var node in GetNodes())
                result.Add(node);
            
            return result;
        }

        public bool TryAdd(T item)
        {
            Push(item);
            return true;
        }

        public bool TryTake([MaybeNullWhen(false)] out T item)
        {
            return TryPop(out item);
        }

        public void Clear()
        {
            _head = null;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var node in GetNodes())
                yield return node;
        }
        
        public void CopyTo(Array array, int index)
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));

            ((ICollection)ToList()).CopyTo(array, index);
        }

        public void CopyTo(T[] array, int index)
        {
            if (array is null)
                throw new ArgumentNullException(nameof(array));

            ToList().CopyTo(array, index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private int GetCount()
        {
            var count = 0;
            
            for (var current = _head; current != null; current = current.Next)
                count++;

            return count;
        }

        private void PushInternal(Node node)
        {
            var spin = new SpinWait();
            do
            {
                spin.SpinOnce();
                node.Next = _head;
            }
            while (!CompareAndSwap(ref _head, node, node.Next));
        }

        private bool TryPopInternal(out T result)
        {
            var spin = new SpinWait();
            var head = (Node)null;

            while (true)
            {
                head = _head;
                if (head == null)
                {
                    result = default(T);
                    return false;
                }

                if (CompareAndSwap(ref _head, head.Next, head))
                {
                    result = head.Value;
                    return true;
                }

                spin.SpinOnce();
            }
        }

        private bool CompareAndSwap(ref Node location, Node newValue, Node value)
        {
            return Interlocked.CompareExchange(ref location, newValue, value) == value;
        }

        private IEnumerable<T> GetNodes()
        {
            var current = _head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }
    }
}