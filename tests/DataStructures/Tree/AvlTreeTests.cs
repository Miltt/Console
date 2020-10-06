using System.Linq;
using Cnsl.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.DataStructures
{
    [TestClass]
    public class AvlTreeTests
    {
        [TestMethod]
        public void AddTest()
        {
            const int rootKey = 4;

            var tree = new AvlTree();
            tree.Add(rootKey);

            Assert.IsTrue(tree.Root == rootKey, "Added node with wrong key");
        }

        [TestMethod]
        public void RemoveTest()
        {
            const int rootKey = 4;
            const int removeKey = 3;

            var tree = new AvlTree();
            tree.Add(rootKey);
            tree.Add(removeKey);
            tree.Remove(removeKey);

            Assert.IsTrue(tree.Root == rootKey, "Deleted node with wrong key");
        }

        [TestMethod]
        public void InOrderTraversTest()
        {
            var tree = new AvlTree();
            tree.Add(4);
            tree.Add(3);
            tree.Add(5);
            tree.Add(7);

            var result = tree.InOrderTravers();
            var expectedResult = new[] { 3, 4, 5, 7 };

            Assert.IsTrue(result.SequenceEqual(expectedResult), "The tree traver is wrong");
        }
    }
}