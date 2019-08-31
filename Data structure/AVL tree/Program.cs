using System;

namespace AVLTree
{
    public class Node
    {
        public int Key { get; }
        public int Height { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node(int key)
        {
            Key = key;
            Height = 1;
        }
    }

    public class Avl
    {
        private const int DefaultBalance = 2;

        public Node Root { get; private set; }

        public void Add(int key)
            => Add(Root, key);

        public void Remove(int key)
            => Remove(Root, key);

        private Node Add(Node node, int key)
        {
            if (node == null)
            {
                node = new Node(key);

                if (Root == null)
                    Root = node;

                return node;
            }

            if (key < node.Key)
                node.Left = Add(node.Left, key);
            else
                node.Right = Add(node.Right, key);

            return Balancing(node);
        }

        private Node Remove(Node node, int key)
        {
            if (node == null)            
                return null;            

            if (key < node.Key)            
                node.Left = Remove(node.Left, key);            
            else if (key > node.Key)            
                node.Right = Remove(node.Right, key);            
            else // key == node.key
            {
                var rootLeft = node.Left;
                var rootRight = node.Right;

                node = null;

                if (rootRight == null)                
                    return rootLeft;                

                var min = FindMin(rootRight);
                min.Right = RemoveMin(rootRight);
                min.Left = rootLeft;

                return Balancing(min);
            }

            return Balancing(node);
        }

        private void RecalcHeight(Node node)
        {
            var hLeft = GetHeight(node.Left);
            var hRight = GetHeight(node.Right);
            node.Height = (hLeft > hRight ? hLeft : hRight) + 1;
        }

        private Node RotateRight(Node node)
        {
            var root = node.Left;
            node.Left = root.Right;
            root.Right = node;

            RecalcHeight(node);
            RecalcHeight(root);

            return root;
        }

        private Node RotateLeft(Node node)
        {
            var root = node.Right;
            node.Right = root.Left;
            root.Left = node;

            RecalcHeight(node);
            RecalcHeight(root);

            return root;
        }

        private Node Balancing(Node node)
        {
            RecalcHeight(node);

            if (GetBalance(node) == DefaultBalance)
            {
                if (GetBalance(node.Right) < 0)
                    node.Right = RotateRight(node.Right);

                return RotateLeft(node);
            }

            if (GetBalance(node) == -DefaultBalance)
            {
                if (GetBalance(node.Left) > 0)
                    node.Left = RotateLeft(node.Left);

                return RotateRight(node);
            }

            return node;
        }

        private Node RemoveMin(Node node)
        {
            if (node.Left == null)
                return node.Right;

            node.Left = RemoveMin(node.Left);
            return Balancing(node);
        }

        private Node FindMin(Node node) => node.Left != null ? FindMin(node.Left) : node;
        
        private int GetHeight(Node node) => node?.Height ?? 0;

        private int GetBalance(Node node) => GetHeight(node.Right) - GetHeight(node.Left);
    }

    class Program
    {
        static void Main(string[] args)
        {
            var avlTree = new Avl();
            avlTree.Add(4);
            avlTree.Add(3);
            avlTree.Add(5);
            avlTree.Add(7);
            avlTree.Add(1);
            avlTree.Add(6);
            
            AvlTreeTravers.InOrderTravers(avlTree.Root);
            
            avlTree.Remove(3);
            avlTree.Remove(6);

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }

    public static class AvlTreeTravers
    {
        private static readonly IPerformer Performer = new Display();

        public static void PreOrderTravers(Node root)
        {
            if (root != null)
            {
                Performer.Perform(root.Key);
                PreOrderTravers(root.Left);
                PreOrderTravers(root.Right);
            }
        }

        public static void InOrderTravers(Node root)
        {
            if (root != null)
            {
                InOrderTravers(root.Left);
                Performer.Perform(root.Key);
                InOrderTravers(root.Right);
            }
        }

        public static void PostOrderTravers(Node root)
        {
            if (root != null)
            {
                PostOrderTravers(root.Left);
                PostOrderTravers(root.Right);
                Performer.Perform(root.Key);
            }
        }
    }    

    public interface IPerformer
    {
        void Perform(int data);
    }

    public class Display : IPerformer
    {
        public void Perform(int data) => Console.Write($"{data} ");
    }
}