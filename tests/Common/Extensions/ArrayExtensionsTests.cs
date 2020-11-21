using System;
using Cnsl.Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Common.Extensions
{
    [TestClass]
    public class ArrayExtensionsTests
    {
        [TestMethod]
        public void SortedByAscTest()
        {
            var array = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            Assert.IsTrue(array.IsSortedByAsc(), "The array is not sorted ascending");
        }

        [TestMethod]
        public void SortedByAscFailsTest()
        {
            var array = new int[] { 0, 1, 2, 3, 4, 6, 5, 7, 8, 9 };

            Assert.IsTrue(!array.IsSortedByAsc(), "The array is sorted ascending");
        }

        [TestMethod]
        public void SortedByDescTest()
        {
            var array = new int[] { 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };

            Assert.IsTrue(array.IsSortedByDesc(), "The array is not sorted descending");
        }

        [TestMethod]
        public void SortedByDescFailTest()
        {
            var array = new int[] { 9, 8, 7, 6, 5, 4, 3, 2, 0, 1 };

            Assert.IsTrue(!array.IsSortedByDesc(), "The array is not sorted descending");
        }
    }
}