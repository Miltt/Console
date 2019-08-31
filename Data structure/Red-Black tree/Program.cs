using System;

namespace RBTree
{
    public class RedBlackTree<T> 
        where T : IComparable
    {
        private enum EColor
        {
            Red = 0,
            Black = 1
        }

        private class Node
        {
            public EColor Color { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public Node Parent { get; set; }
            public T Data { get; set; }

            public Node(T data)
            {
                Data = data;
            }
        }

        private Node _root;

        public void Add(T data)
        {
            var newNode = new Node(data);

            if (_root == null)
            {
                _root = newNode;
                _root.Color = EColor.Black;
                return;
            }

            var a = (Node)null;
            var b = _root;

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

        public void Remove(T data)
        {            
            var a = Find(data);
            if (a == null)
                return;

            var b = (Node)null;
            var c = (Node)null;

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
            
        private void RotateLeft(Node a)
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
            
        private void RotateRight(Node b)
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
            
        private Node Find(T data)
        {
            var temp = _root;

            while (temp != null)
            {
                var ret = data.CompareTo(temp.Data);
                if (ret < 0) // data < temp.Data
                    temp = temp.Left;
                if (ret > 0) // >
                    temp = temp.Right;
                if (ret == 0) // ==
                    return temp;
            }
            
            return null;
        }

        private void AddFix(Node a)
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
                    var b = (Node)null;
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
            
        private void RemoveFix(Node a)
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

        private Node Successor(Node a)
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

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
