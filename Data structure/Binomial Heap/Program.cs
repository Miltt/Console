using System;
using System.Collections.Generic;

namespace Collections
{
    public class BinomialHeap : IEnumerable<int>
    {
        private class Node
        {        
            public int Key { get; set; }
            public int Degree { get; set; }
            public Node Parent { get; set; }
            public Node Child { get; set; }
            public Node Sibling { get; set; }

            public Node(int key)
            {
                Key = key;
            }
        }

        private Node _heap;
        private Node _heapR;

        public int Count { get; private set; }

        public void Add(int key)
        {
            var node = new Node(key);
            
            _heap = Union(node);
            Count++;
        }

        public bool TryExtractMin(out int key)
        {
            if (_heap == null)
            {
                key = 0;
                return false;
            }

            _heapR = null;
            var retNode = _heap;
            var tmpNode = retNode;
            var minKey = retNode.Key;

            var node = (Node)null;
            while (tmpNode.Sibling != null)
            {
                if (tmpNode.Sibling.Key < minKey)
                {
                    minKey = tmpNode.Sibling.Key;
                    node = tmpNode;
                    retNode = tmpNode.Sibling;
                }

                tmpNode = tmpNode.Sibling;
            }

            if (node == null && retNode.Sibling == null)
                _heap = null;
            else if (node == null)
                _heap = retNode.Sibling;
            else if (node.Sibling == null)
                node = null;
            else
                node.Sibling = retNode.Sibling;

            if (retNode.Child != null)
            {
                Reverse(retNode.Child);
                retNode.Child.Sibling = null;
            }

            _heap = Union(_heapR);
            Count--;

            key = retNode.Key;
            return true;
        }

        public void DecreaseKey(int curKey, int newKey)
        {
            if (newKey > curKey)
                throw new InvalidOperationException("The new key must not exceed the current key");

            var node = Find(_heap, curKey);
            if (node == null)
                throw new InvalidOperationException("Сurrent key not found");

            node.Key = newKey;
            var parentNode = node.Parent;

            while (parentNode != null && node.Key < parentNode.Key)
            {
                var tempKey = node.Key;
                node.Key = parentNode.Key;
                parentNode.Key = tempKey;

                node = parentNode;
                parentNode = parentNode.Parent;
            }
        }

        public void Delete(int key)
        {
            DecreaseKey(key, int.MinValue);
            TryExtractMin(out int minKey);
        }

        private void Link(Node child, Node parent)
        {
            child.Parent = parent;
            child.Sibling = parent.Child;
            parent.Child = child;
            parent.Degree = parent.Degree + 1;
        }
    
        private Node Union(Node node)
        {
            var newNode = Merge(node);
            if (newNode == null)            
                return newNode;

            var currNode = newNode;            
            var nextNode = currNode.Sibling;
            var prevNode = (Node)null;

            while (nextNode != null)
            {
                if ((currNode.Degree != nextNode.Degree) || 
                    (nextNode.Sibling != null && nextNode.Sibling.Degree == currNode.Degree))
                {
                    prevNode = currNode;
                    currNode = nextNode;
                }
                else
                {
                    if (currNode.Key <= nextNode.Key)
                    {
                        currNode.Sibling = nextNode.Sibling;
                        Link(nextNode, currNode);
                    }
                    else
                    {
                        if (prevNode == null)
                            newNode = nextNode;
                        else
                            prevNode.Sibling = nextNode;

                        Link(currNode, nextNode);
                        currNode = nextNode;
                    }
                }

                nextNode = currNode.Sibling;

                if (nextNode == currNode)
                    nextNode = null;
            }

            return newNode;
        }
     
        private Node Merge(Node node)
        {
            var newNode = (Node)null;

            if (_heap != null)
            {
                if (node != null)
                {
                    if (_heap.Degree <= node.Degree)
                        newNode = _heap;
                    else if (_heap.Degree > node.Degree)
                        newNode = node;
                }
                else
                    newNode = _heap;
            }
            else
                newNode = node;

            while (_heap != null && node != null)
            {
                if (_heap.Degree < node.Degree)
                    _heap = _heap.Sibling;
                else if (_heap.Degree == node.Degree)
                {
                    var tmpNode = _heap.Sibling;
                    _heap.Sibling = node;
                    _heap = tmpNode;
                }
                else
                {
                    var tmpNode = node.Sibling;
                    node.Sibling = _heap;
                    node = tmpNode;
                }
            }

            return newNode;
        }
     
        private void Reverse(Node node)
        {
            if (node.Sibling != null)
            {
                Reverse(node.Sibling);
                node.Sibling.Sibling = node;
            }
            else
                _heapR = node;
        }

        private Node Find(Node node, int key)
        {
            if (node.Key == key)
                return node;
            if (node.Child != null)
                return Find(node.Child, key);
            if (node.Sibling != null)
                return Find(node.Sibling, key);

            return null;
        }

        public IEnumerator<int> GetEnumerator()
        {
            var count = Count;
            var key = 0;

            for (int i = 0; i <= count; i++)
            {
                if (TryExtractMin(out key))
                    yield return key;
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
            var heap = new BinomialHeap();
            heap.Add(7);
            heap.Add(5);
            heap.Add(4);
            heap.Add(8);
            heap.Add(11);
            heap.Add(12);
            heap.Add(6);
            heap.Add(3);
            heap.DecreaseKey(5, 2);
            heap.Delete(4);

            foreach (var key in heap)
                Console.WriteLine(key);

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}

