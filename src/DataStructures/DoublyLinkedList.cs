using System;
using System.Collections;
using System.Collections.Generic;

namespace Cnsl.DataStructures
{
    public class Node<T>
        where T : IEquatable<T>
    {
        public T Value { get; }
        public Node<T> Next { get; set; }
        public Node<T> Previous { get; set; }

        public Node(T value, Node<T> previous, Node<T> next)
        {
            Value = value;
            Previous = previous;
            Next = next;
        }
    }

    public class DoublyLinkedList<T> : IEnumerable<Node<T>>
        where T : IEquatable<T>
    {
        public Node<T> First { get; private set; }
        public Node<T> Last { get; private set; }
        public int Count { get; private set; }

        public Node<T> AddFirst(T value)
        {
            var tempNode = First;
            First = new Node<T>(value, null, tempNode);

            if (tempNode != null)
                tempNode.Previous = First;
            if (Count == 0)
                Last = First;

            Count++;

            return First;
        }

        public Node<T> AddLast(T value)
        {
            var tempNode = Last;
            Last = new Node<T>(value, tempNode, null);

            if (tempNode != null)
                tempNode.Next = Last;
            if (Count == 0)
                First = Last;

            Count++;

            return Last;
        }

        public Node<T> AddAfter(Node<T> node, T value)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            var newNode = new Node<T>(value, node, node.Next);

            if (node.Next != null)
                node.Next.Previous = newNode;
            node.Next = newNode;

            if (node == Last)
                Last = newNode;

            Count++;

            return newNode;
        }

        public Node<T> AddBefore(Node<T> node, T value)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            var newNode = new Node<T>(value, node.Previous, node);

            if (node.Previous != null)
                node.Previous.Next = newNode;
            node.Previous = newNode;

            if (node == First)
                First = newNode;

            Count++;

            return newNode;
        }

        public Node<T> FindFirst(T value)
        {
            var iterator = First;

            while (iterator != null)
            {
                if (iterator.Value.Equals(value))
                    return iterator;

                iterator = iterator.Next;                
            }

            return null;
        }

        public void Remove(Node<T> node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            if (node == First)
            {                
                First = node.Next;
                First.Previous = null;                
            }
            else if (node == Last)
            {
                Last = node.Previous;
                Last.Next = null;                
            }
            else
            {
                node.Previous.Next = node.Next;
                node.Next.Previous = node.Previous;                
            }

            node = null;

            Count--;
        }

        public void Reverse()
        {
            var current = First;

            while (current != null)
            {
                var temp = current.Next;

                current.Next = current.Previous;
                current.Previous = temp;

                current = temp;
            }

            current = Last;
            Last = First;
            First = current;
        }

        public IEnumerator<Node<T>> GetEnumerator()
        {
            var iterator = First;

            while (iterator != null)
            {
                yield return iterator;
                iterator = iterator.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}