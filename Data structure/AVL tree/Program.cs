using System;

namespace AVLTree
{
    public class Node
    {
        public int Key { get; set; }
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
        private const int DefaultRootKey = 4;

        private Node _root;
        private IPerformer _performer;

        public Avl(IPerformer performer) 
            : this(performer, DefaultRootKey) { }

        public Avl(IPerformer performer, int rootKey)
        {
            _root = new Node(rootKey);
            _performer = performer;
        }

        public Node Add(int key)
        {
            return Add(_root, key);
        }

        private Node Add(Node node, int key)
        {
            if (node == null)
                return new Node(key);

            if (key < node.Key)
                node.Left = Add(node.Left, key);
            else
                node.Right = Add(node.Right, key);

            return Balancing(node);
        }

        public Node Remove(int key)
        {
            return Remove(_root, key);
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

        public void PreOrderTravers()
        {
            PreOrderTravers(_root);
        }

        private void PreOrderTravers(Node root)
        {
            if (root != null)
            {
                _performer.Perform(root.Key);
                PreOrderTravers(root.Left);
                PreOrderTravers(root.Right);
            }
        }

        public void InOrderTravers()
        {
            InOrderTravers(_root);
        }

        private void InOrderTravers(Node root)
        {
            if (root != null)
            {
                InOrderTravers(root.Left);
                _performer.Perform(root.Key);
                InOrderTravers(root.Right);
            }
        }

        public void PostOrderTravers()
        {
            PostOrderTravers(_root);
        }

        private void PostOrderTravers(Node root)
        {
            if (root != null)
            {
                PostOrderTravers(root.Left);
                PostOrderTravers(root.Right);
                _performer.Perform(root.Key);
            }
        }

        private int GetHeight(Node node)
        {
            return node?.Height ?? 0;
        }

        private int GetBalance(Node node)
        {
            return GetHeight(node.Right) - GetHeight(node.Left);
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

        private Node FindMin(Node node)
        {
            return node.Left != null ? FindMin(node.Left) : node;
        }

        private Node RemoveMin(Node node)
        {
            if (node.Left == null)
                return node.Right;

            node.Left = RemoveMin(node.Left);
            return Balancing(node);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var avlTree = new Avl(new Display());
            avlTree.Add(3);
            avlTree.Add(5);
            avlTree.Add(7);
            avlTree.Add(1);
            avlTree.Add(6);
            
            avlTree.InOrderTravers();
            
            avlTree.Remove(3);
            avlTree.Remove(6);

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }

    public interface IPerformer
    {
        void Perform(int data);
    }

    public class Display : IPerformer
    {
        public void Perform(int data)
        {
            Console.Write($"{data} ");
        }
    }
}