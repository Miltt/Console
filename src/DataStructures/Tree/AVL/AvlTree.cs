namespace Cnsl.DataStructures
{
    public class AvlTree
    {
        private const int DefaultBalance = 2;

        public AvlNode Root { get; private set; }

        public void Add(int key)
            => Add(Root, key);

        public void Remove(int key)
            => Remove(Root, key);

        private AvlNode Add(AvlNode node, int key)
        {
            if (node == null)
            {
                node = new AvlNode(key);

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

        private AvlNode Remove(AvlNode node, int key)
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

        private void RecalcHeight(AvlNode node)
        {
            var hLeft = GetHeight(node.Left);
            var hRight = GetHeight(node.Right);
            node.Height = (hLeft > hRight ? hLeft : hRight) + 1;
        }

        private AvlNode RotateRight(AvlNode node)
        {
            var root = node.Left;
            node.Left = root.Right;
            root.Right = node;

            RecalcHeight(node);
            RecalcHeight(root);

            return root;
        }

        private AvlNode RotateLeft(AvlNode node)
        {
            var root = node.Right;
            node.Right = root.Left;
            root.Left = node;

            RecalcHeight(node);
            RecalcHeight(root);

            return root;
        }

        private AvlNode Balancing(AvlNode node)
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

        private AvlNode RemoveMin(AvlNode node)
        {
            if (node.Left == null)
                return node.Right;

            node.Left = RemoveMin(node.Left);
            return Balancing(node);
        }

        private AvlNode FindMin(AvlNode node) 
            => node.Left != null ? FindMin(node.Left) : node;
        
        private int GetHeight(AvlNode node) 
            => node?.Height ?? 0;

        private int GetBalance(AvlNode node) 
            => GetHeight(node.Right) - GetHeight(node.Left);
    }
}