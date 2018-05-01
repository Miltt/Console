using System;

namespace RBTree
{
    public enum EColor
    {
        Red = 0,
        Black = 1
    }

    public class Node<T>
    {
        public EColor Color { get; set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }
        public Node<T> Parent { get; set; }
        public T Data { get; set; }
    }

    public class RedBlackTree<T> where T : IComparable
    {
        private Node<T> _root;
            
        private void RotateLeft(Node<T> a)
        {
            var b = a.Right;
            a.Right = b.Left;

            if (b.Left != null)
                b.Left.Parent = a;
            if (b != null)
                b.Parent = a.Parent;
            if (a.Parent == null)
                _root = b;
            if (a == a.Parent.Left)
                a.Parent.Left = b;
            else
                a.Parent.Right = b;

            b.Left = a;
            if (a != null)
                a.Parent = b;
        }
            
        private void RotateRight(Node<T> b)
        {                
            var a = b.Left;
            b.Left = a.Right;

            if (a.Right != null)
                a.Right.Parent = b;
            if (a != null)
                a.Parent = b.Parent;
            if (b.Parent == null)
                _root = a;
            if (b == b.Parent.Right)
                b.Parent.Right = a;
            else
                b.Parent.Left = a;

            a.Right = b;
            if (b != null)
                b.Parent = a;
        }                        
            
        public Node<T> Find(T data)
        {
            var isFound = false;
            var temp = _root;

            Node<T> item = null;
            while (!isFound)
            {
                if (temp == null)
                    break;

                var ret = data.CompareTo(temp.Data);
                if (ret < 0) // data < temp.Data
                    temp = temp.Left;
                if (ret > 0) // >
                    temp = temp.Right;
                if (ret == 0) // ==
                {
                    item = temp;
                    isFound = true;
                }
            }

            if (isFound)
            {
                Console.WriteLine("{0} was found", data);
                return temp;
            }
            
            Console.WriteLine("{0} not found", data);
            return null;
        }
            
        public void Add(T data)
        {
            var newNode = new Node<T> { Data = data };

            if (_root == null)
            {
                _root = newNode;
                _root.Color = EColor.Black;
                return;
            }

            Node<T> a = null;
            Node<T> b = _root;

            while (b != null)
            {
                a = b;

                if (newNode.Data.CompareTo(b.Data) < 0) // <
                    b = b.Left;
                else
                    b = b.Right;
            }

            newNode.Parent = a;
            if (a == null)
                _root = newNode;
            else if (newNode.Data.CompareTo(a.Data) < 0) // <
                a.Left = newNode;
            else
                a.Right = newNode;

            newNode.Left = null;
            newNode.Right = null;
            newNode.Color = EColor.Red;

            AddFix(newNode);
        }

        private void AddFix(Node<T> a)
        {                
            while (a != _root && a.Parent.Color == EColor.Red)
            {                    
                if (a.Parent == a.Parent.Parent.Left)
                {
                    var b = a.Parent.Parent.Right;
                    if (b != null && b.Color == EColor.Red) // Case 1: uncle is red
                    {
                        a.Parent.Color = EColor.Black;
                        b.Color = EColor.Black;
                        a.Parent.Parent.Color = EColor.Red;
                        a = a.Parent.Parent;
                    }
                    else // Case 2: uncle is black
                    {
                        if (a == a.Parent.Right)
                        {
                            a = a.Parent;
                            RotateLeft(a);
                        }
                        
                        // Case 3: recolor and rotate
                        a.Parent.Color = EColor.Black;
                        a.Parent.Parent.Color = EColor.Red;
                        RotateRight(a.Parent.Parent);
                    }
                }
                else // mirror image of code above
                {                    
                    Node<T> b = null;

                    b = a.Parent.Parent.Left;
                    if (b != null && b.Color == EColor.Black) // Case 1
                    {
                        a.Parent.Color = EColor.Red;
                        b.Color = EColor.Red;
                        a.Parent.Parent.Color = EColor.Black;
                        a = a.Parent.Parent;
                    }
                    else
                    {
                        if (a == a.Parent.Left)
                        {
                            a = a.Parent;
                            RotateRight(a);
                        }
                        
                        a.Parent.Color = EColor.Black;
                        a.Parent.Parent.Color = EColor.Red;
                        RotateLeft(a.Parent.Parent);
                    }
                }

                _root.Color = EColor.Black;
            }
        }
            
