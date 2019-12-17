using System.Linq;
using Cnsl.DataStructures;
using Cnsl.Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.DataStructures
{
    [TestClass]
    public class BinaryMaxHeapTests
    {
        [TestMethod]
        public void GetMaxTest()
        {
            var array = new[] { 1, 8, 7 };
            var heap = new BinaryMaxHeap(array);

            var max = heap.GetMax();
            const int expectedMax = 8;

            Assert.IsTrue(max == expectedMax, "Invalid maximum element retrieved");
        }

        [TestMethod]
        public void OrderTest()
        {
            var array = new[] { 1, 8, 22, 7, 6, 2, 0, 5, 3, 4 };
            var heap = new BinaryMaxHeap(array);

            Assert.IsTrue(heap.ToArray().IsSortedByDesc(), "The array is not sorted descending");
        }
    }
}