using System;
using System.Collections.Generic;

namespace BinomialHeap
{
    public class Node
    {        
        public int Key { get; set; }
        public int Degree { get; set; }
        public Node Parent { get; set; }
        public Node Child { get; set; }
        public Node Sibling { get; set; }
    }

    public class BinomialHeap
    {
        private Node _heap;
        private Node _heapR;

        public void Add(Node node)
        {
            _heap = Union(node);
        }

        public Node ExtractMin()
        {
            if (_heap == null)
            {
                Console.WriteLine("Heap is empty");
                return null;
            }

            _heapR = null;
            var retHeap = _heap;
            var tmpHeap = retHeap;
            var min = retHeap.Key;

            Node tmpNode = null;
            while (tmpHeap.Sibling != null)
            {
                if (tmpHeap.Sibling.Key < min)
                {
                    min = tmpHeap.Sibling.Key;
                    tmpNode = tmpHeap;
                    retHeap = tmpHeap.Sibling;
                }

                tmpHeap = tmpHeap.Sibling;
            }

            if (tmpNode == null && retHeap.Sibling == null)
                _heap = null;
            else if (tmpNode == null)
                _heap = retHeap.Sibling;
            else if (tmpNode.Sibling == null)
                tmpNode = null;
            else
                tmpNode.Sibling = retHeap.Sibling;

            if (retHeap.Child != null)
            {
                Reverse(retHeap.Child);
                retHeap.Child.Sibling = null;
            }

            _heap = Union(_heapR);
            return retHeap;
        }

        public void DecreaseKey(int curKey, int newKey)
        {
            if (newKey > curKey)
                throw new InvalidOperationException("The new key cannot exceed the current");

            var node = Find(_heap, curKey);
            if (node == null)
                throw new KeyNotFoundException(nameof(curKey));

            node.Key = newKey;

            var parent = node.Parent;
            while (parent != null && node.Key < parent.Key)
            {
                int temp = node.Key;
                node.Key = parent.Key;
                parent.Key = temp;

                node = parent;
                parent = parent.Parent;
            }
        }

        public void Delete(int key)
        {
            DecreaseKey(key, int.MinValue);
            ExtractMin();
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
            var newHeap = Merge(node);
            if (newHeap == null)            
                return newHeap;

            var currHeap = newHeap;            
            var nextHeap = currHeap.Sibling;                       

            Node prevHeap = null;
            while (nextHeap != null)
            {
                if ((currHeap.Degree != nextHeap.Degree) || (nextHeap.Sibling != null && nextHeap.Sibling.Degree == currHeap.Degree))
                {
                    prevHeap = currHeap;
                    currHeap = nextHeap;
                }
                else
                {
                    if (currHeap.Key <= nextHeap.Key)
                    {
                        currHeap.Sibling = nextHeap.Sibling;
                        Link(nextHeap, currHeap);
                    }
                    else
                    {
                        if (prevHeap == null)
                            newHeap = nextHeap;
                        else
                            prevHeap.Sibling = nextHeap;

                        Link(currHeap, nextHeap);
                        currHeap = nextHeap;
                    }
                }

                nextHeap = currHeap.Sibling;

                if (nextHeap == currHeap)
                    nextHeap = null;
            }

            return newHeap;
        }
     
        private Node Merge(Node node)
        {
            Node newHeap = null;

            if (_heap != null)
            {
                if (node != null)
                {
                    if (_heap.Degree <= node.Degree)
                        newHeap = _heap;
                    else if (_heap.Degree > node.Degree)
                        newHeap = node;
                }
                else
                    newHeap = _heap;
            }
            else
                newHeap = node;

            while (_heap != null && node != null)
            {
                if (_heap.Degree < node.Degree)
                    _heap = _heap.Sibling;
                else if (_heap.Degree == node.Degree)
                {
                    var tmpHeap = _heap.Sibling;
                    _heap.Sibling = node;
                    _heap = tmpHeap;
                }
                else
                {
                    var tmpNode = node.Sibling;
                    node.Sibling = _heap;
                    node = tmpNode;
                }
            }

            return newHeap;
        }
     
        private void Reverse(Node heap)
        {
            if (heap.Sibling != null)
            {
                Reverse(heap.Sibling);
                heap.Sibling.Sibling = heap;
            }
            else
                _heapR = heap;
        }

        private Node Find(Node heap, int key)
        {
            if (heap.Key == key)
                return heap;
            if (heap.Child != null)
                return Find(heap.Child, key);
            if (heap.Sibling != null)
                return Find(heap.Sibling, key);

            return null;
        }
    }

    class Program
    {        
        static void Main(string[] args)
        {
            var binomialHeap = new BinomialHeap();
            binomialHeap.Add(new Node { Key = 7 });
            binomialHeap.Add(new Node { Key = 5 });
            binomialHeap.Add(new Node { Key = 4 });
            binomialHeap.Add(new Node { Key = 8 });
            binomialHeap.Add(new Node { Key = 11 });
            binomialHeap.Add(new Node { Key = 12 });
            binomialHeap.Add(new Node { Key = 6 });
            binomialHeap.Add(new Node { Key = 3 });

            binomialHeap.DecreaseKey(5, 2);
            binomialHeap.Delete(4);

            Node node;
            while ((node = binomialHeap.ExtractMin()) != null)
                Console.WriteLine(node.Key);

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}

