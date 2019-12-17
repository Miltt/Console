using Cnsl.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.DataStructures
{
    [TestClass]
    public class DynamicArrayTests
    {
        [TestMethod]
        public void AddTest()
        {
            const int value = 1;

            var array = new DynamicArray<int>(size: 1);
            array.Add(value);

            Assert.IsTrue(array.Contains(value), "Item not added");
        }

        [TestMethod]
        public void RemoveTest()
        {
            const int value = 1;

            var array = new DynamicArray<int>(size: 1);
            array.Add(value);
            array.Remove(value);

            Assert.IsTrue(!array.Contains(value), "The deleted item exists");
        }

        [TestMethod]
        public void GrowTest()
        {
            var array = new DynamicArray<int>();
            var lengthBefore = array.Length;
            array.Add(1);
            var lengthAfter = array.Length;

            Assert.IsTrue(lengthBefore == 0 && lengthAfter == 1, "Array size has grown incorrectly");
        }

        [TestMethod]
        public void IndexOfTest()
        {
            const int existsValue = 30;
            const int notExistsValue = 40;

            var array = new DynamicArray<int>(size: 8);
            array.Add(1);
            array.Add(2);
            array.Add(existsValue);
            array.Add(4);

            Assert.IsTrue(array.IndexOf(existsValue) != -1 && array.IndexOf(notExistsValue) == -1, "item not found");
        }
    }
}