using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructure
{
    public class Node<T>
    {
        public T Data { get; }
        public Node<T> Next { get; set; }
        public Node<T> Previous { get; set; }

        public Node(T data, Node<T> previous, Node<T> next)
        {
            Data = data;
            Previous = previous;
            Next = next;
        }
    }

    public class DoublyLinkedList<T> : IEnumerable<Node<T>>
    {
        public Node<T> First { get; private set; }
        public Node<T> Last { get; private set; }
        public int Length { get; private set; }

        public void AddFirst(T data)
        {
            var tempNode = First != null ? First : null;
            First = CreateNode(data, null, tempNode);

            if (tempNode != null)
                tempNode.Previous = First;
            if (Length == 0)
                Last = First;

            Length++;            
        }

        public void AddLast(T data)
        {
            var tempNode = Last != null ? Last : null;
            Last = CreateNode(data, tempNode, null);

            if (tempNode != null)
                tempNode.Next = Last;
            if (Length == 0)
                First = Last;

            Length++;
        }

        public Node<T> AddAfter(Node<T> node, T data)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            var newNode = CreateNode(data, node, node.Next);

            if (node.Next != null)
                node.Next.Previous = newNode;
            node.Next = newNode;            

            Length++;

            return newNode;
        }

        public Node<T> AddBefore(Node<T> node, T data)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            var newNode = CreateNode(data, node.Previous, node);

            if (node.Previous != null)
                node.Previous.Next = newNode;
            node.Previous = newNode;

            Length++;

            return newNode;
        }

        public Node<T> FindFirst(T data)
        {
            var iterator = First;

            while (iterator != null)
            {
                if (iterator.Data.Equals(data))
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

            Length--;
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

        private Node<T> CreateNode(T data, Node<T> prev, Node<T> next)
        {
            return new Node<T>(data, prev, next);
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

    class Program
    {
        static void Main(string[] args)
        {
            var list = new DoublyLinkedList<string>();
            list.AddFirst("a");
            list.AddFirst("b");
            list.AddLast("c");
            list.AddAfter(list.FirstNode, "u");
            list.AddBefore(list.LastNode, "p");                               
            list.Remove(list.FindFirst("b"));                       
            list.Reverse();

            foreach (var node in list)
                Console.WriteLine(node.Data);

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
