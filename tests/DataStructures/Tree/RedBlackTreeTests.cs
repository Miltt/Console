using Cnsl.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.DataStructures
{
    [TestClass]
    public class RedBlackTreeTests
    {
        [TestMethod]
        public void AddTest()
        {
            const int a = 5;
            const int b = 3;

            var tree = new RedBlackTree<int>();
            tree.Add(a);
            tree.Add(b);

            Assert.IsTrue(tree.Exists(a) && tree.Exists(b), "Added node with wrong key");
        }

        [TestMethod]
        public void RemoveTest()
        {
            const int a = 5;
            const int b = 3;

            var tree = new RedBlackTree<int>();
            tree.Add(a);
            tree.Add(b);
            tree.Remove(b);

            Assert.IsTrue(tree.Exists(a) && !tree.Exists(b), "Deleted node with wrong key");
        }
    }
}