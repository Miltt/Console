using System.Collections.Generic;

namespace Cnsl.Common.Collections
{
    public sealed class PriorityQueue<T> : Heap<T>
    {
        public PriorityQueue()
            : this(Comparer<T>.Default) { }

        public PriorityQueue(Comparer<T> comparer)
            : base(comparer) { }

        public PriorityQueue(IEnumerable<T> collection) 
            : base(collection) { }

        public PriorityQueue(IEnumerable<T> collection, Comparer<T> comparer)
            : base(collection, comparer) { }

        protected override bool Compare(T i, T j)
            => _comparer.Compare(i, j) <= 0;

        public bool IsEmpty()
            => Count == 0;
    }
}