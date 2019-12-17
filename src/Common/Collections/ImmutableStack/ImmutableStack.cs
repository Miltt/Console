using System;
using System.Collections;
using System.Collections.Generic;

namespace Cnsl.Common.Collections
{
    public sealed class ImmutableStack<T> : IImmutableStack<T>, IEnumerable<T>
    {
        private static readonly ImmutableStack<T> _empty = new ImmutableStack<T>();
        private readonly T _head;
        private readonly IImmutableStack<T> _tail;

        public static IImmutableStack<T> Empty => _empty;
        public bool IsEmpty => _tail == null;

        private ImmutableStack() { }

        private ImmutableStack(T head, IImmutableStack<T> tail)
        {
            _head = head;
            _tail = tail;
        }

        public IImmutableStack<T> Push(T value)
        {
            return new ImmutableStack<T>(value, this);
        }

        public IImmutableStack<T> Pop()
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
            for (IImmutableStack<T> stack = this; !stack.IsEmpty; stack = stack.Pop())
                yield return stack.Peek();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IImmutableStack<T> Clear()
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