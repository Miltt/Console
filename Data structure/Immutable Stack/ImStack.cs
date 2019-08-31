using System;
using System.Collections;
using System.Collections.Generic;

namespace Collections
{
    public interface IImStack<T>
    {
        bool IsEmpty { get; }
        IImStack<T> Push(T value);
        IImStack<T> Pop();
        T Peek();
        IImStack<T> Clear();
    }

    public sealed class ImStack<T> : IImStack<T>, IEnumerable<T>
    {
        private static readonly ImStack<T> _empty = new ImStack<T>();
        private readonly T _head;
        private readonly IImStack<T> _tail;

        public static IImStack<T> Empty => _empty;
        public bool IsEmpty => _tail == null;

        private ImStack() { }

        private ImStack(T head, IImStack<T> tail)
        {
            _head = head;
            _tail = tail;
        }

        public IImStack<T> Push(T value)
        {
            return new ImStack<T>(value, this);
        }

        public IImStack<T> Pop()
        {
            ThrowIfEmpty();
            return _tail;
        }

        public T Peek()
        {
            ThrowIfEmpty();
            return _head;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (IImStack<T> stack = this; !stack.IsEmpty; stack = stack.Pop())
                yield return stack.Peek();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IImStack<T> Clear()
        {
            return Empty;
        }

        private void ThrowIfEmpty()
        {
            if (IsEmpty)
                throw new InvalidOperationException("The stack is empty");
        }
    }
}