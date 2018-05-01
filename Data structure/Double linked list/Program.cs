using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructure
{
    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T> Next { get; set; }
        public Node<T> Previous { get; set; }
    }

    public class DoublyLinkedList<T> : IEnumerable<Node<T>>
    {
        private Node<T> _firstNode;
        private Node<T> _lastNode;
        private int _length;

        public int Length { get { return _length; } }
        public Node<T> FirstNode {  get { return _firstNode; } }
        public Node<T> LastNode { get { return _lastNode; } }

        public void AddFirst(T data)
        {
            Node<T> tmpNode = null;
            if (_firstNode != null)
                tmpNode = _firstNode;

            _firstNode = CreateNode(data, null, tmpNode);

            if (tmpNode != null)
                tmpNode.Previous = _firstNode;
            if (_length == 0)
                _lastNode = _firstNode;

            _length++;            
        }

        public void AddLast(T data)
        {
            Node<T> tmpNode = null;
            if (_lastNode != null)
                tmpNode = _lastNode;

            _lastNode = CreateNode(data, tmpNode, null);

            if (tmpNode != null)
                tmpNode.Next = _lastNode;
            if (_length == 0)
                _firstNode = _lastNode;

            _length++;
        }

        public Node<T> AddAfter(Node<T> node, T data)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            var newNode = CreateNode(data, node, node.Next);

            node.Next.Previous = newNode;
            node.Next = newNode;            

            _length++;

            return newNode;
        }

        public Node<T> AddBefore(Node<T> node, T data)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            var newNode = CreateNode(data, node.Previous, node);

            node.Previous.Next = newNode;
            node.Previous = newNode;

            _length++;

            return newNode;
        }

        public Node<T> FindFirst(T data)
        {
            var iterator = _firstNode;
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

            if (node == _firstNode)
            {                
                _firstNode = node.Next;
                _firstNode.Previous = null;                
            }
            else if (node == _lastNode)
            {
                _lastNode = node.Previous;
                _lastNode.Next = null;                
            }
            else
            {
                node.Previous.Next = node.Next;
                node.Next.Previous = node.Previous;                
            }

            node = null;

            _length--;
        }

        public void Reverse()
        {
            var current = _firstNode;

            while (current != null)
            {
                var temp = current.Next;

                current.Next = current.Previous;
                current.Previous = temp;

                current = temp;
            }

            current = _lastNode;
            _lastNode = _firstNode;
            _firstNode = current;
        }

        private Node<T> CreateNode(T data, Node<T> prev, Node<T> next)
        {
            return new Node<T> { Data = data, Previous = prev, Next = next };
        }

        public IEnumerator<Node<T>> GetEnumerator()
        {
            var iterator = _firstNode;
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
