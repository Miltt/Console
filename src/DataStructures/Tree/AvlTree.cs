using System.Collections.Generic;

namespace Cnsl.DataStructures
{
    public class AvlTree
    {
        private class Node
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

        private const int DefaultBalance = 2;

        private Node _root;

        public int Root => _root?.Key ?? -1;

        public void Add(int key)
            => Add(_root, key);

        public void Remove(int key)
            => Remove(_root, key);
        
        public IEnumerable<int> PreOrderTravers()
            => PreOrderTravers(_root);

        public IEnumerable<int> InOrderTravers()
            => InOrderTravers(_root);

        public IEnumerable<int> PostOrderTravers()
            => PostOrderTravers(_root);

        private Node Add(Node node, int key)
        {
            if (node == null)
            {
                node = new Node(key);

                if (_root == null)
                    _root = node;

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

        private Node FindMin(Node node) 
            => node.Left != null ? FindMin(node.Left) : node;
        
        private int GetHeight(Node node) 
            => node?.Height ?? 0;

        private int GetBalance(Node node) 
            => GetHeight(node.Right) - GetHeight(node.Left);

        private IEnumerable<int> PreOrderTravers(Node root)
        {
            if (root != null)
            {
                yield return root.Key;
                foreach (var node in PreOrderTravers(root.Left))
                    yield return node;
                foreach (var node in PreOrderTravers(root.Right))
                    yield return node;
            }
        }

        private IEnumerable<int> InOrderTravers(Node root)
        {
            if (root != null)
            {
                foreach (var node in InOrderTravers(root.Left))
                    yield return node;
                yield return root.Key;
                foreach (var node in InOrderTravers(root.Right))
                    yield return node;
            }
        }

        private IEnumerable<int> PostOrderTravers(Node root)
        {
            if (root != null)
            {
                foreach (var node in PostOrderTravers(root.Left))
                    yield return node;
                foreach (var node in PostOrderTravers(root.Right))
                    yield return node;
                yield return root.Key;
            }
        }
    }
}