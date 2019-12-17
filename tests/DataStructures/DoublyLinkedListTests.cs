using Cnsl.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.DataStructures
{
    [TestClass]
    public class DoublyLinkedListTests
    {
        [TestMethod]
        public void AddFirstTest()
        {
            var list = new DoublyLinkedList<int>();
            list.AddFirst(1);

            Assert.IsTrue(list.First == list.Last, "Incorrectly added node");
        }

        [TestMethod]
        public void AddAfterTest()
        {
            var list = new DoublyLinkedList<int>();
            var node = list.AddFirst(1);
            node = list.AddAfter(node, 2);
            node = list.AddAfter(node, 3);

            Assert.IsTrue(list.Last.Value == 3, "Incorrectly added node");
        }

        [TestMethod]
        public void FindFirstTest()
        {
            var list = new DoublyLinkedList<int>();
            var node = list.AddFirst(1);
            node = list.AddAfter(node, 2);
            node = list.FindFirst(2);

            Assert.IsTrue(node != null, "Node find incorrect");
        }

        [TestMethod]
        public void RemoveTest()
        {
            var list = new DoublyLinkedList<int>();
            var node = list.AddFirst(1);
            node = list.AddAfter(node, 2);
            list.AddAfter(node, 3);
            list.Remove(node);

            Assert.IsTrue(list.First.Value == 1 && list.Last.Value == 3, "Node deletion is incorrect");
        }
    }
}