        public void Remove(T data)
        {            
            var a = Find(data);
            if (a == null)
                return;

            Node<T> b = null;
            Node<T> c = null;
            if (a.Left == null || a.Right == null)
                c = a;
            else
                c = Successor(a);

            if (c.Left != null)
                b = c.Left;
            else
                b = c.Right;

            if (b != null)
                b.Parent = c;

            if (c.Parent == null)
                _root = b;
            else if (c == c.Parent.Left)
                c.Parent.Left = b;
            else
                c.Parent.Left = b;

            if (c != a)
                a.Data = c.Data;

            if (c.Color == EColor.Black)
                RemoveFix(b);            
        }
            
        private void RemoveFix(Node<T> a)
        {
            while (a != null && a != _root && a.Color == EColor.Black)
            {
                if (a == a.Parent.Left)
                {
                    var b = a.Parent.Right;
                    if (b.Color == EColor.Red)
                    {
                        b.Color = EColor.Black; // case 1
                        a.Parent.Color = EColor.Red;

                        RotateLeft(a.Parent);

                        b = a.Parent.Right;
                    }

                    if (b.Left.Color == EColor.Black && b.Right.Color == EColor.Black)
                    {
                        b.Color = EColor.Red; // case 2
                        a = a.Parent;
                    }
                    else if (b.Right.Color == EColor.Black)
                    {
                        b.Left.Color = EColor.Black; // case 3
                        b.Color = EColor.Red;

                        RotateRight(b);

                        b = a.Parent.Right;
                    }

                    b.Color = a.Parent.Color; //case 4
                    a.Parent.Color = EColor.Black; 
                    b.Right.Color = EColor.Black;

                    RotateLeft(a.Parent);

                    a = _root; 
                }
                else //mirror code
                {
                    var b = a.Parent.Left;
                    if (b.Color == EColor.Red)
                    {
                        b.Color = EColor.Black;
                        a.Parent.Color = EColor.Red;

                        RotateRight(a.Parent);

                        b = a.Parent.Left;
                    }

                    if (b.Right.Color == EColor.Black && b.Left.Color == EColor.Black)
                    {
                        b.Color = EColor.Black;
                        a = a.Parent;
                    }
                    else if (b.Left.Color == EColor.Black)
                    {
                        b.Right.Color = EColor.Black;
                        b.Color = EColor.Red;

                        RotateLeft(b);

                        b = a.Parent.Left;
                    }

                    b.Color = a.Parent.Color;
                    a.Parent.Color = EColor.Black;
                    b.Left.Color = EColor.Black;

                    RotateRight(a.Parent);

                    a = _root;
                }
            }

            if (a != null)
                a.Color = EColor.Black;
        }

        private Node<T> Successor(Node<T> a)
        {
            if (a.Left != null)
            {
                while (a.Left.Left != null)
                    a = a.Left;

                if (a.Left.Right != null)
                    a = a.Left.Right;

                return a;
            }
            else
            {
                var b = a.Parent;
                while (b != null && a == b.Right)
                {
                    a = b;
                    b = b.Parent;
                }

                return b;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var tree = new RedBlackTree<int>();
            tree.Add(5);
            tree.Add(3);
            tree.Add(7);
            tree.Add(1);
            tree.Add(9);
            tree.Remove(3);

            var node = tree.Find(7);
            Console.WriteLine($"Properties: {node.Data}, {node.Color}");

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
