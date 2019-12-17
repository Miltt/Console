using Cnsl.Algorithms.Searching;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Algorithms.Searching
{
    [TestClass]
    public class BinaryTests
    {
        [TestMethod]
        public void SearchTest()
        {
            var array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var value = 7;

            var result = Binary.Search(array, value);

            Assert.IsTrue(result != Linear.Unknown, "The item not found in array");
        }

        [TestMethod]
        public void UnsuccsessSearchTest()
        {
            var array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var value = 10;

            var result = Binary.Search(array, value);

            Assert.IsTrue(result == Linear.Unknown, "The item found in array");
        }
    }
}