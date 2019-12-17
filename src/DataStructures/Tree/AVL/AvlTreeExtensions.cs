using System.Collections.Generic;

namespace Cnsl.DataStructures
{
    public static class AvlTreeExtensions
    {
        public static IEnumerable<int> PreOrderTravers(this AvlNode root)
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

        public static IEnumerable<int> InOrderTravers(this AvlNode root)
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

        public static IEnumerable<int> PostOrderTravers(this AvlNode root)
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