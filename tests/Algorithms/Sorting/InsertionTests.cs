using Cnsl.Algorithms.Sorting;
using Cnsl.Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Algorithms.Sorting
{
    [TestClass]
    public class InsertionTests
    {
        [TestMethod]
        public void SortAscTest()
        {
            var array = new int[] { 8, 2, 3, 5, 6, 2, 1, 7, 4, 0, -4, 9 };

            new Insertion().Sort(array);

            Assert.IsTrue(array.IsSortedByAsc(), "The array is not sorted ascending");
        }

        [TestMethod]
        public void SortDescTest()
        {
            var array = new int[] { 8, 2, 3, 5, 6, 2, 1, 7, 4, 0, -4, 9 };

            new Insertion().SortByDescending(array);

            Assert.IsTrue(array.IsSortedByDesc(), "The array is not sorted descending");
        }
    }
}
