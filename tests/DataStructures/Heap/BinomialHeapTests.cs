using System.Linq;
using Cnsl.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.DataStructures
{
    [TestClass]
    public class BinomialHeapTests
    {
        [TestMethod]
        public void AddTest()
        {
            var heap = new BinomialHeap();
            heap.Add(7);
            heap.Add(5);
            heap.Add(4);
            heap.Add(8);

            const int expectedCount = 4;

            Assert.IsTrue(heap.Count == expectedCount, "Incorrect number of items added");
        }

        [TestMethod]
        public void ExtractMinTest()
        {
            var heap = new BinomialHeap();
            heap.Add(7);
            heap.Add(5);
            heap.Add(4);
            heap.Add(8);

            heap.TryExtractMin(out var min);
            const int expectedMin = 4;

            Assert.IsTrue(min == expectedMin, "Extracted the wrong item");
        }

        [TestMethod]
        public void RemoveTest()
        {
            var heap = new BinomialHeap();
            heap.Add(7);
            heap.Add(5);
            heap.Add(4);
            heap.Add(8);
            heap.Remove(4);

            const int expectedCount = 3;

            Assert.IsTrue(heap.Count == expectedCount, "Incorrect number of items after deletion");
        }

        [TestMethod]
        public void DecreaseKeyTest()
        {
            const int newMinKey = 2;

            var heap = new BinomialHeap();
            heap.Add(7);
            heap.Add(5);
            heap.Add(4);
            heap.Add(8);
            heap.DecreaseKey(4, newMinKey);

            heap.TryExtractMin(out var min);

            Assert.IsTrue(min == newMinKey, "The specified key has not changed");
        }
    }
}